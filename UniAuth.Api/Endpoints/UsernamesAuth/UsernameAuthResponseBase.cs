using UniAuth.Domain.Users;
using UniAuth.Infra.Auth;

namespace UniAuth.Api.Endpoints.UsernamesAuth
{
    public class UsernameAuthResponseBase
    {
        public required User User { get; set; }
        public required JwtToken Token { get; set; }
    }
}
