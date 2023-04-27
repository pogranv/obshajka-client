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
using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Interfaces;

namespace Obshajka.ViewModels
{
    class OutsideAdvertsViewModel : AdvertViewModelAbstract, INotifyPropertyChanged
    {
        // public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

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
            //AdvertisementsListViewElements =
            //   new ObservableCollection<Advertisement>();
        }
        /*public*/protected override async void UpdateAdvertisementCollection()
        {
            if (DormitoryId == null) 
            {
                IsRefreshing = false;
                return;
            }
            AdvertisementsListViewElements.Clear();

            var client = new ObshajkaClient();

            try
            {
                // List<Advertisement> actualAdverts = (await Helpers.Helpers.TryGetOutsideAdvertisements((int)DormitoryId)).ToList();
                var actualAdverts = (await client.GetAdvertisementsFromOtherUsers((int)DormitoryId, UserSettings.UserSettings.UserId));
                foreach (IAdvertisement advert in actualAdverts)
                {
                    AdvertisementsListViewElements.Add(Advertisement.Build(advert));
                }
            } 
            catch (FailGetAdvertisementsException) { }
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
                base.OnPropertyChanged();
            }
        }

        //#region INotifyPropertyChanged

        //public event PropertyChangedEventHandler PropertyChanged;

        //void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //#endregion
    }
}
