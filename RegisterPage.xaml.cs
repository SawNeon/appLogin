using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace appLogin
{
    public partial class RegisterPage : ContentPage
    {
        private readonly IDatabaseService _databaseService;

        // Construtor sem par�metros
        public RegisterPage()
        {
            InitializeComponent();
            _databaseService = DependencyService.Get<IDatabaseService>(); // Obt�m a inst�ncia do servi�o
        }

        // Construtor com par�metros
        public RegisterPage(IDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnAdicionarClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text; // Presumo que aqui voc� quer o nome do usu�rio
            int idade;

            if (!int.TryParse(senhaEntry.Text, out idade))
            {
                await DisplayAlert("Erro", "Idade deve ser um n�mero v�lido.", "OK");
                return;
            }

            // Verifica se j� existe um registro com o mesmo nome
            var dadosExistentes = await _databaseService.ObterDadosPorEmailAsync(email);

            if (dadosExistentes.Any())
            {
                await DisplayAlert("Erro", "J� existe um registro com esse email.", "OK");
                return;
            }

            var novoDado = new Tabela { Email = email, Senha = idade.ToString() }; // Corrigido aqui
            await _databaseService.InserirDadoAsync(novoDado);
            await DisplayAlert("Sucesso", "Dado inserido com sucesso!", "OK");

            emailEntry.Text = string.Empty;
            senhaEntry.Text = string.Empty;
            confirmarSenhaEntry.Text = string.Empty; // Limpando o campo de confirma��o de senha
        }

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            // Navega para a Login Page
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
