using Obshajka.ViewModels;
using Obshajka.Models;
using Microsoft.Maui.Controls;

namespace Obshajka.Pages;

public partial class MyAdvertisements : ContentPage
{
	public MyAdvertisements()
    {
        InitializeComponent();
        Routing.RegisterRoute("AdvertisementPage", typeof(AdvertisementPage));
        BindingContext = new AdvertisementsViewModel();
    }
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (collectionView.SelectedItem == null)
        {
            return;
        }
        var selectedAdvert = e.CurrentSelection.FirstOrDefault() as Advertisement;
        // kek();
        await Navigation.PushAsync(new AdvertisementPage
        {
            BindingContext = selectedAdvert
        });
        collectionView.SelectedItem = null;
    }

    private async void kek()
    {
        await DisplayAlert("Уведомление", "Пришло новое сообщение", "ОK");
    }
}