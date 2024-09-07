using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace appLogin
{
    public partial class RegisterPage : ContentPage
    {
        private readonly IDatabaseService _databaseService;

        // Construtor sem parâmetros
        public RegisterPage()
        {
            InitializeComponent();
            _databaseService = DependencyService.Get<IDatabaseService>(); // Obtém a instância do serviço
        }

        // Construtor com parâmetros
        public RegisterPage(IDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnAdicionarClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text; // Presumo que aqui você quer o nome do usuário
            int idade;

            if (!int.TryParse(senhaEntry.Text, out idade))
            {
                await DisplayAlert("Erro", "Idade deve ser um número válido.", "OK");
                return;
            }

            // Verifica se já existe um registro com o mesmo nome
            var dadosExistentes = await _databaseService.ObterDadosPorEmailAsync(email);

            if (dadosExistentes.Any())
            {
                await DisplayAlert("Erro", "Já existe um registro com esse email.", "OK");
                return;
            }

            var novoDado = new Tabela { Email = email, Senha = idade.ToString() }; // Corrigido aqui
            await _databaseService.InserirDadoAsync(novoDado);
            await DisplayAlert("Sucesso", "Dado inserido com sucesso!", "OK");

            emailEntry.Text = string.Empty;
            senhaEntry.Text = string.Empty;
            confirmarSenhaEntry.Text = string.Empty; // Limpando o campo de confirmação de senha
        }

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            // Navega para a Login Page
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
