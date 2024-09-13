namespace appLogin
{
    public partial class loginPage : ContentPage
    {
        private IDatabaseService _databaseService;

        // Construtor padr�o
        public loginPage()
        {
            InitializeComponent();
            // Inst�ncia padr�o, voc� pode injetar o servi�o depois
            _databaseService = DependencyService.Get<IDatabaseService>();
        }

        // Construtor com inje��o de depend�ncia
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
