using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Helpers;
using Obshajka.Models;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace Obshajka.ViewModels
{
    // TODO: чекнуть изменение на странице при изменении списка и если что реализовать интерфейс INotifyPropertyChanged https://metanit.com/sharp/maui/6.7.php
    public class AdvertisementsViewModel : AdvertViewModelAbstract, INotifyPropertyChanged
    {
        // public override ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        public AdvertisementsViewModel() : base()
        {
            //source = Helpers.Helpers.TryGetUserAdvertisements(UserSettings.UserSettings.UserId).ToList();
            AdvertisementsListViewElements =
               new ObservableCollection<Advertisement>();
            //source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
            //AdvertisementsListViewElements =
            //   new ObservableCollection<Advertisement>(source);
            // UpdateAdvertisementCollection();
        }

        public async void UpdateAdvertisementCollection()
        {
            AdvertisementsListViewElements.Clear();
            try
            {
                List<Advertisement> actualAdverts = (await Helpers.Helpers.TryGetUserAdvertisements()).ToList();
                foreach (var advert in actualAdverts)
                {
                    AdvertisementsListViewElements.Add(advert);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsRefreshing = false;
            }
        }

        bool isRefreshing = false;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // TODO: удаление без уведомления https://stackoverflow.com/questions/5118513/removeall-for-observablecollections
        //public void UpdateAdvertisementCollection()
        //{
        //    // AdvertisementsListViewElements.Clear();
        //    // AdvertisementsListViewElements = new ObservableCollection<Advertisement>(source);
        //    // AdvertisementsListViewElements
        //    //source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
        //    //AdvertisementsListViewElements =
        //    //   new ObservableCollection<Advertisement>(source);
        //    //IsRefreshing = false;
        //}

        //bool isRefreshing = false;

        //public bool IsRefreshing
        //{
        //    get => isRefreshing;
        //    set
        //    {
        //        isRefreshing = value;

        //        OnPropertyChanged();
        //    }
        //}

        //#region INotifyPropertyChanged

        //public event PropertyChangedEventHandler PropertyChanged;

        //void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //#endregion
    }
}
