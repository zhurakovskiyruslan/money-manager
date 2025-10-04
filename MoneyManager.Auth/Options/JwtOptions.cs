namespace MoneyManager.Auth.Options;

public class JwtOptions
{
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
    public string Secret { get; init; } = "";  
    public int AccessMinutes { get; init; } = 15;
}