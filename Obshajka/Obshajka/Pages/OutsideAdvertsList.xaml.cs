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

    // TODO: сделать чуствительность к длинным title об€ъвлений
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

    private void DormitoryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        (BindingContext as OutsideAdvertsViewModel).DormitoryId = dormitoryPicker.SelectedIndex + 1;
    }
}