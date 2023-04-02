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
        bool result = await DisplayAlert("Удаление объявления", "Вы действительно хотите удалить это объявление?", "Да", "Нет");
        if (!result)
        {
            return;
        }
        long id = (BindingContext as Models.Advertisement).Id;
        // TODO: я хз как передать данные на предыдущую страницу, поэтому только обновлением пока
        // TODO: обработать исключение если не удалось удалить
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
        // await DisplayAlert("Уведомление", "Ваше объявление успешно удалено! Потяните список вниз, чтобы обновить актуальные объявления.", "ОK");

        //if (Shell.Current?.MyAdvertisements is NavigationPage navPage)
        //{
        // стек навигации
        //    IReadOnlyList<Page> navStack = Navigation.NavigationStack;
        //// количество страниц в стеке
        //int pageCount = navStack.Count;
        //    // находим в стеке предыдущую страницу - то есть MainPage
        //    if (navStack[pageCount - 1] is MyAdvertisements mainPage)
        //    {
        //        // вызываем у главной страницы метод AddPerson для добавления
        //        mainPage.RemoveAdvertisementFromListView(Advertisement);
        //    }
        //}
    }
}