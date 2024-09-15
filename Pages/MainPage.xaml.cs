namespace appLogin
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        protected override bool OnBackButtonPressed()
        {
            // Exibe um alerta perguntando se o usuário deseja sair do app
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await DisplayAlert("Sair", "Deseja sair do aplicativo?", "Sim", "Não");
                if (result)
                {
                    // Fecha o app
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });

            // Retorna true para impedir o comportamento padrão de minimizar o app
            return true;
        }

        private async void OnNavigateGoLoginPage(object sender, EventArgs e)
        {
            // Navega para a Login Page
            await Shell.Current.GoToAsync("///loginPage");
        }

        private async void OnNavigateGoRegisterPage(object sender, EventArgs e)
        {
            // Navega para a Login Page
            await Shell.Current.GoToAsync("///RegisterPage");
        }


    }

}
