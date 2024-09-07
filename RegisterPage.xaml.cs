namespace appLogin;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void OnNavegationGoBack(object sender, EventArgs e)
    {
        // Navega para a Login Page
        await Shell.Current.GoToAsync("///MainPage");
    }
}