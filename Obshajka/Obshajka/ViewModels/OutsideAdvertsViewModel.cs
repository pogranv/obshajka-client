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
    class OutsideAdvertsViewModel : AdvertsViewModel, INotifyPropertyChanged
    {
        private int? dormitoryId;
        public int? DormitoryId {
            get => dormitoryId;
            set
            {
                dormitoryId = value;
                IsRefreshing = true;
            }
        }

        public OutsideAdvertsViewModel() : base() { }
        protected override async void UpdateAdvertisementCollection()
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
                var actualAdverts = (await client.GetAdvertisementsFromOtherUsers((int)DormitoryId, UserSettings.UserSettings.UserId));
                if (actualAdverts == null)
                {
                    return;
                }

                foreach (IAdvertisement advert in actualAdverts)
                {
                    AdvertisementsListViewElements.Add(Advertisement.Build(advert));
                }
            } 
            catch (FailGetAdvertisementsException) { }
            catch (NetworkUnavailableException) { }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
