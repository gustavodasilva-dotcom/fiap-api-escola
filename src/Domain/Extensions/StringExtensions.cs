using static BCrypt.Net.BCrypt;
using System.Text.RegularExpressions;
using Fiap.Api.Escola.Domain.Shared;

namespace Fiap.Api.Escola.Domain.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"\d")]
    private static partial Regex PeloUmNumeroRegex();

    [GeneratedRegex("[A-Z]")]
    private static partial Regex PeloUmCaracterMaiusculoRegex();

    [GeneratedRegex("[a-z]")]
    private static partial Regex PeloUmCaracterMinusculoRegex();

    public static Result<string, Error> HashSenhaForte(this string input)
    {
        List<string> messages = ["A senha deve possuir: \n"];

        if (input.Length < 8 || input.Length > 15)
        {
            messages.Add("Um tamanho entre 8 e 15 caracteres \n");
        }

        if (!PeloUmNumeroRegex().IsMatch(input))
        {
            messages.Add("Pelo menos, um número \n");
        }

        if (!PeloUmCaracterMaiusculoRegex().IsMatch(input))
        {
            messages.Add("Pelo menos, um caracter maiúsculo \n");
        }

        if (!PeloUmCaracterMinusculoRegex().IsMatch(input))
        {
            messages.Add("Pelo menos, um caracter minúsculo");
        }

        if (messages.Count == 1)
        {
            return HashPassword(input);
        }
        else
        {
            return new Error(
                "AlunoSenha.SenhaFraca",
                string.Join(string.Empty, [.. messages!]));
        }
    }
}
