using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

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
            LoadUsers();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//HomePage");
            return true;  // Impede o comportamento padrão do botão de voltar
        }

        private async void LoadUsers()
        {
            var users = await _databaseService.ObterTodosDadosAsync();
            UsersListView.ItemsSource = new ObservableCollection<User>(users);
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
