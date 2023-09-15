using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniAuth.Domain.UsernamesAuth;

namespace Api.Endpoints.UsernamesAuth
{
    public class Register : EndpointBaseAsync
        .WithRequest<RegisterUsernameAuthRequest>
        .WithActionResult
    {
        private readonly IUsernamesAuthService usernamesAuthService;

        public Register(IUsernamesAuthService usernamesAuthService)
        {
            this.usernamesAuthService = usernamesAuthService;
        }

        [HttpPost("[controller]")]
        public override async Task<ActionResult> HandleAsync([FromBody] RegisterUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await usernamesAuthService.Register(request.Username, request.Password, cancellationToken);
                return Ok(result);
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
