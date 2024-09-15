using System.Collections.Generic;
using System.Threading.Tasks;

namespace appLogin
{
    public interface IDatabaseServiceUser
    {
        Task<int> InserirDadoAsync(User user);
        Task<List<User>> ObterTodosDadosAsync();
        Task<User> ObterDadoPorIdAsync(long cpf);  // Ajustado para long
        Task<int> AtualizarDadoAsync(User user);
        Task<int> DeletarDadoAsync(User user);
        Task<List<User>> ObterDadosPorNomeAsync(string name);
        Task<bool> AtualizarTelefoneAsync(string cpf, string phone);
    }
}
