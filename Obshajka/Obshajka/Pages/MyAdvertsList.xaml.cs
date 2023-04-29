using Obshajka.ViewModels;
using Obshajka.Models;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Windows.Input;

namespace Obshajka.Pages;

public partial class MyAdvertsList : ContentPage
{
    public MyAdvertsList()
    {
        InitializeComponent();
        Routing.RegisterRoute("OwnAdvertView", typeof(OwnAdvertView));
        Routing.RegisterRoute("MakeAdvertisementPage", typeof(MakeAdvertisementPage));
        BindingContext = new MyAdvertsViewModel();
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

    public void RemoveAdvertisementFromListView(Advertisement advertisement)
    {
        (BindingContext as MyAdvertsViewModel).AdvertisementsListViewElements.Remove(advertisement);
    }
}