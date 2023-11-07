using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NAF.DOMAIN.DomainObjects.Account.User;
using NAF.DOMAIN.ViewModels.Account.Login;
using NAF.INFRASTRUCTURE;
using NAF.INFRASTRUCTURE.Interface.Account.Login;
using NAFCommon.Base.Common.EnCrypt;
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
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IMapper mapper, ILoginRepository jWTManagerRepository, IUserRepository userRepository)
        {

            _mapper = mapper;
            _jWTManagerRepository = jWTManagerRepository;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LoginCommandResponse>();
            var param = _mapper.Map<LoginRequestViewModel>(request);
            var existingUser = await _userRepository.Get(x => x.UserName == request.UserName.ToLower() && x.PassWord == CommonEncrypt.ToMD5(request.Password)).FirstOrDefaultAsync(cancellationToken);
            if (existingUser == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                      {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.UserName),
                    });
                return methodResult;
            }
            var token = await _jWTManagerRepository.GenerateJWTTokens(param,cancellationToken);
            methodResult.Result = _mapper.Map<LoginCommandResponse>(token);
            return methodResult;
        }
    }
}