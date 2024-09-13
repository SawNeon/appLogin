using SQLite;

namespace appLogin
{
    public class User
    {
        [PrimaryKey, NotNull]
        public long CPF { get; set; }
        [MaxLength(100), NotNull]
        public string Name { get; set; }
        [MaxLength(100), NotNull]
        public string Phone { get; set; } 
    }
}
