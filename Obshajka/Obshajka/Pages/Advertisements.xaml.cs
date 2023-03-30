using Obshajka.ViewModels;
using Obshajka.Models;
using System.Windows.Input;

namespace Obshajka.Pages;

public partial class Advertisements : ContentPage
{
    public Advertisements()
	{
		InitializeComponent();
        Routing.RegisterRoute("AdvertisementPage", typeof(AdvertisementPage));
        BindingContext = new OutsideAdvertsViewModel();
    }

    // TODO: сделать чуствительность к длинным title обяъвлений
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

        // await Shell.Current.GoToAsync("AdvertisementPage");
    }

    private async void kek()
    {
        await DisplayAlert("Уведомление", "Пришло новое сообщение", "ОK");
    }
}