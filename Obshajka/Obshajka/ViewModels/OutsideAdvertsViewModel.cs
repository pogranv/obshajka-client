using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Obshajka.Models;

namespace Obshajka.ViewModels
{
    class OutsideAdvertsViewModel : AdvertViewModelAbstract, INotifyPropertyChanged
    {

        public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        private int? dormitoryId;
        public int? DormitoryId {
            get => dormitoryId;
            set
            {
                dormitoryId = value;
                IsRefreshing = true;
            }
        }

        public OutsideAdvertsViewModel() : base()
        {
            //source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
            AdvertisementsListViewElements =
               new ObservableCollection<Advertisement>();
            //source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
            //AdvertisementsListViewElements =
            //   new ObservableCollection<Advertisement>(source);
            // UpdateAdvertisementCollection();
            //AdvertisementsListViewElements.Clear();
        }

        // TODO: удаление без уведомления https://stackoverflow.com/questions/5118513/removeall-for-observablecollections
        // TODO: подумать как без Add сделать, потому что каждый раз событие вызывается
        public void UpdateAdvertisementCollection()
        {
            if (DormitoryId == null) 
            {
                IsRefreshing = false;
                return;
            }
            AdvertisementsListViewElements.Clear();
            List<Advertisement> actualAdverts = Helpers.Helpers.GetAdvertisementsFromOthers((int)DormitoryId).ToList();
            foreach (var advert in actualAdverts)
            {
                AdvertisementsListViewElements.Add(advert);
            }
            IsRefreshing = false;
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



        //public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);
        //public OutsideAdvertsViewModel() : base()
        //{
        //    source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
        //    AdvertisementsListViewElements =
        //       new ObservableCollection<Advertisement>(source);
        //    //source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
        //    //AdvertisementsListViewElements =
        //    //   new ObservableCollection<Advertisement>(source);
        //    // UpdateAdvertisementCollection();
        //}

        //// TODO: удаление без уведомления https://stackoverflow.com/questions/5118513/removeall-for-observablecollections
        //public void UpdateAdvertisementCollection()
        //{
        //    AdvertisementsListViewElements.Clear();
        //    var kek = new ObservableCollection<Advertisement>();
        //    kek.Add(
        //        new Advertisement
        //    {
        //        Id = 1,
        //        CreatorId = 1,
        //        CreatorName = "Name1",
        //        Title = "Title1",
        //        Description = "Description1",
        //        DormitoryId = 1,
        //        Price = 1000,
        //        Image = "dotnet_bot.png",
        //        DateOfAddition = DateOnly.FromDateTime(DateTime.Now),
        //    });
        //    AdvertisementsListViewElements = kek;

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
