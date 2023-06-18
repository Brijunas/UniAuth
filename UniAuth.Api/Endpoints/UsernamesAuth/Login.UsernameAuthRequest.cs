using System.ComponentModel.DataAnnotations;

namespace Api.Endpoints.UsernamesAuth
{
    public class LoginUsernameAuthRequest : UsernameAuthRequestBase
    {
        [Required]
        public override required string Username { get; set; }

        [Required]
        public override required string Password { get; set; }
    }
}