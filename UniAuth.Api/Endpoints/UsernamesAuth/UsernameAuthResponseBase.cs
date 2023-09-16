using UniAuth.Domain.Users;

namespace UniAuth.Api.Endpoints.UsernamesAuth
{
    public class UsernameAuthResponseBase
    {
        public required User User { get; set; }
        public required string Token { get; set; }
    }
}
