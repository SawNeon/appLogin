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

            // Carregar todos os usuários no início (opcional)
            CarregarUsuarios();
        }

        // Função para carregar todos os usuários na CollectionView
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

            // Implementar a lógica de pesquisa aqui
            List<Tabela> resultados = await _databaseService.ObterDadosPorEmailAsync(searchText);

            // Exibe os resultados na CollectionView
            if (resultados.Any())
            {
                userList.ItemsSource = resultados; // Exibe os usuários encontrados
            }
            else
            {
                await DisplayAlert("Resultado", "Nenhum usuário encontrado.", "OK");
                userList.ItemsSource = null; // Limpa a lista se não houver resultados
            }
        }
    }
}
