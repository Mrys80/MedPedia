using System.Security.Cryptography;
using System.Text;

namespace MedPedia.Common.Utilities.Security.Hashing;

public class HashingHelper
{
    public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using HMACSHA512 hmac = new();
        passwordSalt = Convert.ToBase64String(hmac.Key);
        passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private static byte[] CreatePasswordHash(string password, string salt)
    {
        var passwordSalt = Convert.FromBase64String(salt);
        using HMACSHA512 hmac = new(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash;
    }

    public static string CreatePasswordHashString(string password, string salt)
    {
        return Convert.ToBase64String(CreatePasswordHash(password, salt));
    }

    public static bool VerifyPasswordHash(string password, string hash, string salt)
    {
        var passwordHash = Convert.FromBase64String(hash);
        var computeHash = CreatePasswordHash(password, salt);

        return !computeHash.Where((t, i) => t != passwordHash[i]).Any();
    }
}