using System.Text.Json.Serialization;

namespace Fiap.Api.Escola.Application.Contracts.Responses;

public class TurmaResponse(
    int id,
    int cursoId,
    string turma,
    int ano)
{
    public TurmaResponse()
        : this(
              id: 0,
              cursoId: 0,
              turma: string.Empty,
              ano: DateTime.MinValue.Year)
    {
    }

    [JsonPropertyName("id")]
    public int Id { get; set; } = id;

    [JsonPropertyName("cursoId")]
    public int CursoID { get; set; } = cursoId;

    [JsonPropertyName("turma")]
    public string Nome { get; set; } = turma;

    [JsonPropertyName("ano")]
    public int Ano { get; set; } = ano;
}
