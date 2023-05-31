using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.UsernamesAuth
{
    public class Login : EndpointBaseAsync
        .WithRequest<LoginUsernameAuthRequest>
        .WithActionResult

    {
        [HttpPost("[controller]")]
        public override Task<ActionResult> HandleAsync(LoginUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
