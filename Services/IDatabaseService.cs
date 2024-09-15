using appLogin;
public interface IDatabaseService
{
    Task<int> InserirDadoAsync(Tabela tabela); // Inserir novo registro
    Task<List<Tabela>> ObterDadosAsync(); // Obter todos os dados
    Task<Tabela> ObterDadoAsync(int id); // Obter um dado específico por ID (se necessário)
    Task<int> AtualizarDadoAsync(Tabela tabela); // Atualizar registro
    Task<int> DeletarDadoAsync(Tabela tabela); // Deletar registro
    Task<List<Tabela>> ObterDadosPorEmailAsync(string email); // Obter dados filtrando pelo email
    Task<List<Tabela>> ObterTodosDadosAsync(); // Listar todos os dados
    Task<bool> LoginAsync(string email, string senha); // Certifique-se de que este método está presente
    Task<IEnumerable<Tabela>> ObterDadosPorEmailRecuperarAsync(string email);
    Task<bool> AtualizarSenhaAsync(string email, string novaSenha);
}
