using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appLogin
{
    public partial class AddPage : ContentPage
    {
        private readonly IDatabaseServiceUser _databaseService;
        private User _selectedUser;

        public AddPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseServiceUser();
            LoadUsers();
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntryAdd.Text) ||
                string.IsNullOrWhiteSpace(CPFEntryAdd.Text) ||
                string.IsNullOrWhiteSpace(TellEntryAdd.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (!long.TryParse(CPFEntryAdd.Text, out long cpf))
            {
                await DisplayAlert("Error", "Invalid CPF. Please enter a valid number.", "OK");
                return;
            }

            var name = NameEntryAdd.Text;
            var phone = TellEntryAdd.Text;

            var user = new User { CPF = cpf, Name = name, Phone = phone };
            await _databaseService.InserirDadoAsync(user);

            NameEntryAdd.Text = string.Empty;
            CPFEntryAdd.Text = string.Empty;
            TellEntryAdd.Text = string.Empty;

            await DisplayAlert("Success", "User added successfully!", "OK");
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var users = await _databaseService.ObterTodosDadosAsync();
            UsersListView.ItemsSource = users;
        }

        private void OnUserTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is User user)
            {
                if (_selectedUser == user)
                {
                    UserDetailsLayout.IsVisible = !UserDetailsLayout.IsVisible;
                }
                else
                {
                    _selectedUser = user;
                    UserDetailsLayout.IsVisible = true;
                    NameLabel.Text = user.Name;
                    PhoneLabel.Text = user.Phone;
                    CPFLabel.Text = user.CPF.ToString();
                }
            }
        }

        private async void OnNavegationGoBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///HomePage");
        }
    }
}
