using System.Globalization;

namespace Obshajka.Pages;

public partial class ConfirmVerificationCodePage : ContentPage
{
	public ConfirmVerificationCodePage()
	{
		InitializeComponent();
	}


    public async void EnterApplication_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(regCodeEntry.Text) || regCodeEntry.Text.Length != 4 || !IsNumber(regCodeEntry.Text)) {
            incorrectCodeLabel.IsVisible = true;
            incorrectCodeInfoLabel.IsVisible = true;
            incorrectCodeInfoLabel2.IsVisible = true;
            return;
        }
        Helpers.Helpers.ConfirmVerificationCode(regCodeEntry.Text);
        await Shell.Current.GoToAsync("//Bar");
    }

    //private bool IsCorrentEnteredCode(string? code)
    //{
    //    return string.IsNullOrEmpty(code) && code.Length == 4 && IsNumber(code);
    //}

    private bool IsNumber(string code)
    {
        foreach (var digit in code) {
            if (!char.IsDigit(digit))
            {
                return false;
            }
        }
        return true;
    }

    private void RegCodeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        incorrectCodeLabel.IsVisible = false;
        incorrectCodeInfoLabel.IsVisible = false;
        incorrectCodeInfoLabel2.IsVisible = false;
    }

    //private void SendCodeAgain_Tapped(object sender, EventArgs e)
    //{

    //}
}