using Microsoft.AspNetCore.DataProtection;

public class EncryptionService
{
    private readonly IDataProtector _protector;

    public EncryptionService(IDataProtectionProvider provider)
    {
        // Purpose string: keep it constant forever once you start storing encrypted values
        _protector = provider.CreateProtector("JetwaysAdmin.Password.v1");
    }

    public string Encrypt(string plainText)
    {
        return _protector.Protect(plainText);
    }

    public string Decrypt(string encryptedText)
    {
        return _protector.Unprotect(encryptedText);
    }
}
