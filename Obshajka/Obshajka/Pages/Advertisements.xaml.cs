using Obshajka.ViewModels;
using Obshajka.Models;

namespace Obshajka.Pages;

public partial class Advertisements : ContentPage
{
    public Advertisements()
	{
		InitializeComponent();
        Routing.RegisterRoute("AdvertisementPage", typeof(AdvertisementPage));
        BindingContext = new AdvertisementsViewModel();
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedAdvert = e.CurrentSelection.FirstOrDefault() as Advertisement;
        // kek();
        //await Navigation.PushAsync(new AdvertisementPage
        //{
        //    BindingContext = selectedAdvert
        //});
        await Shell.Current.GoToAsync("AdvertisementPage");
    }

    private async void kek()
    {
        await DisplayAlert("Уведомление", "Пришло новое сообщение", "ОK");
    }
}