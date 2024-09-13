namespace appLogin
{
    public partial class loginPage : ContentPage
    {
        private IDatabaseService _databaseService;

        // Construtor padrão
        public loginPage()
        {
            InitializeComponent();
            // Instância padrão, você pode injetar o serviço depois
            _databaseService = DependencyService.Get<IDatabaseService>();
        }

        // Construtor com injeção de dependência
        public loginPage(IDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = emailEntry.Text;
            var senha = senhaEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
                return;
            }

            bool sucesso = await _databaseService.LoginAsync(email, senha);

            if (sucesso)
            {
                await Shell.Current.GoToAsync("///HomePage");
            }
            else
            {
                await DisplayAlert("Erro", "Email ou senha incorretos.", "OK");
            }

            emailEntry.Text = string.Empty;
            senhaEntry.Text = string.Empty;
        }

        private async void OnRecuperarSenhaPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///RecoverPage");

        }

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
