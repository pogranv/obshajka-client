using System.ComponentModel;

using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Interfaces;

using Obshajka.Models;

namespace Obshajka.ViewModels
{
    class OutsideAdvertsViewModel : AdvertsViewModel, INotifyPropertyChanged
    {
        private int? _dormitoryId;
        public int? DormitoryId {
            get => _dormitoryId;
            set
            {
                _dormitoryId = value;
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
