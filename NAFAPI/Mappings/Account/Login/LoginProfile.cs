using AutoMapper;
using NAF.DOMAIN.DTOs.Account.Login;
using NAF.DOMAIN.ViewModels.Account.Login;
using NAFAPI.APPLICATION.Commands.Account.Login;

namespace NAF.Mappings.Account.Login
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginCommand, LoginRequestViewModel> ();
            CreateMap<TokenDTO,  LoginCommandResponse> ();
        }
    }
}
