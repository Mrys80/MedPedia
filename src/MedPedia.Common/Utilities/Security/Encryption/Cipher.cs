using System.Security.Cryptography;
using System.Text;

namespace MedPedia.Common.Utilities.Security.Encryption;

public class Cipher
{
    public static string DecryptAES(string encryptedValue, string key)
    {
        //DECRYPT FROM CRYPTOJS
        var encrypted = Convert.FromBase64String(encryptedValue);
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, key);
        return decriptedFromJavascript;
    }

    private static string DecryptStringFromBytes(byte[] cipherText, string key)
    {
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        var decryptor = GetDecryptor(key);

        // Create the streams used for decryption.
        try
        {
            using (var msDecrypt = new MemoryStream(cipherText))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception)
        {
            return "null";
        }

        return plaintext;
    }

    public static ICryptoTransform GetEncryptor(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var IV = Encoding.UTF8.GetBytes(key.Substring(0, 16));
        // Check arguments.
        if (keyBytes == null || keyBytes.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");


        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            //aesAlg.KeySize = keyBytes.Length * 8;
            aesAlg.Key = keyBytes;
            aesAlg.IV = IV;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.Mode = CipherMode.CBC;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            return encryptor;

            // Create the streams used for encryption.
        }
    }

    public static ICryptoTransform GetDecryptor(string key)
    {
        var keybytes = Encoding.UTF8.GetBytes(key);
        var iv = Encoding.UTF8.GetBytes(key.Substring(0, 16));
        // Check arguments.

        if (keybytes == null || keybytes.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        // Create an RijndaelManaged object
        // with the specified key and IV.
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;
            rijAlg.Key = keybytes;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
            return decryptor;
        }
    }

    public static string Encrypt(string plainText, string key)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return null;
        }

        if (string.IsNullOrEmpty(key))
        {
            key = String.Empty;
        }

        // Get the bytes of the string
        var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
        //var keyBytes = Encoding.UTF8.GetBytes(key);
        var bytesEncrypted = EncryptStringToBytes_Aes(plainText, key);
        return Convert.ToBase64String(bytesEncrypted);
    }


    public static byte[] EncryptStringToBytes_Aes(string plainText, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var IV = Encoding.UTF8.GetBytes(key.Substring(0, 16));
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (keyBytes == null || keyBytes.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.


        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = GetEncryptor(key);

        // Create the streams used for encryption.
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }

                encrypted = msEncrypt.ToArray();
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }
}