using Obshajka.ViewModels;
using Obshajka.Models;
using Microsoft.Maui.Controls;

namespace Obshajka.Pages;

public partial class MyAdvertisements : ContentPage
{
	public MyAdvertisements()
    {
        InitializeComponent();
        Routing.RegisterRoute("OwnAdvertView", typeof(OwnAdvertView));
        Routing.RegisterRoute("MakeAdvertisementPage", typeof(MakeAdvertisementPage));
        BindingContext = new AdvertisementsViewModel();
    }
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (collectionView.SelectedItem == null)
        {
            return;
        }
        var selectedAdvert = e.CurrentSelection.FirstOrDefault() as Advertisement;
        await Navigation.PushAsync(new OwnAdvertView
        {
            BindingContext = selectedAdvert
        });
        collectionView.SelectedItem = null;
    }

    private async void addAdvertButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MakeAdvertisementPage");
    }
}