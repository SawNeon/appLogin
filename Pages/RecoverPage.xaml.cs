namespace appLogin
{
    public partial class RecoverPage : ContentPage
    {
        private readonly IDatabaseService _databaseService;

        
        public RecoverPage()
        {
            InitializeComponent();
            _databaseService = DependencyService.Get<IDatabaseService>();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//MainPage");
            return true;  // Impede o comportamento padrão do botão de voltar
        }

        private async void OnNewPassword(object sender, EventArgs e)
        {
            string email = emailRecover.Text?.Trim();
            string novaSenha = NewPasswordEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Erro", "Por favor, insira seu e-mail.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(novaSenha))
            {
                await DisplayAlert("Erro", "Por favor, insira a nova senha.", "OK");
                return;
            }

            if (novaSenha.Length < 12)
            {
                await DisplayAlert("Erro", "A nova senha deve conter no mínimo 12 caracteres.", "OK");
                return;
            }

           
            var dadosExistentes = await _databaseService.ObterDadosPorEmailRecuperarAsync(email);

            if (dadosExistentes == null || !dadosExistentes.Any())
            {
                await DisplayAlert("Erro", "Nenhum registro encontrado com esse e-mail.", "OK");
                return;
            }

           
            bool sucesso = await _databaseService.AtualizarSenhaAsync(email, novaSenha);

            if (sucesso)
            {
                await DisplayAlert("Sucesso", "Sua senha foi atualizada com sucesso.", "OK");
                await Shell.Current.GoToAsync("///MainPage"); 
            }
            else
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao atualizar a senha.", "OK");
            }
        }
        
        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///loginPage"); 
        }
    }
}
