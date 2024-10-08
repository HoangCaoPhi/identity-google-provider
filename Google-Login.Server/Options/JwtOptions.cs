﻿namespace google_login.Server.Options;

public class JwtOptions
{
    public static string GetSection() => "Jwt";
    public required string Key { get; set; }

    public required string Issuer { get; set; }

    public required string Audience { get; set; }
}
