using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
namespace JetwaysAdmin.WebAPI.Controllers
{
    public class EncryptionServiceController : Controller
    {
        private readonly IDataProtector _protector;

        public EncryptionServiceController(IDataProtectionProvider provider)
        {
            // Do NOT change this purpose string after saving encrypted passwords
            _protector = provider.CreateProtector("JetwaysAdmin.AdminPassword.v1");
        }

        public string Encrypt(string plainText)
            => _protector.Protect(plainText);

        public string Decrypt(string encryptedText)
            => _protector.Unprotect(encryptedText);
    }
}
