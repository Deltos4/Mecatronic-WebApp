using backMecatronic.Models.DTOs.External;
using System.Text;
using System.Text.Json;

namespace backMecatronic.Services.External
{
    public class NubeFactService : INubeFactService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public NubeFactService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<NubeFactResponseDto> Enviar(NubeFactRequestDto request)
        {
            var url = _config["NubeFact:Url"];
            var token = _config["NubeFact:Token"];

            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", $"Token token={token}");

            var response = await _http.PostAsync(url, content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"NubeFact error: {responseBody}");

            return JsonSerializer.Deserialize<NubeFactResponseDto>(responseBody)!;
        }
    }
}
