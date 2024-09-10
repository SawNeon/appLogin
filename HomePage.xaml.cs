using System;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appLogin
{
    public partial class HomePage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public HomePage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string searchText = search.Text?.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                await DisplayAlert("Erro", "Por favor, insira um texto para pesquisar.", "OK");
                return;
            }

            // Implementar a l�gica de pesquisa aqui
            List<Tabela> resultados = await _databaseService.ObterDadosPorEmailAsync(searchText);

            // Exemplo: Mostrar os resultados em um alerta (substitua com o seu c�digo de exibi��o)
            if (resultados.Any())
            {
                string resultadosStr = string.Join(", ", resultados.Select(r => r.Email));
                await DisplayAlert("Resultados", $"Usu�rios encontrados: {resultadosStr}", "OK");
            }
            else
            {
                await DisplayAlert("Resultado", "Nenhum usu�rio encontrado.", "OK");
            }
        }
    }
}
