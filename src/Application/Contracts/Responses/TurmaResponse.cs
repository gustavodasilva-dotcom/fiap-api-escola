using System.Text.Json.Serialization;

namespace Fiap.Api.Escola.Application.Contracts.Responses;

public class TurmaResponse(
    int id,
    int curso_id,
    string turma,
    int ano)
{
    public TurmaResponse()
        : this(
              id: 0,
              curso_id: 0,
              turma: string.Empty,
              ano: DateTime.MinValue.Year)
    {
    }

    [JsonPropertyName("id")]
    public int Id { get; set; } = id;

    [JsonPropertyName("curso_id")]
    public int CursoID { get; set; } = curso_id;

    [JsonPropertyName("turma")]
    public string Nome { get; set; } = turma;

    [JsonPropertyName("ano")]
    public int Ano { get; set; } = ano;
}
