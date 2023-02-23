namespace Obshajka.Pages;

public partial class ConfirmVerificationCodePage : ContentPage
{
	public ConfirmVerificationCodePage()
	{
		InitializeComponent();
	}

    public async void EnterApplication_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Bar");
    }
}