namespace CircleCI.Services.Configuration;

public class JwtConfig
{ 
    public JwtConfiguration JwtConfiguration { get; set; } = new();
}

public class JwtConfiguration
{
    public string AccessTokenSecret { get; set; } = string.Empty;
    public string RefreshTokenSecret { get; set; } = string.Empty;
}