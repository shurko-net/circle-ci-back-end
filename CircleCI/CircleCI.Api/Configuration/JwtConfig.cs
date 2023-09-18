namespace CircleCI.Api.Configuration;

public class JwtConfig
{
    public const string SectionName = "JwtConfiguration";
    public string AccessTokenSecret { get; set; } = string.Empty;
    public string RefreshTokenSecret { get; set; } = string.Empty;
}