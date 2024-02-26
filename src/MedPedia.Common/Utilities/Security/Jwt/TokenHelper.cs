using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Entities.Dto;
using MedPedia.Common.Entities.Entities;
using MedPedia.Common.Utilities.Assistant;
using MedPedia.Common.Utilities.Extensions;
using MedPedia.Common.Utilities.Security.Encryption;
using MedPedia.Common.Utilities.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MedPedia.Common.Utilities.Security.Jwt
{
    public class TokenHelper : ITokenHelper, IScopedDependency
    {
        private DateTime _accessTokenExpirationTime;
        private readonly JwtSetting _jwtSetting;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHelper(
            IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<Setting> setting)
        {
            _jwtSetting = setting.Value.JwtSetting;
            _httpContextAccessor = httpContextAccessor;
        }

        public AccessToken CreateToken(User user, IEnumerable<RoleDto> roles, string sessionId)
        {
            _accessTokenExpirationTime = DateTime.Now.AddMinutes(_jwtSetting.ExpirationMinutes);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_jwtSetting.SecretKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(user, signingCredentials, roles, sessionId);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.WriteToken(jwt);

            return new AccessToken {Token = token, Expiration = _accessTokenExpirationTime};
        }

        private JwtSecurityToken CreateJwtSecurityToken(User user, SigningCredentials signingCredentials,
            IEnumerable<RoleDto> roles, string sessionId)
        {
            JwtSecurityToken jwt = new
            (
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: SetClaim(user, roles, sessionId),
                notBefore: DateTime.Now,
                expires: _accessTokenExpirationTime,
                signingCredentials: signingCredentials
            );

            return jwt;
        }

        private IEnumerable<Claim> SetClaim(User user, IEnumerable<RoleDto> roles, string sessionId)
        {
            List<Claim> claims = new();
            claims.AddUsername(user.PhoneNumber);
            claims.AddSessionId(sessionId);
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddRoles(roles.Select(o => o.Title).ToArray());

            return claims;
        }

        public CurrentUser CurrentUser => AccessTokenHelper.GetCurrentUser(_httpContextAccessor.HttpContext);
        public List<string> UserRoles => AccessTokenHelper.GetRoles(_httpContextAccessor.HttpContext);
    }
}