namespace UniAuth.Infra.Auth
{
    public class JwtToken
    {
        public required string Key { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
