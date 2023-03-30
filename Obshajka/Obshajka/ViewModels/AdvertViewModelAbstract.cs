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
    public class AdvertViewModelAbstract : INotifyPropertyChanged
    {
        // public virtual ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        // TODO: тут вместо AdvertisementsListViewElement заменить
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; internal set; } =
            new ObservableCollection<Advertisement>();

        internal List<Advertisement> source;

        public AdvertViewModelAbstract()
        {
            // source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
            //AdvertisementsListViewElements =
            //   new ObservableCollection<Advertisement>(source);
            // UpdateAdvertisementCollection();
        }

        // TODO: удаление без уведомления https://stackoverflow.com/questions/5118513/removeall-for-observablecollections
        public void UpdateAdvertisementCollection()
        {
            // AdvertisementsListViewElements.Clear();
            // AdvertisementsListViewElements = new ObservableCollection<Advertisement>(source);
            // AdvertisementsListViewElements
            //source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
            //AdvertisementsListViewElements =
            //   new ObservableCollection<Advertisement>(source);
            // IsRefreshing = false;
        }

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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
