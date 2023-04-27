using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Obshajka.Helpers;
using Obshajka.Models;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Interfaces;

namespace Obshajka.ViewModels
{
    public class AdvertisementsViewModel : AdvertViewModelAbstract, INotifyPropertyChanged
    {
        // public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        public AdvertisementsViewModel() : base()
        {
            //AdvertisementsListViewElements =
            //   new ObservableCollection<Advertisement>();
        }

        /*public*/protected async override void UpdateAdvertisementCollection()
        {
            AdvertisementsListViewElements.Clear();

            var client = new ObshajkaClient();
            try
            {
                // List<Advertisement> actualAdverts = (await Helpers.Helpers.TryGetUserAdvertisements()).ToList();
                //var kek = await client.GetUserAdvertisements(UserSettings.UserSettings.UserId);
                //var lol = (List<Advertisement>)kek;
                var actualAdverts = await client.GetUserAdvertisements(UserSettings.UserSettings.UserId);
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
