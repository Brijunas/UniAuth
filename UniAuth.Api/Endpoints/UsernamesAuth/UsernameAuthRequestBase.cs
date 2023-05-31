namespace Api.Endpoints.UsernamesAuth
{
    public class UsernameAuthRequestBase
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
