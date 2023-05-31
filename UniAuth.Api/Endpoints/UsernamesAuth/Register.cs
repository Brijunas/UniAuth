using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.UsernamesAuth
{
    public class Register : EndpointBaseAsync
        .WithRequest<RegisterUsernameAuthRequest>
        .WithActionResult
    {
        [HttpPost("[controller]")]
        public override Task<ActionResult> HandleAsync(RegisterUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
