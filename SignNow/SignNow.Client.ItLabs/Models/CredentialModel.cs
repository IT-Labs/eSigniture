using Newtonsoft.Json;

namespace SignNow.Client.ItLabs.Model
{
    public class CredentialModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}
