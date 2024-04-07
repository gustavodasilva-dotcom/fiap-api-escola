using System.Text.Json.Serialization;

namespace Fiap.Api.Escola.Application.Contracts.Responses;

public class AlunoResponse(
    int id,
    string nome,
    string usuario)
{
    public AlunoResponse()
        : this(
              id: 0,
              nome: string.Empty,
              usuario: string.Empty)
    {
    }

    [JsonPropertyName("id")]
    public int Id { get; set; } = id;

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = nome;

    [JsonPropertyName("usuario")]
    public string Usuario { get; set; } = usuario;
}
