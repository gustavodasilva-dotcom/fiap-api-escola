using Fiap.Api.Escola.Domain.Shared;
using Fiap.Api.Escola.Domain.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Escola.Domain.Entities;

[Table("aluno")]
public class Aluno
{
    private Aluno(
        int id,
        string nome,
        string usuario,
        string senha)
    {
        Id = id;
        Nome = nome;
        Usuario = usuario;
        Senha = senha;
    }

    private Aluno(
        string nome,
        string usuario,
        string senha)
    {
        Nome = nome;
        Usuario = usuario;
        Senha = senha;
    }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; }

    [Column("usuario")]
    public string Usuario { get; set; }

    [Column("senha")]
    public string Senha { get; set; }

    public static Result<Aluno, Error> Create(
        string nome,
        string usuario,
        string senha)
    {
        var senhaForteResult = senha.HashSenhaForte();

        if (senhaForteResult.IsFailure)
        {
            return senhaForteResult.Error!;
        }

        var novoAluno = new Aluno(
            nome.Trim(),
            usuario.Trim(),
            senhaForteResult.Value!);

        return novoAluno;
    }

    public static Result<Aluno, Error> Update(
        int id,
        string nome,
        string usuario,
        string senha)
    {
        var senhaForteResult = senha.HashSenhaForte();

        if (senhaForteResult.IsFailure)
        {
            return senhaForteResult.Error!;
        }

        var alunoAtualizado = new Aluno(
            id,
            nome.Trim(),
            usuario.Trim(),
            senhaForteResult.Value!);

        return alunoAtualizado;
    }
}
