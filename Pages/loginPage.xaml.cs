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

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//MainPage");
            return true;  // Impede o comportamento padrão do botão de voltar
        }

        // Construtor com injeção de dependência
        public loginPage(IDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string senha = senhaEntry.Text;

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

            // Obter um único usuário pelo email
            var usuarios = await _databaseService.ObterDadosPorEmailAsync(email);
            var usuario = usuarios.FirstOrDefault(); // Pega o primeiro (ou único) usuário da lista

            if (usuario == null || !usuario.VerificarSenha(senha))
            {
                await DisplayAlert("Erro", "Email ou senha incorretos.", "OK");
                return;
            }

            // Navegar para a próxima página
            await Shell.Current.GoToAsync("//HomePage");
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
