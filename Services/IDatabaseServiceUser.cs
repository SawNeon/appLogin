using System.Collections.Generic;
using System.Threading.Tasks;

namespace appLogin
{
    public interface IDatabaseServiceUser
    {
        Task<int> InserirDadoAsync(User user);
        Task<List<User>> ObterTodosDadosAsync();
        Task<User> ObterDadoPorIdAsync(int cpf);
        Task<int> AtualizarDadoAsync(User user);
        Task<int> DeletarDadoAsync(User user);
        Task<List<User>> ObterDadosPorNomeAsync(string name);
        Task<bool> AtualizarTelefoneAsync(string cpf, string phone); 
    }
}
