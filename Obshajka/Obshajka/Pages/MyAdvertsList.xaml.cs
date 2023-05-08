using Obshajka.ViewModels;
using Obshajka.Models;

namespace Obshajka.Pages;
public partial class MyAdvertsList : ContentPage
{
    public MyAdvertsList()
    {
        InitializeComponent();
        Routing.RegisterRoute("OwnAdvertView", typeof(OwnAdvertView));
        Routing.RegisterRoute("MakeAdvertisementPage", typeof(MakeAdvertisementPage));
        BindingContext = new MyAdvertsViewModel();
        (BindingContext as MyAdvertsViewModel).IsRefreshing = true;
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (collectionView.SelectedItem == null)
        {
            return;
        }
        var selectedAdvert = e.CurrentSelection.FirstOrDefault() as Advertisement;
        await Navigation.PushAsync(new OwnAdvertView((BindingContext as MyAdvertsViewModel), selectedAdvert));
        collectionView.SelectedItem = null;
    }

    private async void AddAdvertButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MakeAdvertisementPage((BindingContext as MyAdvertsViewModel)));
    }
}