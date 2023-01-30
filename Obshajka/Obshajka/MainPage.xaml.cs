namespace Obshajka;
using Obshajka.Pages;

public partial class MainPage : ContentPage
{
	int count = 0;

    public Command LoginCommand { get; }

    public MainPage()
	{
		InitializeComponent();
        //LoginCommand = new Command(LoginButton_Clicked);
        btnGoto.Clicked += async (s, e) => await Shell.Current.GoToAsync("//Goto");
    }

    public async void LoginButton_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"//{nameof(Goto)}");
    }
}

