using MinimalApi.Endpoint;

namespace Api.Authentication
{
    public class LoginEndpoint : IEndpoint
    {
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapPost("api/login", () => HandleAsync());
        }

        private Task<IResult> HandleAsync() => Task.FromResult(Results.Ok("Logged"));
    }
}
