using System.Text;
using MedPedia.Common.Utilities.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MedPedia.WebFramework.Configuration;

public static class JwtExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtSetting jwtSetting)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtSetting.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(jwtSetting.EncryptionKey);

            TokenValidationParameters validationParameters = new()
            {
                ClockSkew = TimeSpan
                    .Zero, //impact to expire time & not before - default is 5 min and 5 min after expire also valid
                RequireSignedTokens = true, // token have signature

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true, // default: false
                ValidAudience = jwtSetting.Audience,

                ValidateIssuer = true, // default: false
                ValidIssuer = jwtSetting.Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;

            /*
            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                    logger.LogError("Authentication failed.", context.Exception);

                    if (context.Exception != null)
                        throw new AppException(ResultStatus.Unauthorized, "Authentication failed", HttpStatusCode.Unauthorized, context.Exception, null); // throw context.Exception;

                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    /*
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

                    var claimIdentity = context.Principal.Identity as ClaimsIdentity;
                    if (claimIdentity.Claims?.Any() != true)
                        context.Fail("This toke has no claim.");

                    var securityStamp = claimIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);

                    if (!securityStamp.HasValue())
                        context.Fail("This toke has no security stamp.");

                    // Find user and token from database and perform custom validation
                    var userId = claimIdentity.GetUserId<int>();
                    var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

                    if (user is null)
                        context.Fail("User is not exist");

                    //if(user.SecurityStamp != Guid.Parse(securityStamp))
                    //if(user.SecurityStamp != securityStamp)
                    //    context.Fail("Token security stamp is not valid.");

                    var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                    if (validatedUser is null)
                        context.Fail("Token security stamp is not valid.");

                    if (!user.IsActive)
                        context.Fail("User is not active.");

                    await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                    
                    //return Task.CompletedTask;
                },
                //OnMessageReceived = context =>
                //{

                //},
                OnChallenge = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                    logger.LogError("On challenge error", context.Error, context.ErrorDescription);

                    if (context.AuthenticateFailure != null)
                        throw new AppException(ResultStatus.Unauthorized, "Authentication failed", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null); // throw context.AuthenticateFailure;

                    //throw new AppException(ResultStatus.Unauthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                    throw new UnauthorizedAccessException("You are unauthorized to access this resource.");

                    //return Task.CompletedTask;
                }
            };
            */
        });
    }
}