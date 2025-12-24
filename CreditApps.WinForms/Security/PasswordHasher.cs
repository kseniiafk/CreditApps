using System.Security.Cryptography;
using System.Text;

namespace CreditApps.WinForms.Security;

public static class PasswordHasher
{
    public static string Sha256Hex(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);

        var sb = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
            sb.Append(b.ToString("X2")); // ВАЖНО: X2 = ВЕРХНИЙ РЕГИСТР

        return sb.ToString();
    }
}
