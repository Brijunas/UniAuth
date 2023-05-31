namespace Api.Endpoints.UsernamesAuth
{
    public class UsernameAuthRequestBase
    {
        public required virtual string Username { get; set; }
        public required virtual string Password { get; set; }
    }
}
