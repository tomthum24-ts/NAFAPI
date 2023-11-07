using NAF.DOMAIN.DTOs.Account.Login;
using NAF.DOMAIN.ViewModels.Account.Login;
using System.Threading;
using System.Threading.Tasks;

namespace NAF.INFRASTRUCTURE.Interface.Account.Login
{
    public interface ILoginRepository
    {
        Task<TokenDTO> GenerateJWTTokens(LoginRequestViewModel users, CancellationToken cancellationToken);
    }
}