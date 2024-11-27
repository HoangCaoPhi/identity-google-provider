using Newtonsoft.Json;

namespace google_login.Server.Abtractions;

public class GoogleUserInfoResponse
{
    [JsonProperty("sub")]
    public string Sub { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("given_name")]
    public string GivenName { get; set; }

    [JsonProperty("family_name")]
    public string FamilyName { get; set; }

    [JsonProperty("picture")]
    public Uri Picture { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("email_verified")]
    public bool EmailVerified { get; set; }
}
