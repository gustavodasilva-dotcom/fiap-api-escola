namespace Fiap.Api.Escola.Application.Contracts.Requests;

public class UpdateTurmaRequest(
    int cursoId,
    string turma,
    int ano)
{
    public UpdateTurmaRequest()
        : this(
              cursoId: 0,
              turma: string.Empty,
              ano: DateTime.Now.Year)
    {
    }

    public int CursoId { get; set; } = cursoId;

    public string Turma { get; set; } = turma;

    public int Ano { get; set; } = ano;
}
