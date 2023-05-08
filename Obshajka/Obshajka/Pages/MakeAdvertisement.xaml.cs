using Obshajka.Models;
using Obshajka.ViewModels;

using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

namespace Obshajka.Pages;

public partial class MakeAdvertisementPage : ContentPage
{
    private string _imagePath;

    public MakeAdvertisementPage()
	{
		InitializeComponent();
	}

    public MakeAdvertisementPage(MyAdvertsViewModel advertisements)
    {
        InitializeComponent();
    }
    private async void DownloadImageBtn_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Выбор изображения объявления",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
        {
            return;
        }

        _imagePath = result.FullPath;
        var imageStream = await result.OpenReadAsync();
        advertImage.Source = ImageSource.FromStream(() => imageStream);
    }

    private bool IsCorrectPrice(string? price)
    {
        if (string.IsNullOrEmpty(price))
        {
            return true;
        }
        int intPrice;
        if (price.Length <= 9 && int.TryParse(price, out intPrice) && intPrice >= 0)
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
            await DisplayAlert("Не все поля заполнены", "Заполните обязательные поля и проверьте на корректность выставленную цену, чтобы опубликовать объявление.", "Ок");
            return;
        }

        var publishingAdvertisement = new PublishingAdvertisement()
        {
            CreatorId = UserSettings.UserSettings.UserId,
            Title = titleEntry.Text,
            Description = descriptionEditor.Text,
            DormitoryId = dormitoryPicker.SelectedIndex + 1,
            Price = GetPriceOrNul(priceEntry.Text),
        };

        var client = new ObshajkaClient();
        publishBtn.IsEnabled = false;
        try
        {
            await client.PubslishNewAdvertisement(publishingAdvertisement, _imagePath);
        }
        catch(FailPublishAdvertisementException ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Oк");
            return;
        }
        catch (NetworkUnavailableException ex)
        {
            await DisplayAlert("Сеть недоступна", ex.Message, "Ок");
            return;
        }
        finally
        {
            publishBtn.IsEnabled = true;
        }
        await Navigation.PopAsync();
        await DisplayAlert("Создание объявления", "Объявление успешно создано! Обновите страницу, чтобы его увидеть :)", "Oк");
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