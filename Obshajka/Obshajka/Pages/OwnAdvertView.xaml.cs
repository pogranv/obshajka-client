using Obshajka.Models;
using Obshajka.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace Obshajka.Pages;

public partial class OwnAdvertView : ContentPage
{

    Advertisement Advertisement { get; set; }
    private AdvertisementsViewModel adverts;
    public OwnAdvertView(AdvertisementsViewModel advertisements, Advertisement selectedAdvertisement)
    {
        InitializeComponent();
        Advertisement = selectedAdvertisement;
        BindingContext = Advertisement;
        adverts = advertisements;
        // image.Source = Advertisement.Image;
    }

    //public OwnAdvertView(AdvertisementsViewModel advertisements)
    //{
    //    InitializeComponent();
    //    BindingContext = advertisements;
    //}

    private async void deleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("�������� ����������", "�� ������������� ������ ������� ��� ����������?", "��", "���");
        if (!result)
        {
            return;
        }
        long id = (BindingContext as Models.Advertisement).Id;
        // TODO: � �� ��� �������� ������ �� ���������� ��������, ������� ������ ����������� ����
        // TODO: ���������� ���������� ���� �� ������� �������
        Helpers.Helpers.RemoveOwnAdvert(id);

        for (int i = 0; i < adverts.AdvertisementsListViewElements.Count; i++)
        {
            if (adverts.AdvertisementsListViewElements[i].Id == id)
            {
                adverts.AdvertisementsListViewElements.RemoveAt(i);
                break;
            }
        }


        await Navigation.PopAsync();
        // await DisplayAlert("�����������", "���� ���������� ������� �������! �������� ������ ����, ����� �������� ���������� ����������.", "�K");

        //if (Shell.Current?.MyAdvertisements is NavigationPage navPage)
        //{
        // ���� ���������
        //    IReadOnlyList<Page> navStack = Navigation.NavigationStack;
        //// ���������� ������� � �����
        //int pageCount = navStack.Count;
        //    // ������� � ����� ���������� �������� - �� ���� MainPage
        //    if (navStack[pageCount - 1] is MyAdvertisements mainPage)
        //    {
        //        // �������� � ������� �������� ����� AddPerson ��� ����������
        //        mainPage.RemoveAdvertisementFromListView(Advertisement);
        //    }
        //}
    }
}