using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniAuth.Api.Endpoints.UsernamesAuth;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Infra.Auth;

namespace Api.Endpoints.UsernamesAuth
{
    public class Register : EndpointBaseAsync
        .WithRequest<RegisterUsernameAuthRequest>
        .WithActionResult<UsernameAuthResponseBase>
    {
        private readonly IUsernamesAuthService usernamesAuthService;
        private readonly IJwtService jwtService;

        public Register(IUsernamesAuthService usernamesAuthService, IJwtService jwtService)
        {
            this.usernamesAuthService = usernamesAuthService;
            this.jwtService = jwtService;
        }

        [HttpPost("[controller]")]
        public override async Task<ActionResult<UsernameAuthResponseBase>> HandleAsync([FromBody] RegisterUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await usernamesAuthService.Register(request.Username, request.Password, cancellationToken);
                var token = jwtService.CreateToken(user);

                return new UsernameAuthResponseBase
                {
                    User = user,
                    Token = token
                };
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
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
