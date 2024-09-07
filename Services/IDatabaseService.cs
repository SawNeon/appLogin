using System.Collections.Generic;
using System.Threading.Tasks;

namespace appLogin
{
    public interface IDatabaseService
    {
        Task<int> InserirDadoAsync(Tabela tabela);
        Task<List<Tabela>> ObterDadosAsync();
        Task<Tabela> ObterDadoAsync(int id);
        Task<int> AtualizarDadoAsync(Tabela tabela);
        Task<int> DeletarDadoAsync(Tabela tabela);
        Task<List<Tabela>> ObterDadosPorEmailAsync(string email);
    }
}
