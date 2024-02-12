using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;

namespace Juzhen.MiniProgramAPI
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }

    public class IdentityHttClient
    {
        private readonly HttpClient _httpClient;

        private const string TokenEndpoint = "/connect/token";
        private const string ClientId = "WecahtMiniProgram";
        private const string ClientSecret = "cs3hMfq2aNe31YA!LJaBW1G56PJhkhra";

        public IdentityHttClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenResponse> RequestTokenByPasswordAsync(string account, string password)
        {
            var data = new Dictionary<string, string>();
            data.Add("client_id", ClientId);
            data.Add("client_secret", ClientSecret);
            data.Add("grant_type", "password");
            data.Add("username", account);
            data.Add("password", password);
            var content = new FormUrlEncodedContent(data);
            var response = await _httpClient.PostAsync(TokenEndpoint, content);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ServiceException("身份认证服务器错误");
            }
            var contentResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TokenResponse>(contentResponse);
        }
    }
}
