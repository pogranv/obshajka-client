using System.ComponentModel;

using ObshajkaWebApi;
using ObshajkaWebApi.Exceptions;
using ObshajkaWebApi.Interfaces;

using Obshajka.Models;

namespace Obshajka.ViewModels
{
    public class MyAdvertsViewModel : AdvertsViewModel, INotifyPropertyChanged
    {
        public MyAdvertsViewModel() : base() { }

        protected async override void UpdateAdvertisementCollection()
        {
            AdvertisementsListViewElements.Clear();

            var client = new ObshajkaClient();
            try
            {
                var actualAdverts = await client.GetUserAdvertisements(UserSettings.UserSettings.UserId);
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
