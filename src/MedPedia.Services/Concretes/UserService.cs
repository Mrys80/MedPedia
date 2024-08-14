using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Entities.Dto;
using MedPedia.Common.Entities.Entities;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class UserService(
    IRepository<User> userRepository,
    IRepository<City> cityRepository,
    IRepository<Province> provinceRepository
) : IScopedDependency, IUserService
{
    public async Task<IDataResult<UserDto>> GetUserInfo(CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var userInfo = await
        (
            from user in userRepository.TableNoTracking
            join province in provinceRepository.TableNoTracking on user.ProvinceId equals province.Id into userProvince
            from province in userProvince.DefaultIfEmpty()
            join city in cityRepository.TableNoTracking on user.CityId equals city.Id into userCity
            from city in userCity.DefaultIfEmpty() 
            where user.Id == currentUser.UserId
            select new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                City = city.CityName,
                CityId = city.Id,
                Province = province.ProvinceName,
                ProvinceId = province.Id,
                PhoneNumber = user.PhoneNumber
            }
        ).FirstOrDefaultAsync(cancellationToken);

        if (userInfo == null)
        {
            return new ErrorDataResult<UserDto>(Messages.NotFound);
        }

        return new SuccessDataResult<UserDto>(userInfo);
    }
    

    public async Task<IDataResult<UserDto>> SetUserInfo(AddUserInfoDto addUserInfoDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var userInfo = await userRepository.GetAsync(
            x =>
                x.IsDeleted == false &&
                x.Id == currentUser.UserId,
            cancellationToken);
        
        userInfo.FirstName = addUserInfoDto.FirstName;
        userInfo.LastName = addUserInfoDto.LastName;
        userInfo.Email = addUserInfoDto.Email;
        userInfo.CityId = addUserInfoDto.CityId;
        userInfo.ProvinceId = addUserInfoDto.ProvinceId;
        userInfo.BirthDate = addUserInfoDto.BirthDate;


        await userRepository.UpdateAsync(userInfo, cancellationToken);

        var userInfoDto = userInfo.Adapt<UserDto>();

        return new SuccessDataResult<UserDto>(userInfoDto);
    }
}