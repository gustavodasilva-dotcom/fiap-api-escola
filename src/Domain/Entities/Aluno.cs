using Fiap.Api.Escola.Domain.Primitives;
using Fiap.Api.Escola.Domain.Shared;
using Fiap.Api.Escola.Domain.Extensions;

namespace Fiap.Api.Escola.Domain.Entities;

public class Aluno : Entity
{
    private Aluno(
        int id,
        string nome,
        string usuario,
        string senha) : base(id)
    {
        Nome = nome;
        Usuario = usuario;
        Senha = senha;
    }

    private Aluno(
        string nome,
        string usuario,
        string senha) : base()
    {
        Nome = nome;
        Usuario = usuario;
        Senha = senha;
    }

    public string Nome { get; set; }

    public string Usuario { get; set; }

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

        var aluno = new Aluno(
            nome.Trim(),
            usuario.Trim(),
            senhaForteResult.Value!);

        return aluno;
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

        var aluno = new Aluno(
            id,
            nome.Trim(),
            usuario.Trim(),
            senhaForteResult.Value!);

        return aluno;
    }

    public override string ToInsertQuery()
    {
        return $@"
            INSERT INTO [dbo].[aluno]
                ([nome]
                ,[usuario]
                ,[senha])
            VALUES
                ('{Nome}'
                ,'{Usuario}'
                ,'{Senha}');";
    }

    public override string ToUpdateQuery()
    {
        return $@"
            UPDATE [dbo].[aluno]
            SET
                 [nome] = '{Nome}'
                ,[usuario] = '{Usuario}'
                ,[senha] = '{Senha}'
            WHERE [id] = {Id};";
    }

    public override string ToDeleteQuery()
    {
        return $@"DELETE FROM [dbo].[aluno] WHERE [id] = {Id};";
    }
}
