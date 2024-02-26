namespace MedPedia.Common.Utilities.Security.Encryption;

public class EncrDecrAes
{
    public static string Encrypt(string result, string key)
    {
        if (string.IsNullOrEmpty(result)) return result;
        result = Cipher.Encrypt(result, key);
        return result;
    }

    public static string Decrypt(string encrypted, string key)
    {
        try
        {
            if (string.IsNullOrEmpty(encrypted))
                return encrypted;

            return Cipher.DecryptAES(encrypted, key);
        }
        catch
        {
            return null;
        }
    }
}