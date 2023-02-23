namespace Obshajka.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    public async void SendVerificationCode_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ConfirmVerificationCodePage");
    }
}