using System;

namespace NAF.DOMAIN.DTOs.Account.Login
{
    public class TokenDTO 
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}