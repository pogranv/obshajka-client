namespace Obshajka.Pages;

using Obshajka.Exceptions;

public partial class RegistrationPage : ContentPage
{

    private readonly string _domainHse = "edu.hse.ru";
    public RegistrationPage()
	{
		InitializeComponent();
        Routing.RegisterRoute("ConfirmVerificationCodePage", typeof(ConfirmVerificationCodePage));
    }

    private async void SendVerificationCode_Clicked(object sender, EventArgs e)
    {
        if (!IsCorrectEntryData(regNameEntry.Text, regEmailEntry.Text, regPasswordEntry.Text))
        {
            return;
        }
        var enteredName = regNameEntry.Text.Trim();
        var enteredEmail = regEmailEntry.Text.Trim();
        var enteredPassword = regPasswordEntry.Text.Trim();

        // TODO: обработать эксепшн, если не удалось авторизовться

        try
        {
            Helpers.Helpers.TryRegisterUser(enteredName, enteredEmail, enteredPassword);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка регистрации", ex.Message, "Ок");
            await Navigation.PopAsync();
            return;
        }
        await Navigation.PushAsync(new ConfirmVerificationCodePage(enteredEmail));
        // await Shell.Current.GoToAsync("ConfirmVerificationCodePage");
    }

    private bool IsCorrectEntryData(string name, string email, string password)
    {
        bool isCorrectFlag = true;
        if (string.IsNullOrEmpty(password) || password.Length <= 4)
        {
            isCorrectFlag = false;
            incorrectPasswordLabel.IsVisible = true;
            regPasswordFrame.BorderColor = Colors.LightPink;
        }
        if (!IsCorrectEmail(email))
        {
            isCorrectFlag = false;
            incorrectEmailLabel.IsVisible = true;
            regEmailFrame.BorderColor = Colors.LightPink;
        }
        if (string.IsNullOrEmpty(name) || name.Length <= 1)
        {
            isCorrectFlag = false;
            incorrectNameLabel.IsVisible = true;
            regNameFrame.BorderColor = Colors.LightPink;
        }
        return isCorrectFlag;
    }
    private bool IsCorrectEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        var loginAndDomain = email.Split('@');
        return loginAndDomain.Length == 2 && loginAndDomain[0].Length >= 4 && loginAndDomain[0].Length <= 20 && loginAndDomain[1].Equals(_domainHse);
    }

    private void RegEmailEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        incorrectEmailLabel.IsVisible = false;
        regEmailFrame.BorderColor = Colors.LightGreen;
    }

    private void RegPasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        incorrectPasswordLabel.IsVisible = false;
        regPasswordFrame.BorderColor = Colors.LightGreen;
    }

    private void LogInPasswordEye_Clicked(object sender, EventArgs e)
    {
        if (regPasswordEntry.IsPassword)
        {
            regPasswordEntry.IsPassword = false;
            regPasswordEye.Source = "hide_eye.png";
        }
        else
        {
            regPasswordEntry.IsPassword = true;
            regPasswordEye.Source = "view_eye.png";
        }
    }
}