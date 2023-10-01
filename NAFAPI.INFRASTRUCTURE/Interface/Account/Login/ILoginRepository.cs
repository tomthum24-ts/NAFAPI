using NAF.DOMAIN.DTOs.Account.Login;
using NAF.DOMAIN.ViewModels.Account.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NAF.INFRASTRUCTURE.Interface.Account.Login
{
    public interface ILoginRepository
    {
        Task<TokenDTO> GenerateJWTTokens(LoginRequestViewModel users, CancellationToken cancellationToken);
    }
}
