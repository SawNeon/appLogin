using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
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
            return true;  // Impede o comportamento padr�o do bot�o de voltar
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

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUser != null)
                {
                    // Atualiza os dados do usu�rio com os valores dos campos de entrada
                    _selectedUser.Name = NameEntry.Text;
                    _selectedUser.Phone = PhoneEntry.Text;
                    _selectedUser.CPF = long.Parse(CPFEntry.Text); // Certifique-se de que CPF � do tipo long

                    // Chama o servi�o para atualizar o usu�rio no banco de dados
                    await _databaseService.AtualizarDadoAsync(_selectedUser);

                    // Esconde o layout de detalhes e recarrega a lista de usu�rios
                    UserDetailsLayout.IsVisible = false;
                    await LoadUsers();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao salvar os dados. Tente novamente.", "OK");
                Console.WriteLine($"Erro ao salvar: {ex.Message}");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Esconde o layout de detalhes sem salvar as altera��es
            UserDetailsLayout.IsVisible = false;
        }

        private async void OnUserTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is User user)
            {
                _selectedUser = user;
                UserDetailsLayout.IsVisible = true;
                NameEntry.Text = user.Name;
                PhoneEntry.Text = user.Phone;
                CPFEntry.Text = user.CPF.ToString();
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            // Verifica se um usu�rio est� selecionado
            if (_selectedUser == null)
            {
                await DisplayAlert("Erro", "Nenhum usu�rio selecionado para deletar.", "OK");
                return;
            }

            // Confirma��o de exclus�o
            var result = await DisplayAlert("Confirmar", "Deseja realmente deletar este usu�rio?", "Sim", "N�o");

            if (result)
            {
                try
                {
                    // Chama o servi�o para deletar o usu�rio do banco de dados
                    await _databaseService.DeletarDadoAsync(_selectedUser);

                    // Limpa os campos e atualiza a lista de usu�rios
                    UserDetailsLayout.IsVisible = false;
                    _selectedUser = null;
                    await LoadUsers();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao deletar o usu�rio. Tente novamente.", "OK");
                    Console.WriteLine($"Erro ao deletar: {ex.Message}");
                }
            }
        }
    }
}
