namespace UniAuth.Api.Settings
{
    public class Jwt
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Key { get; set; }
    }
}
