using Microsoft.Extensions.Configuration;
using NAF.DOMAIN.DomainObjects.Account.Login;
using NAF.INFRASTRUCTURE.Interface.Account.Login;
using System;
using Microsoft.AspNetCore.Http;

namespace NAF.INFRASTRUCTURE.Repositories.Account.Login
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IConfiguration _iconfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenRepository(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public UserRefreshToken GenerateRefreshToken(string ipAddress, string userName)
        {
            //var randomBytes = CMSEncryption.RandomBytes();

            //#region LogDevice

            //var deviceModel = _httpContextAccessor.HttpContext.Request.GetDeviceInformation(_browserDetector.Browser);

            //#endregion LogDevice

            //var refreshToken = new UserRefreshToken(
            //     Convert.ToBase64String(randomBytes),
            //     DateTime.UtcNow.AddMinutes(int.Parse(_iconfiguration["JWT:TimeRefresh"])),
            //     ipAddress,
            //     userName,
            //     null,
            //     null,
            //     null,
            //     true,
            //     deviceModel.UserAgent,
            //     deviceModel.Type,
            //     deviceModel.OsName,
            //     deviceModel.OsVersion,
            //     deviceModel.DeviceHash,
            //     deviceModel.BrowserName,
            //     deviceModel.BrowserVersion,
            //     deviceModel.TimeZone
            //    );
            //return refreshToken;
            throw new NotImplementedException();
        }
    }
}