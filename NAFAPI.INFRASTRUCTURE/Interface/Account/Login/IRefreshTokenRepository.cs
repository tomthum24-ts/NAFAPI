using NAF.DOMAIN.DomainObjects.Account.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAF.INFRASTRUCTURE.Interface.Account.Login
{
    public interface IRefreshTokenRepository 
    {
        UserRefreshToken GenerateRefreshToken(string ipAddress, string userName);
    }
}
