using Microsoft.Maui.Controls;


namespace appLogin
{
    public partial class AddPage : ContentPage
    {
        private readonly IDatabaseServiceUser _databaseService;

        public AddPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseServiceUser();
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("//HomePage");
            return true;  // Impede o comportamento padrão do botão de voltar
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntryAdd.Text) ||
                string.IsNullOrWhiteSpace(CPFEntryAdd.Text) ||
                string.IsNullOrWhiteSpace(TellEntryAdd.Text))
            {
                await DisplayAlert("Erro", "Preencha todos os campos", "OK");
                return;
            }

            if (!long.TryParse(CPFEntryAdd.Text, out long cpf))
            {
                await DisplayAlert("Erro", "CPF inválido. Insira um número válido.", "OK");
                return;
            }

            var user = new User
            {
                CPF = cpf,
                Name = NameEntryAdd.Text,
                Phone = TellEntryAdd.Text
            };

            try
            {
                await _databaseService.InserirDadoAsync(user);
                await DisplayAlert("Sucesso", "Pessoa adicionada com sucesso!", "OK");

                NameEntryAdd.Text = string.Empty;
                CPFEntryAdd.Text = string.Empty;
                TellEntryAdd.Text = string.Empty;
            }
            catch (InvalidOperationException ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao adicionar a pessoa. Tente novamente.", "OK");
                Console.WriteLine($"Erro: {ex.Message}");
            }
        } 

       
    }
}
