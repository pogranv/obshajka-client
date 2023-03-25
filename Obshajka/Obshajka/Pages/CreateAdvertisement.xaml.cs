using Obshajka.ViewModels;
using Obshajka.Models;

namespace Obshajka.Pages;

public partial class CreateAdvertisement : ContentPage
{
	public CreateAdvertisement()
    {
        InitializeComponent();
        Routing.RegisterRoute("AdvertisementPage", typeof(AdvertisementPage));
        BindingContext = new AdvertisementsViewModel();
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // kek();
        var selectedAdvert = e.CurrentSelection.FirstOrDefault() as Advertisement;
        await Navigation.PushAsync(new AdvertisementPage
        {
            BindingContext = selectedAdvert
        });
    }

    private async void kek()
    {
        await DisplayAlert("Уведомление", "Пришло новое сообщение", "ОK");
    }
}