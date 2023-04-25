using Obshajka.Models;
using Obshajka.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

namespace Obshajka.Pages;

public partial class OwnAdvertView : ContentPage
{

    Advertisement Advertisement { get; set; }
    private AdvertisementsViewModel _adverts;
    public OwnAdvertView(AdvertisementsViewModel advertisements, Advertisement selectedAdvertisement)
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

        // TODO: обработать исключение если не удалось удалить

        var client = new ObshajkaClient();
        try
        {
            client.RemoveAdvertisement(id);
        } 
        catch (FailRemoveAdvertisementException ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "Ок");
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