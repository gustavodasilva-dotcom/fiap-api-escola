using System.Text.RegularExpressions;
using Fiap.Api.Escola.Domain.Shared;
using System.Security.Cryptography;
using System.Text;

namespace Fiap.Api.Escola.Domain.Extensions;

public static partial class StringExtensions
{
    private static string _secretKey => "b14ca5898a4e4133bbce2ea2315a1916";

    [GeneratedRegex(@"\d")]
    private static partial Regex PeloUmNumeroRegex();

    [GeneratedRegex("[A-Z]")]
    private static partial Regex PeloUmCaracterMaiusculoRegex();

    [GeneratedRegex("[a-z]")]
    private static partial Regex PeloUmCaracterMinusculoRegex();

    public static string EncryptString(this string text)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_secretKey);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(text);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string DecryptString(this string text)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(text);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_secretKey);
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    public static Result<string, Error> HashSenhaForte(this string text)
    {
        List<string> messages = ["A senha deve possuir: \n"];

        if (text.Length < 8 || text.Length > 15)
        {
            messages.Add("Um tamanho entre 8 e 15 caracteres \n");
        }

        if (!PeloUmNumeroRegex().IsMatch(text))
        {
            messages.Add("Pelo menos, um número \n");
        }

        if (!PeloUmCaracterMaiusculoRegex().IsMatch(text))
        {
            messages.Add("Pelo menos, um caracter maiúsculo \n");
        }

        if (!PeloUmCaracterMinusculoRegex().IsMatch(text))
        {
            messages.Add("Pelo menos, um caracter minúsculo");
        }

        if (messages.Count == 1)
        {
            return text.EncryptString();
        }
        else
        {
            return new Error(
                "AlunoSenha.SenhaFraca",
                string.Join(string.Empty, [.. messages!]));
        }
    }
}
