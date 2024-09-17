using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace appLogin
{
    public partial class PersonListPage : ContentPage
    {
        private readonly IDatabaseServiceUser _databaseService;
        private User _selectedUser;

        public PersonListPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseServiceUser();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//HomePage");
            return true;  // Impede o comportamento padrão do botão de voltar
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _databaseService.ObterTodosDadosAsync();
                UsersListView.ItemsSource = new ObservableCollection<User>(users);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao carregar os dados. Tente novamente.", "OK");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private void OnUserTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is User user)
            {
                _selectedUser = user;
                UserDetailsLayout.IsVisible = true;
                NameLabel.Text = user.Name;
                PhoneLabel.Text = user.Phone;
                CPFLabel.Text = user.CPF.ToString();
            }
        }
    }
}
