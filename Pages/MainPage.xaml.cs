namespace appLogin
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
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
