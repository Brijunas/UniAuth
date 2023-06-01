using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Api.Endpoints.UsernamesAuth
{
    public partial class RegisterUsernameAuthRequest : UsernameAuthRequestBase, IValidatableObject
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
            // Username validation
            if (Username.StartsWith(" ") || Username.EndsWith(" "))
            {
                yield return new ValidationResult("Username cannot start or end with whitespace", new[] { nameof(Username) });
            }

            if (!AplhanumericWithSeparators().IsMatch(Username))
            {
                yield return new ValidationResult("Username can only contain letters, numbers, and the characters _, -, and ., which must be separated by at least one letter or number", new[] { nameof(Username) });
            }

            // Password validation
            static string passwordErrMsg(string characterType) => $"Password must contain at least one {characterType}.";

            if (!Password.Any(char.IsLower))
            {
                yield return new ValidationResult(passwordErrMsg("lowercase letter"), new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsUpper))
            {
                yield return new ValidationResult(passwordErrMsg("uppercase letter"), new[] { nameof(Password) });
            }

            if (!Password.Any(char.IsDigit))
            {
                yield return new ValidationResult(passwordErrMsg("digit"), new[] { nameof(Password) });
            }

            if (!Password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                yield return new ValidationResult(passwordErrMsg("non-alphanumeric character"), new[] { nameof(Password) });
            }

            // Calculate the size of the string in bytes
            var byteLength = Encoding.UTF8.GetByteCount(Password);
            if (byteLength > 56)
            {
                yield return new ValidationResult("Password too long. It exceeds maximum byte limit of 56.", new[] { nameof(Password) });
            }
        }

        [GeneratedRegex("^[a-zA-Z0-9]+([-_.][a-zA-Z0-9]+)*$")]
        private static partial Regex AplhanumericWithSeparators();
    }
}