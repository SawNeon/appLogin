using Microsoft.Maui.Controls;

namespace appLogin
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            // Exibe um alerta perguntando se o usu�rio deseja sair do app
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await DisplayAlert("Sair", "Deseja voltar para tela de Login?", "Sim", "N�o");
                if (result)
                {
                    Shell.Current.GoToAsync("//loginPage");
                }
            });

            // Retorna true para impedir o comportamento padr�o de minimizar o app
            return true;
        }

        private async void OnAddPersonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AddPage");
        }

        private async void OnShowPersonListClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//PersonListPage");
        }
    }
}
