using AutoMapper;
using MediatR;
using NAF.DOMAIN.ViewModels.Account.Login;
using NAF.INFRASTRUCTURE.Interface.Account.Login;
using NAFCommon.Base.Common.Enum;
using NAFCommon.Base.Common.MethodResult;
using System.Threading;
using System.Threading.Tasks;

namespace NAFAPI.APPLICATION.Commands.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MethodResult<LoginCommandResponse>>
    {
        private readonly ILoginRepository _jWTManagerRepository;
        private readonly IMapper _mapper;

        public LoginCommandHandler(ILoginRepository jWTManagerRepository, IMapper mapper)
        {
            _jWTManagerRepository = jWTManagerRepository;
            _mapper = mapper;
        }

        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LoginCommandResponse>();
            var param = _mapper.Map<LoginRequestViewModel>(request);
            var token = await _jWTManagerRepository.GenerateJWTTokens(param,cancellationToken);
            if(token == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                 {
                        ErrorHelpers.GenerateErrorResult(request.UserName, request.UserName)
                    });
            }
            methodResult.Result = _mapper.Map<LoginCommandResponse>(token);
            return methodResult;
        }
    }
}