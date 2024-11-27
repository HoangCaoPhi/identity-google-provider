namespace google_login.Server.Abtractions;

public interface IGoogleLoginProvider
{
    Task<GoogleUserInfoResponse?> ValidateTokenAsync(string token);
}
