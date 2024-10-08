﻿using appLogin;
using SQLite;

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
        return await _database.Table<Tabela>()
                              .Where(t => t.Email == email)
                              .ToListAsync();
    }

    public async Task<List<Tabela>> ObterTodosDadosAsync()
    {
        return await _database.Table<Tabela>().ToListAsync();
    }

    public async Task<bool> LoginAsync(string email, string senha)
    {
        var user = await _database.Table<Tabela>()
                                  .Where(t => t.Email == email && t.Senha == senha)
                                  .FirstOrDefaultAsync();
        return user != null;
    }

    public async Task<bool> AtualizarSenhaAsync(string email, string novaSenha)
    {
        var user = await _database.Table<Tabela>()
                                  .Where(t => t.Email == email)
                                  .FirstOrDefaultAsync();
        if (user == null)
        {
            return false;
        }

        user.Senha = novaSenha;
        await _database.UpdateAsync(user);
        return true;
    }

    public async Task<IEnumerable<Tabela>> ObterDadosPorEmailRecuperarAsync(string email)
    {
        return await _database.Table<Tabela>()
                              .Where(t => t.Email == email)
                              .ToListAsync();
    }
}
