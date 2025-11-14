using System.Text;
using System.Text.Json;
using AnalizadorSoftware.Models;

namespace AnalizadorSoftware.Services;

public class AnalizadorService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AnalizadorService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["Anthropic:ApiKey"] ?? "";
    }

    public async Task<List<Proceso>> AnalizarEspecificacion(string especificacion)
    {
        var prompt = $@"Analiza la siguiente especificación de software y extrae los procesos, subprocesos y casos de uso necesarios.

ESPECIFICACIÓN:
{especificacion}

Responde ÚNICAMENTE con un JSON válido sin ningún texto adicional, con esta estructura exacta:
{{
  ""procesos"": [
    {{
      ""nombre"": ""Nombre del proceso"",
      ""descripcion"": ""Descripción del proceso"",
      ""subprocesos"": [
        {{
          ""nombre"": ""Nombre del subproceso"",
          ""descripcion"": ""Descripción del subproceso"",
          ""casos_uso"": [
            {{
              ""nombre"": ""Nombre del caso de uso"",
              ""descripcion"": ""Descripción del caso de uso"",
              ""actor_principal"": ""Actor principal"",
              ""tipo_caso_uso"": 1,
              ""precondiciones"": ""Precondiciones"",
              ""postcondiciones"": ""Postcondiciones"",
              ""criterios_de_aceptacion"": ""Criterios de aceptación""
            }}
          ]
        }}
      ]
    }}
  ]
}}

IMPORTANTE: 
- tipo_caso_uso debe ser: 1=Funcional, 2=No Funcional, 3=Sistema
- Responde SOLO con el JSON, sin texto antes ni después
- No uses markdown ni ```json
";

        var requestBody = new
        {
            model = "claude-sonnet-4-20250514",
            max_tokens = 4096,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages")
        {
            Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        };

        request.Headers.Add("x-api-key", _apiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(responseContent);
        
        var claudeResponse = responseJson.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString() ?? "";

        // Limpiar cualquier markdown o texto adicional
        claudeResponse = claudeResponse.Trim();
        if (claudeResponse.StartsWith("```json"))
        {
            claudeResponse = claudeResponse.Substring(7);
        }
        if (claudeResponse.StartsWith("```"))
        {
            claudeResponse = claudeResponse.Substring(3);
        }
        if (claudeResponse.EndsWith("```"))
        {
            claudeResponse = claudeResponse.Substring(0, claudeResponse.Length - 3);
        }
        claudeResponse = claudeResponse.Trim();

        var analisis = JsonSerializer.Deserialize<AnalisisResponse>(claudeResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return ConvertirAProcesos(analisis);
    }

    private List<Proceso> ConvertirAProcesos(AnalisisResponse? analisis)
    {
        if (analisis?.Procesos == null) return new List<Proceso>();

        var procesos = new List<Proceso>();

        foreach (var procesoDto in analisis.Procesos)
        {
            var proceso = new Proceso
            {
                Nombre = procesoDto.Nombre ?? "",
                Descripcion = procesoDto.Descripcion,
                Subprocesos = new List<Subproceso>()
            };

            if (procesoDto.Subprocesos != null)
            {
                foreach (var subprocesoDto in procesoDto.Subprocesos)
                {
                    var subproceso = new Subproceso
                    {
                        Nombre = subprocesoDto.Nombre ?? "",
                        Descripcion = subprocesoDto.Descripcion,
                        CasosUso = new List<CasoUso>()
                    };

                    if (subprocesoDto.CasosUso != null)
                    {
                        foreach (var casoUsoDto in subprocesoDto.CasosUso)
                        {
                            var casoUso = new CasoUso
                            {
                                Nombre = casoUsoDto.Nombre ?? "",
                                Descripcion = casoUsoDto.Descripcion,
                                ActorPrincipal = casoUsoDto.ActorPrincipal,
                                TipoCasoUso = casoUsoDto.TipoCasoUso,
                                Precondiciones = casoUsoDto.Precondiciones,
                                Postcondiciones = casoUsoDto.Postcondiciones,
                                CriteriosDeAceptacion = casoUsoDto.CriteriosDeAceptacion
                            };
                            subproceso.CasosUso.Add(casoUso);
                        }
                    }

                    proceso.Subprocesos.Add(subproceso);
                }
            }

            procesos.Add(proceso);
        }

        return procesos;
    }
}

// DTOs para deserialización
public class AnalisisResponse
{
    public List<ProcesoDto>? Procesos { get; set; }
}

public class ProcesoDto
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public List<SubprocesoDto>? Subprocesos { get; set; }
}

public class SubprocesoDto
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public List<CasoUsoDto>? CasosUso { get; set; }
}

public class CasoUsoDto
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? ActorPrincipal { get; set; }
    public short? TipoCasoUso { get; set; }
    public string? Precondiciones { get; set; }
    public string? Postcondiciones { get; set; }
    public string? CriteriosDeAceptacion { get; set; }
}
