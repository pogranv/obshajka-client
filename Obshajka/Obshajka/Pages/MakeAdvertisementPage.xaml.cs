using Microsoft.Maui.Platform;
using Obshajka.Models;
using Obshajka.ViewModels;

namespace Obshajka.Pages;

public partial class MakeAdvertisementPage : ContentPage
{
	public MakeAdvertisementPage()
	{
		InitializeComponent();
	}

    private AdvertisementsViewModel adverts;

    public MakeAdvertisementPage(AdvertisementsViewModel advertisements)
    {
        adverts = advertisements;
        InitializeComponent();
    }

    private async void DownloadImageBtn_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image Please",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
            return;

        var stream = await result.OpenReadAsync();

        advertImage.Source = ImageSource.FromStream(() => stream);
    }

    private bool IsCorrectPrice(string? price)
    {
        if (string.IsNullOrEmpty(price))
        {
            return true;
        }
        int intPrice;
        if (int.TryParse(price, out intPrice) && intPrice >= 0)
        {
            return true;
        }
        return false;
    }

    private int GetPriceOrNul(string? price)
    {
        if (string.IsNullOrEmpty(price))
        {
            return 0;
        }
        int intPrice;
        if (int.TryParse(price, out intPrice) && intPrice >= 0)
        {
            return intPrice;
        }
        return 0;
    }
    private bool CheckCorrectAllFields()
    {
        bool isCorrectFlag = true;
        if (string.IsNullOrEmpty(titleEntry.Text))
        {
            titleEntry.PlaceholderColor = Colors.LightPink;
            titleBorder.BackgroundColor = Colors.LightPink;
            isCorrectFlag = false;
        }
        if (dormitoryPicker.SelectedItem == null)
        {
            dormitoryPicker.Background = Colors.LightPink;
            dormitoryBorder.Background = Colors.LightPink;
            isCorrectFlag = false;
        }
        if (!IsCorrectPrice(priceEntry.Text))
        {
            priceBorder.Background = Colors.LightPink;
            isCorrectFlag = false;
        }

        return isCorrectFlag;
    }

    private async void PublishBtn_Clicked(Object sender, EventArgs e)
    {
        if (!CheckCorrectAllFields())
        {
            await DisplayAlert("�� ��� ���� ���������", "��������� ������������ ���� � ��������� �� ������������ ������������ ����, ����� ������������ ����������.", "��");
            return;
        }
        var newAdvertisement = new Advertisement
        {
            CreatorId = UserSettings.UserSettings.UserId,
            CreatorName = UserSettings.UserSettings.UserName,
            Title = titleEntry.Text,
            Description = titleEntry.Text,
            DormitoryId = dormitoryPicker.SelectedIndex + 1,
            Price = GetPriceOrNul(priceEntry.Text),
            Image = advertImage.Source.ToString(),
            DateOfAddition = DateOnly.FromDateTime(DateTime.Now).ToString(),
        };
        var createdAdvertisement = Helpers.Helpers.PublishAndGetNewAdvert(newAdvertisement);
        adverts.AdvertisementsListViewElements.Add(createdAdvertisement);
        await Navigation.PopAsync();
        await DisplayAlert("�������� ����������", "���������� ������� �������!", "O�");
    }

    private void TitleEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        titleEntry.PlaceholderColor = Colors.LightGray;
        titleBorder.BackgroundColor = Colors.LightGray;
    }

    private void DormitoryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        dormitoryPicker.Background = Color.FromArgb("#E4E4E4");
        dormitoryBorder.Background = Color.FromArgb("#E4E4E4");
    }

    private void PriceEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        priceBorder.Background = Colors.Transparent;
    }
}