using Microsoft.AspNetCore.Http;
using Microsoft.Maui.Platform;
using Obshajka.Models;
using Obshajka.ViewModels;
using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

namespace Obshajka.Pages;

public partial class MakeAdvertisementPage : ContentPage
{
	public MakeAdvertisementPage()
	{
		InitializeComponent();
	}

    // private readonly AdvertisementsViewModel _adverts;
    private string _imagePath;

    public MakeAdvertisementPage(AdvertisementsViewModel advertisements)
    {
        // _adverts = advertisements;
        InitializeComponent();
    }

    private Stream _imageStream = null;
    private async void DownloadImageBtn_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "����� ����������� ����������",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
            return;

        _imagePath = result.FullPath;
        _imageStream = await result.OpenReadAsync();
        advertImage.Source = ImageSource.FromStream(() => _imageStream);
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

        var publishingAdvertisement= new PublishingAdvertisement()
        {
            CreatorId = UserSettings.UserSettings.UserId,
            Title = titleEntry.Text,
            Description = titleEntry.Text,
            DormitoryId = dormitoryPicker.SelectedIndex + 1,
            Price = GetPriceOrNul(priceEntry.Text),
        };

        var client = new ObshajkaClient();

        try
        {
            // ATTENTION! ���� ������ ����������, ��� �� ������ ����
            client.PubslishNewAdvertisement(publishingAdvertisement, _imagePath);
        }
        catch(FailPublishAdvertisementException ex)
        {
            await DisplayAlert("������", ex.Message, "O�");
            return;
        }
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