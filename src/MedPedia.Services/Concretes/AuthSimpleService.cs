using MedPedia.Common.Abstracts;
using MedPedia.Common.Entities.Dto;
using MedPedia.Common.Entities.Entities;
using MedPedia.Common.Utilities.Constants;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Common.Utilities.Security.Hashing;
using MedPedia.Common.Utilities.Security.Jwt;
using MedPedia.Entities.Dto.Auth;
using MedPedia.Entities.Dto.MediaPayamak;
using MedPedia.Entities.Dto.Session;
using MedPedia.Entities.Entities;
using MedPedia.Entities.Enums;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class AuthSimpleService : IAuthSimpleService, IScopedDependency
{
    private readonly IRepository<User> _userRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly ISessionService _sessionService;
    private readonly IRepository<Session> _sessionRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<UsersSmsAuth> _userSmsAuthRepository;
    private readonly IRepository<UserRole> _userRoleRepository;


    public AuthSimpleService(IRepository<User> userRepository, ITokenHelper tokenHelper,
        ISessionService sessionService, IRepository<Session> sessionRepository, IRepository<Role> roleRepository,
        IRepository<UsersSmsAuth> userSmsAuthRepository, IRepository<UserRole> userRoleRepository)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _sessionService = sessionService;
        _sessionRepository = sessionRepository;
        _roleRepository = roleRepository;
        _userSmsAuthRepository = userSmsAuthRepository;
        _userRoleRepository = userRoleRepository;
    }


    public async Task<IDataResult<LoginResultDto>> OtpLogin(OtpDto otpDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.TableNoTracking.FirstOrDefaultAsync(user =>
            user.PhoneNumber == otpDto.PhoneNumber, cancellationToken: cancellationToken);

        if (user is null)
            return new ErrorDataResult<LoginResultDto>(Messages.UserNotFound);

        var userCode =
            await _userSmsAuthRepository.GetAsync(
                x => x.UserId == user.Id && !x.IsDeleted,
                cancellationToken);

        if (userCode.SmsCode != otpDto.Code)
        {
            return new ErrorDataResult<LoginResultDto>(Messages.AuthCodeWrong);
        }

        user.IsDeleted = false;
        await _userRepository.UpdateAsync(user, cancellationToken);
        userCode.IsDeleted = true;
        await _userSmsAuthRepository.UpdateAsync(userCode, cancellationToken);

        var roles = await GetUserRoles(user.Id, cancellationToken);

        var sessionAddDto = new SessionAddDto
        {
            RegUserId = user.Id
        };
        var session = await _sessionService.Add(sessionAddDto, cancellationToken);

        if (session.IsSuccess is false)
        {
            return new ErrorDataResult<LoginResultDto>(Messages.ServerError);
        }
        
        
        var accessToken = _tokenHelper.CreateToken(user, roles, session.Data.Id.ToString());


        var loginResultDto = new LoginResultDto
        {
            UserId = user.Id,
            Token = accessToken.Token,
            FirstName = user.FirstName,
            LastName = user.LastName,
            roles = roles
        };


        return new SuccessDataResult<LoginResultDto>(loginResultDto);
    }

    public async Task<IResult> UserSignUp(string phoneNumber, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(
            x => x.PhoneNumber == phoneNumber,
            cancellationToken);


        if (user == null)
        {
            user = await AddUser(phoneNumber, cancellationToken);
        }


        var smsData = await _userSmsAuthRepository
            .GetAsync(x => x.UserId == user.Id && x.IsDeleted == false, cancellationToken);


        if (smsData is null)
        {
            await SendSms(user, cancellationToken);
        }
        else if ((DateTime.Now - smsData.RegDate).TotalMinutes > 2)
        {
            smsData.IsDeleted = true;
            await SendSms(user, cancellationToken);
        }
        else
        {
            return new ErrorResult(Messages.WaitUntilSmsSend);
        }


        return new SuccessResult();
    }


    private async Task SendSms(User user,
        CancellationToken cancellationToken)
    {
        var authDigit = GenerateRandomNo().ToString();

        var message =
            SMSConstant.Title +
            Environment.NewLine +
            SMSConstant.SubmitCode +
            Environment.NewLine +
            authDigit;

        var values = new Dictionary<string, string>
        {
            { "userName", "mt.09142195677" },
            { "password", "evm*137" },
            { "fromNumber", "1000101" },
            { "toNumbers", user.PhoneNumber },
            { "messageContent", message },
            { "isFlash", "False" },
        };

        var content = new FormUrlEncodedContent(values);
        var response = await new HttpClient().PostAsync($"http://{SMSConstant.SMSUrl}/api/v1/RestWebApi/SendBatchSms",
            content,
            cancellationToken);
        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);


        var mediaPayamakDto = JsonConvert.DeserializeObject<MediaPayamakDto>(responseString);

        await AddSmsToRepository(user, authDigit, cancellationToken);
    }


    private static int GenerateRandomNo()
    {
        const int min = 1000;
        const int max = 9999;
        var rdm = new Random();
        return rdm.Next(min, max);
    }

    private async Task AddSmsToRepository(User user, string authDigit,
        CancellationToken cancellationToken)
    {
        var userSmsAuth = new UsersSmsAuth
        {
            UserId = user.Id,
            SmsCode = authDigit,
            RegUserId = 0,
            RegDate = DateTime.Now,
            IsDeleted = false
        };
        await _userSmsAuthRepository.AddAsync(userSmsAuth, cancellationToken);
    }


    private async Task<User> AddUser(string phoneNumber,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            PhoneNumber = phoneNumber,
            IsDeleted = true,
            RegDate = DateTime.Now,
            RegUserId = 0
        };
        await _userRepository.AddAsync(user, cancellationToken);


        var role = new UserRole
        {
            RoleId = (int)Roles.User,
            UserId = user.Id,
            RegDate = DateTime.Now,
            IsDeleted = false,
            RegUserId = user.Id
        };

        await _userRoleRepository.AddAsync(role, cancellationToken);

        return user;
    }


    private async Task<List<RoleDto>> GetUserRoles(int userId, CancellationToken cancellationToken)
    {
        var data = await (from userRole in _userRoleRepository.Table
                join role in _roleRepository.Table on userRole.RoleId equals role.Id
                where userRole.UserId == userId && userRole.IsDeleted == false
                select new RoleDto { Id = role.Id, Title = role.Title })
            .ToListAsync(cancellationToken);

        return new List<RoleDto>(data);
    }
}