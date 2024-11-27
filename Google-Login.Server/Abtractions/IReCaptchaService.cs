namespace google_login.Server.Abtractions;

public interface IReCaptchaService
{
    Task<bool> ValidateReCaptchaTokenAsync(string recaptchaToken, CancellationToken cancellationToken = default);
}

