using Microsoft.Maui.Controls;

namespace appLogin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Registra o serviço de banco de dados
            DependencyService.Register<IDatabaseService, DatabaseService>();

            MainPage = new AppShell();
        }
    }
}
