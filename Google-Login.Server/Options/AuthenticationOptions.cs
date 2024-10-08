namespace google_login.Server.Options;

public class GoogleOptions
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
}

public class AuthenticationOptions
{
    public static string GetSection() => "Authentication";

    public required GoogleOptions Google { get; init; }
}
