using MediatR;
using NAFCommon.Base.Common.Enum;
using NAFCommon.Base.Common.MethodResult;
using System.Threading;
using System.Threading.Tasks;

namespace NAFAPI.APPLICATION.Commands.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MethodResult<LoginCommandResponse>>
    {
        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LoginCommandResponse>();
            methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                   {
                        ErrorHelpers.GenerateErrorResult(request.UserName, request.UserName)
                    });
            return methodResult;
        }
    }
}