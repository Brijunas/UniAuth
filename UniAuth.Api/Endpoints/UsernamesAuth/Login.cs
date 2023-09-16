using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Authentication;
using UniAuth.Api.Endpoints.UsernamesAuth;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Infra.Auth;

namespace Api.Endpoints.UsernamesAuth
{
    public class Login : EndpointBaseAsync
        .WithRequest<LoginUsernameAuthRequest>
        .WithActionResult<UsernameAuthResponseBase>

    {
        private readonly IUsernamesAuthService usernamesAuthService;
        private readonly IJwtService jwtService;

        public Login(IUsernamesAuthService usernamesAuthService, IJwtService jwtService)
        {
            this.usernamesAuthService = usernamesAuthService;
            this.jwtService = jwtService;
        }

        [HttpPost("[controller]")]
        public override async Task<ActionResult<UsernameAuthResponseBase>> HandleAsync(LoginUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await usernamesAuthService.Login(request.Username, request.Password, cancellationToken);
                var token = jwtService.GenerateToken(user);

                return new UsernameAuthResponseBase
                {
                    User = user,
                    Token = token
                };
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred." });
            }
        }
    }
}
