using System.Security.Cryptography;
using System.Text;

namespace appLogin
{
    public static class PasswordHelper
    {
        public static string CriptografarSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var senhaBytes = Encoding.UTF8.GetBytes(senha);
                var hashBytes = sha256.ComputeHash(senhaBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
