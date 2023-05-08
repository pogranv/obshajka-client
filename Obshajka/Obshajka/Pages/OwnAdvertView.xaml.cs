using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

using Obshajka.Models;
using Obshajka.ViewModels;

namespace Obshajka.Pages;

public partial class OwnAdvertView : ContentPage
{
    private MyAdvertsViewModel _adverts;
    public Advertisement Advertisement { get; set; }
    public OwnAdvertView(MyAdvertsViewModel advertisements, Advertisement selectedAdvertisement)
    {
        InitializeComponent();
        Advertisement = selectedAdvertisement;
        BindingContext = Advertisement;
        _adverts = advertisements;
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Удаление объявления", "Вы действительно хотите удалить это объявление?", "Да", "Нет");
        if (!result)
        {
            return;
        }
        long id = (BindingContext as Advertisement).Id;

        var client = new ObshajkaClient();
        try
        {
            await client.RemoveAdvertisement(id);
        } 
        catch (FailRemoveAdvertisementException ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Ок");
            return;
        }
        catch (NetworkUnavailableException ex)
        {
            await DisplayAlert("Сеть недоступна", ex.Message, "Ок");
            return;
        }

        for (int i = 0; i < _adverts.AdvertisementsListViewElements.Count; i++)
        {
            if (_adverts.AdvertisementsListViewElements[i].Id == id)
            {
                _adverts.AdvertisementsListViewElements.RemoveAt(i);
                break;
            }
        }
        await Navigation.PopAsync();
    }
}