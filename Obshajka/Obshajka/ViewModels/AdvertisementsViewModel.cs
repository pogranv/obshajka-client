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
    public class AdvertisementsViewModel : INotifyPropertyChanged
    {
        public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        // TODO: тут вместо AdvertisementsListViewElement заменить
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; private set; } = 
            new ObservableCollection<Advertisement>();

        private List<Advertisement> source;

        public AdvertisementsViewModel()
        {
            source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
            AdvertisementsListViewElements =
               new ObservableCollection<Advertisement>(source);
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
    }
}
