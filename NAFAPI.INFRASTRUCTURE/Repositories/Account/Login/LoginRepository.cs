using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NAF.DOMAIN.DTOs.Account.Login;
using NAF.DOMAIN.ViewModels.Account.Login;
using NAF.INFRASTRUCTURE.Interface.Account.Login;
using NAFCommon.Base.Common.EnCrypt;
using NAFCommon.Base.Common.Enum;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NAF.INFRASTRUCTURE.Repositories.Account.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _iconfiguration;
        private readonly IUserRepository _userRepository;

        public LoginRepository(IConfiguration iconfiguration, IUserRepository userRepository)
        {
            _iconfiguration = iconfiguration;
            _userRepository = userRepository;
        }

        public async Task<TokenDTO> GenerateJWTTokens(LoginRequestViewModel request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var time = int.Parse(_iconfiguration["JWT:Time"]);
            var userInfo = await _userRepository.Get(x => x.UserName == request.UserName && x.PassWord == CommonEncrypt.ToMD5(request.Password)).FirstOrDefaultAsync(cancellationToken); ;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                 new Claim(AuthorSetting.UserName, request.UserName)
              }),
                Expires = DateTime.UtcNow.AddMinutes(time),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDTO { AccessToken = tokenHandler.WriteToken(token), ExpiresIn = tokenDescriptor.Expires ?? DateTime.UtcNow };
        }
    }
}