using UniAuth.Domain.Auth;
using UniAuth.Domain.Users;

namespace UniAuth.Domain.UsernamesAuth
{
    public class AuthenticatedUser
    {
        public required JwtToken Token { get; set; }
        public required User User { get; set; }
    }
}
