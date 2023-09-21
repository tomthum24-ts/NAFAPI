using MediatR;
using NAFCommon.Base.Common.MethodResult;
using System;

namespace NAFAPI.APPLICATION.Commands.Account.Login
{
    public class LoginCommand : IRequest<MethodResult<LoginCommandResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}