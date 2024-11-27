using System.Text.Json.Serialization;

namespace google_login.Server.Abtractions;

public class GoogleReCaptchaResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")]
    public DateTime ChallengeTimestamp { get; set; }

    [JsonPropertyName("hostname")]
    public string Hostname { get; set; }

    [JsonPropertyName("error-codes")]
    public string[] ErrorCodes { get; set; }
}
