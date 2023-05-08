using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

namespace Obshajka.Pages;

public partial class ConfirmVerificationCode : ContentPage
{
    private readonly string _enteredEmail;

    public ConfirmVerificationCode(string enteredEmail)
    {
        InitializeComponent();
        _enteredEmail = enteredEmail;
    }
    private async void EnterApplication_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(regCodeEntry.Text) || regCodeEntry.Text.Length != 4
            || !IsNumber(regCodeEntry.Text))
        {
            incorrectCodeLabel.IsVisible = true;
            incorrectCodeInfoLabel.IsVisible = true;
            incorrectCodeInfoLabel2.IsVisible = true;
            regCodeFrame.BorderColor = Colors.LightPink;
            return;
        }

        var client = new ObshajkaClient();
        regBtn.IsEnabled = false;
        try
        {
            UserSettings.UserSettings.UserId = await client.ConfirmVerificationCode(_enteredEmail, regCodeEntry.Text);
        }
        catch (FailConfirmVerificationCodeException ex)
        {
            await DisplayAlert("Ошибка регистрации", ex.Message, "Ок");
            return;
        }
        catch (NetworkUnavailableException ex)
        {
            await DisplayAlert("Сеть недоступна", ex.Message, "Ок");
            return;
        }
        finally
        {
            regBtn.IsEnabled = true;
        }

        await Shell.Current.GoToAsync("//Bar");
    }

    private bool IsNumber(string code)
    {
        foreach (var digit in code)
        {
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
        regCodeFrame.BorderColor = Colors.LightGreen;
    }
}