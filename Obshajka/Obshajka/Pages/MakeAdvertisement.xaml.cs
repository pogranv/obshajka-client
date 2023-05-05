using Obshajka.Models;
using Obshajka.ViewModels;
using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;
using System.Reflection;

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
            PickerTitle = "����� ����������� ����������",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
            return;

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
            /*using StreamContent stream = new StreamContent(!string.IsNullOrEmpty(_imagePath) ? File.OpenRead(_imagePath)
                : await FileSystem.Current.OpenAppPackageFileAsync("default_advert_image.png"));*/
            //var imagePath = 
            //var cacheFile = Path.Combine(FileSystem.CacheDirectory, "default_advert_image.png");
            var imagePath = _imagePath;

            //if (imagePath == null)
            //{
            //    var assembly = typeof(App).GetTypeInfo().Assembly.GetManifestResourceNames();

            //    var kek = FileSystem.AppDataDirectory.GetType().Assembly.GetManifestResourceNames();


               
            //    imagePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "Resources", "Raw", "default_advert_image.png");
            //}
           



            client.PubslishNewAdvertisement(publishingAdvertisement, /*stream,*/ imagePath);
        }
        catch(FailPublishAdvertisementException ex)
        {
            await DisplayAlert("������", ex.Message, "O�");
            return;
        }
        catch (NetworkUnavailableException ex)
        {
            await DisplayAlert("���� ����������", ex.Message, "��");
            return;
        }
        await Navigation.PopAsync();
        await DisplayAlert("�������� ����������", "���������� ������� �������! �������� ��������, ����� ��� ������� :)", "O�");
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