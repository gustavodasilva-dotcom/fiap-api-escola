using System.Text.Json.Serialization;

namespace Fiap.Api.Escola.Application.Contracts.Responses;

public class AlunoResponse(
    int id,
    string nome,
    string usuario,
    string senha)
{
    public AlunoResponse()
        : this(
              id: 0,
              nome: string.Empty,
              usuario: string.Empty,
              senha: string.Empty)
    {
    }

    [JsonPropertyName("id")]
    public int Id { get; set; } = id;

    [JsonPropertyName("nome")]
    public string Nome { get; set; } = nome;

    [JsonPropertyName("usuario")]
    public string Usuario { get; set; } = usuario;

    [JsonPropertyName("senha")]
    public string Senha { get; set; } = senha;
}
