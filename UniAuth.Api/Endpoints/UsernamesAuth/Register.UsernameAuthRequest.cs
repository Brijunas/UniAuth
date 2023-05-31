using System.ComponentModel.DataAnnotations;

namespace Api.Endpoints.UsernamesAuth
{
    public class RegisterUsernameAuthRequest : UsernameAuthRequestBase
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public required override string Username { get; set; }

        [Required]
        [MinLength(14)]
        [MaxLength(56)]
        public required override string Password { get; set; }
    }
}