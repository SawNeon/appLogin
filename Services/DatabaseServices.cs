using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace appLogin
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");

            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Tabela>().Wait();
        }

        public Task<int> InserirDadoAsync(Tabela tabela)
        {
            return _database.InsertAsync(tabela);
        }

        public Task<List<Tabela>> ObterDadosAsync()
        {
            return _database.Table<Tabela>().ToListAsync();
        }

        public Task<Tabela> ObterDadoAsync(int id)
        {
            return _database.Table<Tabela>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> AtualizarDadoAsync(Tabela tabela)
        {
            return _database.UpdateAsync(tabela);
        }

        public Task<int> DeletarDadoAsync(Tabela tabela)
        {
            return _database.DeleteAsync(tabela);
        }

        public async Task<List<Tabela>> ObterDadosPorEmailAsync(string email)
        {
            // Garante que a query está correta
            return await _database.Table<Tabela>()
                                  .Where(t => t.Email == email)
                                  .ToListAsync();
        }
        public async Task<bool> LoginAsync(string email, string senha)
        {
        
            var user = await _database.Table<Tabela>().Where(t => t.Email == email && t.Senha == senha).FirstOrDefaultAsync();

            return user != null;
        }
    }
}
