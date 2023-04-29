using Microsoft.Maui.Controls;
using Obshajka.ViewModels;
using Obshajka.ViewModels;
using Obshajka.Models;

namespace Obshajka.Pages;

public partial class OutsideAdvertsList : ContentPage
{
    public OutsideAdvertsList()
    {
        InitializeComponent();
        Routing.RegisterRoute("OutsideAdvertView", typeof(OutsideAdvertView));
        BindingContext = new OutsideAdvertsViewModel();
    }

    
    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (collectionView.SelectedItem == null)
        {
            return;
        }
        var selectedAdvert = e.CurrentSelection.FirstOrDefault() as Advertisement;
        await Navigation.PushAsync(new OutsideAdvertView
        {
            BindingContext = selectedAdvert
        });
        collectionView.SelectedItem = null;
    }

    private async void DormitoryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var advertList = (BindingContext as OutsideAdvertsViewModel);
        advertList.DormitoryId = dormitoryPicker.SelectedIndex + 1;
        if (advertList.AdvertisementsListViewElements== null || advertList.AdvertisementsListViewElements.Count == 0)
        {
            await DisplayAlert("ѕуста€ стена объ€влений", "¬ данном общежитии не нашлось объ€влений :(", "ќк");
        }
    }
}