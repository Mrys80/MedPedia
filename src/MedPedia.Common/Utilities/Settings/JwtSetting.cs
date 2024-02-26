namespace MedPedia.Common.Utilities.Settings;

public class JwtSetting
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public string EncryptionKey { get; set; }
    public int ExpirationMinutes { get; set; }
}