using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;

using Obshajka.Models;
using Obshajka.ViewModels;

namespace Obshajka.Pages;

public partial class OwnAdvertView : ContentPage
{
    private MyAdvertsViewModel _adverts;
    public Advertisement Advertisement { get; set; }
    public OwnAdvertView(MyAdvertsViewModel advertisements, Advertisement selectedAdvertisement)
    {
        InitializeComponent();
        Advertisement = selectedAdvertisement;
        BindingContext = Advertisement;
        _adverts = advertisements;
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("�������� ����������", "�� ������������� ������ ������� ��� ����������?", "��", "���");
        if (!result)
        {
            return;
        }
        long id = (BindingContext as Advertisement).Id;

        var client = new ObshajkaClient();
        try
        {
            await client.RemoveAdvertisement(id);
        } 
        catch (FailRemoveAdvertisementException ex)
        {
            await DisplayAlert("������", ex.Message, "��");
            return;
        }
        catch (NetworkUnavailableException ex)
        {
            await DisplayAlert("���� ����������", ex.Message, "��");
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