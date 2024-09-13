using System.Text.RegularExpressions;

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
            string email = emailEntry.Text;
            string senha = senhaEntry.Text; 
            string confirmarSenha = confirmarSenhaEntry.Text; 

            // Verifica se os campos est�o preenchidos
            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Erro", "O email n�o pode estar vazio.", "OK");
                return;
            }

            if (!IsValidEmail(email))
            {
                await DisplayAlert("Erro", "O email fornecido n�o � v�lido.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Erro", "A senha n�o pode estar vazia.", "OK");
                return;
            }

            if (senha != confirmarSenha)
            {
                await DisplayAlert("Erro", "As senhas n�o coincidem.", "OK");
                return;
            }

            if(senha.Length < 12)
            {
                await DisplayAlert("Erro", "Sua senha deve conter no m�nimo 12 caracteres", "OK");
                return;
            }

            // Verifica se j� existe um registro com o mesmo email
            var dadosExistentes = await _databaseService.ObterDadosPorEmailAsync(email);

            // Protege contra o retorno nulo
            if (dadosExistentes != null && dadosExistentes.Any())
            {
                await DisplayAlert("Erro", "J� existe um registro com esse email.", "OK");
                return;
            }

            // Cria o novo registro
            var novoDado = new Tabela { Email = email, Senha = senha }; // Usa 'senha' corretamente
            await _databaseService.InserirDadoAsync(novoDado);
            await DisplayAlert("Sucesso", "Dado inserido com sucesso!", "OK");

            // Limpa os campos de entrada
            emailEntry.Text = string.Empty;
            senhaEntry.Text = string.Empty;
            confirmarSenhaEntry.Text = string.Empty; // Limpando o campo de confirma��o de senha
        }

        private bool IsValidEmail(string email)
        {
            // Express�o regular para validar o formato do email
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
                // Navega para a Login Page
                await Shell.Current.GoToAsync("///MainPage");
        }
    }
}

