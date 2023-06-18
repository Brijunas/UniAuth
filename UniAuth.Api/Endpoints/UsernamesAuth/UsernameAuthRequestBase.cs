namespace Api.Endpoints.UsernamesAuth
{
    public abstract class UsernameAuthRequestBase
    {
        public required abstract string Username { get; set; }
        public required abstract string Password { get; set; }
    }
}
