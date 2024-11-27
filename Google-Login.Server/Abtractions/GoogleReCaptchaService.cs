using google_login.Server.Options;
using Microsoft.Extensions.Options;

namespace google_login.Server.Abtractions;

internal sealed class GoogleReCaptchaService(
    HttpClient httpClient,
    IOptions<ReCaptchaOptions> options) : IReCaptchaService
{
    private readonly ReCaptchaOptions _options = options.Value;
    public async Task<bool> ValidateReCaptchaTokenAsync(string recaptchaToken, CancellationToken cancellationToken = default)
    {
        var requestUri = $"api/siteverify?secret={_options.SecretKey}&response={recaptchaToken}";
        var response = await httpClient.GetFromJsonAsync<GoogleReCaptchaResponse>(requestUri, cancellationToken: cancellationToken);

        if (response is null || !response.Success)
            return false;

        return true;
    }
}