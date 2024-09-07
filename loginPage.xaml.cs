using Microsoft.Maui.Controls;

namespace appLogin
{
    public partial class loginPage : ContentPage
    {
        public loginPage()
        {
            InitializeComponent();
        }

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            // Navega para a Login Page
            await Shell.Current.GoToAsync("///MainPage");
        }

    }
}