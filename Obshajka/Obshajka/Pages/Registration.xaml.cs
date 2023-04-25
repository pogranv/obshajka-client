using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

namespace Obshajka.Pages;

public partial class Registration : ContentPage
{
    private readonly string _domainHse = "edu.hse.ru";
    private readonly string _hide_eye_image = "hide_eye.png";
    private readonly string _view_eye_image = "view_eye.png";
    public Registration()
    {
        InitializeComponent();
        Routing.RegisterRoute("ConfirmVerificationCode", typeof(ConfirmVerificationCode));
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

        var client = new ObshajkaClient();

        try
        {
            client.RegisterUser(enteredName, enteredEmail, enteredPassword);
        } 
        catch (FailRegisterUserException ex)
        {
            await DisplayAlert("Ошибка регистрации", ex.Message, "Ок");
            return;
        }

        await Navigation.PushAsync(new ConfirmVerificationCode(enteredEmail));
    }

    // TODO: вынести в utils

    private bool IsCorrectEntryData(string name, string email, string password)
    {
        bool isCorrectFlag = true;
        if (string.IsNullOrEmpty(password) || password.Trim().Length < 4)
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
        if (string.IsNullOrEmpty(name) || name.Trim().Length <= 1)
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
        var loginAndDomain = email.Trim().Split('@');
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
            regPasswordEye.Source = _hide_eye_image;
        }
        else
        {
            regPasswordEntry.IsPassword = true;
            regPasswordEye.Source = _view_eye_image;
        }
    }
}