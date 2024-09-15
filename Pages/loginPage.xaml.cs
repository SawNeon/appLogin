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

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//MainPage");
            return true;  // Impede o comportamento padr�o do bot�o de voltar
        }

        // Construtor com inje��o de depend�ncia
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
                await DisplayAlert("Erro", "O email n�o pode estar vazio.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Erro", "A senha n�o pode estar vazia.", "OK");
                return;
            }

            // Obter um �nico usu�rio pelo email
            var usuarios = await _databaseService.ObterDadosPorEmailAsync(email);
            var usuario = usuarios.FirstOrDefault(); // Pega o primeiro (ou �nico) usu�rio da lista

            if (usuario == null || !usuario.VerificarSenha(senha))
            {
                await DisplayAlert("Erro", "Email ou senha incorretos.", "OK");
                return;
            }

            // Navegar para a pr�xima p�gina
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
