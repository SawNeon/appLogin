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
            string email = emailEntry.Text; // Pega o email do campo de entrada
            string senha = senhaEntry.Text; // Pega a senha do campo de entrada

            // Verifica se os campos estão preenchidos
            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Erro", "O email não pode estar vazio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Erro", "A senha não pode estar vazia.", "OK");
                return;
            }

            // Verifica se já existe um registro com o mesmo email
            var dadosExistentes = await _databaseService.ObterDadosPorEmailAsync(email);

            // Protege contra o retorno nulo
            if (dadosExistentes != null && dadosExistentes.Any())
            {
                await DisplayAlert("Erro", "Já existe um registro com esse email.", "OK");
                return;
            }

            // Cria o novo registro
            var novoDado = new Tabela { Email = email, Senha = senha }; // Usa 'senha' corretamente
            await _databaseService.InserirDadoAsync(novoDado);
            await DisplayAlert("Sucesso", "Dado inserido com sucesso!", "OK");

            // Limpa os campos de entrada
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
