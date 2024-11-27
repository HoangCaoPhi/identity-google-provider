namespace google_login.Server.Options;

public sealed class ReCaptchaOptions
{
    public const string SectionName = "ReCaptcha";
    public required string SecretKey { get; init; }
}
