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

            // Carregar todos os usu�rios no in�cio (opcional)
            CarregarUsuarios();
        }

        // Fun��o para carregar todos os usu�rios na CollectionView
        private async void CarregarUsuarios()
        {
            var todosUsuarios = await _databaseService.ObterTodosDadosAsync();
            userList.ItemsSource = todosUsuarios;
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

            // Exibe os resultados na CollectionView
            if (resultados.Any())
            {
                userList.ItemsSource = resultados; // Exibe os usu�rios encontrados
            }
            else
            {
                await DisplayAlert("Resultado", "Nenhum usu�rio encontrado.", "OK");
                userList.ItemsSource = null; // Limpa a lista se n�o houver resultados
            }
        }
    }
}
