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
        public override async Task<ActionResult> HandleAsync(RegisterUsernameAuthRequest request, CancellationToken cancellationToken = default)
        {
            var usernameAuth = new UsernameAuth { Username = request.Username, Password = request.Password };
            var isSuccess = await usernamesAuthService.Register(usernameAuth, cancellationToken);
            return isSuccess ? Ok() : BadRequest();
        }
    }
}
