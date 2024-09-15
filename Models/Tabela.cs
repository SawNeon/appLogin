using SQLite;
using System.Security.Cryptography;
using System.Text;

namespace appLogin
{
    public class Tabela
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string Senha { get; set; }

        // Criptografa a senha usando PasswordHelper
        public void EncryptPassword(string senha)
        {
            Senha = PasswordHelper.CriptografarSenha(senha);
        }

        // Método para verificar a senha
        public bool VerificarSenha(string senha)
        {
            return Senha == PasswordHelper.CriptografarSenha(senha);
        }
    }
}
