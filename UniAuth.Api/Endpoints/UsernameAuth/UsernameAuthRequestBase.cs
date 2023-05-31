namespace Api.Endpoints.UsernameAuth
{
    public class UsernameAuthRequestBase
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
