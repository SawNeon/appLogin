using Microsoft.Maui.Controls;

namespace appLogin
{
    public partial class loginPage : ContentPage
    {
        public loginPage()
        {
            InitializeComponent();
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

            var databaseService = new DatabaseService();
            bool sucesso = await databaseService.LoginAsync(email, senha);

            if (sucesso)
            {
                await Shell.Current.GoToAsync("///HomePage");
            }
            else
            {
                await DisplayAlert("Erro", "Email ou senha incorretos.", "OK");
            }
        }

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}