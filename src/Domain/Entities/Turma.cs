using Fiap.Api.Escola.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.Escola.Domain.Entities;

[Table("turma")]
public class Turma
{
    private Turma(
        int id,
        int curso_id,
        string turma,
        int ano)
    {
        Id = id;
        CursoID = curso_id;
        Nome = turma;
        Ano = ano;
    }

    private Turma(
        int curso_id,
        string turma,
        int ano) : base()
    {
        CursoID = curso_id;
        Nome = turma;
        Ano = ano;
    }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("curso_id")]
    public int CursoID { get; set; }

    [Column("turma")]
    public string Nome { get; set; }

    [Column("ano")]
    public int Ano { get; set; }

    public static Result<Turma, Error> Create(
        int cursoId,
        string turma,
        int ano)
    {
        var novaTurma = new Turma(
            cursoId,
            turma,
            ano);

        return novaTurma;
    }

    public static Result<Turma, Error> Update(
        int id,
        int cursoId,
        string turma,
        int ano)
    {
        var turmaAtualizada = new Turma(
            id,
            cursoId,
            turma,
            ano);

        return turmaAtualizada;
    }
}
