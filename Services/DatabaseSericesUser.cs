using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace appLogin
{
    public class DatabaseServiceUser : IDatabaseServiceUser
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseServiceUser()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public async Task<int> InserirDadoAsync(User user)
        {
            return await _database.InsertAsync(user);
        }

        public async Task<List<User>> ObterTodosDadosAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        public async Task<User> ObterDadoPorIdAsync(int cpf)
        {
            return await _database.Table<User>().Where(u => u.CPF == cpf).FirstOrDefaultAsync();
        }

        public async Task<int> AtualizarDadoAsync(User user)
        {
            return await _database.UpdateAsync(user);
        }

        public async Task<int> DeletarDadoAsync(User user)
        {
            return await _database.DeleteAsync(user);
        }

        public async Task<List<User>> ObterDadosPorNomeAsync(string name)
        {
            return await _database.Table<User>().Where(u => u.Name == name).ToListAsync();
        }

        public async Task<bool> AtualizarTelefoneAsync(string cpf, string phone)
        {
            var users = await _database.Table<User>().Where(u => u.CPF.ToString() == cpf).ToListAsync();
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    user.Phone = phone;
                    await _database.UpdateAsync(user);
                }
                return true;
            }
            return false;
        }
    }
}
