using System.Net.Http.Headers;
using System.Text.Json;
using backMecatronic.Models.DTOs.External;

namespace backMecatronic.Services.External
{
    public class ApiPeruService : IApiPeruService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public ApiPeruService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<ApiPeruDniResponseDto> ConsultarDni(string dni)
        {
            var baseUrl = _config["ApiPeru:Url"] ?? "https://apiperu.dev/api/dni/{dni}";
            var token = _config["ApiPeru:Token"];

            var url = baseUrl.Replace("{dni}", dni);

            if (!string.IsNullOrWhiteSpace(token))
            {
                if (url.Contains("{token}"))
                    url = url.Replace("{token}", token);
                else if (!url.Contains("api_token=", StringComparison.OrdinalIgnoreCase) &&
                         !url.Contains("token=", StringComparison.OrdinalIgnoreCase))
                {
                    url += (url.Contains('?') ? "&" : "?") + $"api_token={token}";
                }

                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"ApiPeru error: {body}");

            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;

            if (root.TryGetProperty("data", out var dataElement))
                root = dataElement;

            var result = new ApiPeruDniResponseDto
            {
                NumeroDocumento = GetString(root, "numero", "dni", "numero_documento"),
                Nombres = GetString(root, "nombres", "nombre"),
                ApellidoPaterno = GetString(root, "apellido_paterno", "apellidoPaterno", "apellido"),
                ApellidoMaterno = GetString(root, "apellido_materno", "apellidoMaterno"),
                NombreCompleto = GetString(root, "nombre_completo", "nombreCompleto")
            };

            if (string.IsNullOrWhiteSpace(result.NombreCompleto))
            {
                result.NombreCompleto = string.Join(' ', new[]
                {
                    result.ApellidoPaterno,
                    result.ApellidoMaterno,
                    result.Nombres
                }.Where(s => !string.IsNullOrWhiteSpace(s)));
            }

            return result;
        }

        private static string? GetString(JsonElement element, params string[] keys)
        {
            foreach (var key in keys)
            {
                if (element.TryGetProperty(key, out var value) && value.ValueKind == JsonValueKind.String)
                {
                    var str = value.GetString();
                    if (!string.IsNullOrWhiteSpace(str))
                        return str;
                }
            }

            return null;
        }
    }
}
