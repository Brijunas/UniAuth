namespace UniAuth.Domain.UsernamesAuth
{
    public class UsernameAuth
    {
        public string? Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
