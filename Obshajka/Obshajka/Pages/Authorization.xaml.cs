using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

namespace Obshajka.Pages;

public partial class Authorization : ContentPage
{
    private readonly string _domainHse = "edu.hse.ru";
    private readonly string _hide_eye_image = "hide_eye.png";
    private readonly string _view_eye_image = "view_eye.png";

    public Authorization()
    {
        InitializeComponent();
    }
    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (!IsCorrectEmailAndPassword(EntryLogInEmail.Text, EntryLogInPassword.Text))
        {
            return;
        }
        var enteredEmail = EntryLogInEmail.Text.Trim();
        var enteredPassword = EntryLogInPassword.Text.Trim();

        var client = new ObshajkaClient();
        try
        {
            UserSettings.UserSettings.UserId = await client.AuthorizeUser(enteredEmail, enteredPassword);
        }
        catch (FailAuthorizeException ex)
        {
            await DisplayAlert("Не удалось войти", ex.Message, "Ок");
            return;
        }

        await Shell.Current.GoToAsync("//Bar");
    }

    private bool IsCorrectEmailAndPassword(string email, string password)
    {
        bool isCorrectFlag = true;
        if (string.IsNullOrEmpty(password) || password.Trim().Length < 4)
        {
            isCorrectFlag = false;
            IncorrectPasswordLabel.IsVisible = true;
            LogInPasswordFrame.BorderColor = Colors.LightPink;
        }
        if (!IsCorrectEmail(email))
        {
            isCorrectFlag = false;
            IncorrectEmailLabel.IsVisible = true;
            LogInEmailFrame.BorderColor = Colors.LightPink;
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
        return loginAndDomain.Length == 2 && loginAndDomain[0].Length >= 4 && loginAndDomain[0].Length <= 20 && loginAndDomain[1] == _domainHse;
    }

    private void EntryLogInEmail_TextChanged(object sender, TextChangedEventArgs e)
    {
        IncorrectEmailLabel.IsVisible = false;
        LogInEmailFrame.BorderColor = Colors.LightGreen;
    }

    private void EntryLogInPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        IncorrectPasswordLabel.IsVisible = false;
        LogInPasswordFrame.BorderColor = Colors.LightGreen;
    }

    private void LogInPasswordEye_Clicked(object sender, EventArgs e)
    {
        if (EntryLogInPassword.IsPassword)
        {
            EntryLogInPassword.IsPassword = false;
            LogInPasswordEye.Source = _hide_eye_image;
        }
        else
        {
            EntryLogInPassword.IsPassword = true;
            LogInPasswordEye.Source = _view_eye_image;
        }
    }

    private async void SignUpCLickedLabel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Registration");
    }
}