using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class GaodeApiService
    {
        private HttpClient _client;

        public GaodeApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<JsonElement> GetLocationByAddressAsync(string address)
        {
            var url = "https://restapi.amap.com/v3/geocode/geo?";
            var values = new Dictionary<string, string>();
            values.Add("key", "11f4d0913418bd09074825792d152f4c");
            values.Add("address", address);
            var query =string.Join("&", values.Select(s => $"{s.Key}={s.Value}"));
            var response = await _client.GetAsync(url + query);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var json = JsonDocument.Parse(bytes);
            if (json.RootElement.GetProperty("status").ToString() != "1")
            {
                return default;
            }
            var geocodes = json.RootElement.GetProperty("geocodes");
            return geocodes;
        }

        public async Task<string> GetLocationByAddressAsync(GetFirstLocationByAddressModel req)
        {
            var url = "https://restapi.amap.com/v3/geocode/regeo?";
            var values = new Dictionary<string, string>();
            values.Add("key", "11f4d0913418bd09074825792d152f4c");
            values.Add("location", req.Longitude + ","+ req.Latitude);
            var query = string.Join("&", values.Select(s => $"{s.Key}={s.Value}"));
            var response = await _client.GetAsync(url + query);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var json = JsonDocument.Parse(bytes);
            if (json.RootElement.GetProperty("status").ToString() != "1")
            {
                return default;
            }
            var geocodes = json.RootElement.GetProperty("regeocode");
            string address= geocodes.GetProperty("formatted_address").ToString();

            return address;
        }

        
    }
    public class GetFirstLocationByAddressModel
    {
       public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
