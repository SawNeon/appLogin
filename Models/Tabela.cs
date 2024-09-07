using SQLite;
using System.ComponentModel;

namespace appLogin
{
    public class Tabela
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100), NotNull ]
        public string Email { get; set; }
        [MaxLength(100), NotNull, PasswordPropertyText]
        public string Senha { get; set; }
    }
}
