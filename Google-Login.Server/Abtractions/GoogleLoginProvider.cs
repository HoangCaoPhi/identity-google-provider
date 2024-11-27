namespace google_login.Server.Abtractions;

public class GoogleLoginProvider(IHttpClientFactory httpClientFactory) : IGoogleLoginProvider
{
    public const string OAuthApiClientName = "OAuthApiClient";
    public const string GoogleApiClientName = "GoogleApiClient";

    public async Task<GoogleUserInfoResponse?> ValidateTokenAsync(string token)
    {
       return   IsIdToken(token) ?
                  await GetTokenInfoAsync(token) :
                  await GetUserInfoAsync(token);
    }

    private async Task<GoogleUserInfoResponse?> GetUserInfoAsync(string accessToken)
    {
        using var client = httpClientFactory.CreateClient(GoogleApiClientName);

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

        try
        {
            var res = await client.GetFromJsonAsync<GoogleUserInfoResponse>("/oauth2/v3/userinfo");
            return res;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private async Task<GoogleUserInfoResponse?> GetTokenInfoAsync(string idToken)
    {
        using var client = httpClientFactory.CreateClient(OAuthApiClientName);

        try
        {
            var res = await client.GetFromJsonAsync<GoogleUserInfoResponse>($"tokeninfo?id_token={idToken}");
            return res;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
 
    private static bool IsIdToken(string token)
    {
        return token.Split('.').Length == 3;
    }
}
