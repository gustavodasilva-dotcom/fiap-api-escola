namespace Fiap.Api.Escola.Application.Contracts.Requests;

public class UpdateAlunoRequest(
    string nome,
    string usuario,
    string senha)
{
    public UpdateAlunoRequest()
        : this(
              nome: string.Empty,
              usuario: string.Empty,
              senha: string.Empty)
    {
    }

    public string Nome { get; set; } = nome;

    public string Usuario { get; set; } = usuario;
    
    public string Senha { get; set; } = senha;
}
