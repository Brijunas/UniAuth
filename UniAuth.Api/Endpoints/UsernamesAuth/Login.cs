using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using UniAuth.Domain.UsernamesAuth;

namespace Api.Endpoints.UsernamesAuth
{
    public class Login : EndpointBaseAsync
        .WithRequest<LoginUsernameAuthRequest>
        .WithActionResult

    {
        private readonly IUsernamesAuthService usernamesAuthService;

        public Login(IUsernamesAuthService usernamesAuthService)
        {
            this.usernamesAuthService = usernamesAuthService;
        }

        [HttpPost("[controller]")]
        public override async Task<ActionResult> HandleAsync(LoginUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                await usernamesAuthService.Login(request.Username, request.Password, cancellationToken);
                return Ok();
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
