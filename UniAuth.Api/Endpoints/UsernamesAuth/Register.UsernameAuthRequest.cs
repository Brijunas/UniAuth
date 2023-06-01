using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Api.Endpoints.UsernamesAuth
{
    public class RegisterUsernameAuthRequest : UsernameAuthRequestBase, IValidatableObject
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public required override string Username { get; set; }

        [Required]
        [MinLength(14)]
        [MaxLength(56)]
        public required override string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            static string errorMsg(string characterType) => $"Password must contain at least one {characterType}.";

            if (!Password.Any(char.IsLower))
            {
                yield return new ValidationResult(errorMsg("lowercase letter"), new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsUpper))
            {
                yield return new ValidationResult(errorMsg("uppercase letter"), new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsDigit))
            {
                yield return new ValidationResult(errorMsg("digit"), new[] { nameof(Password) });
            }

            if (!Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                yield return new ValidationResult(errorMsg("non-alphanumeric character"), new[] { nameof(Password) });
            }

            // Calculate the size of the string in bytes
            var byteLength = Encoding.UTF8.GetByteCount(Password);
            if (byteLength > 56)
            {
                yield return new ValidationResult("Password too long. It exceeds maximum byte limit of 56.", new[] { nameof(Password) });
            }
        }
    }
}