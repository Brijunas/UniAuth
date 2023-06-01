using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
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
                await usernamesAuthService.Register(request.Username, request.Password, cancellationToken);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
