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
    public abstract class AdvertsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; private set; }

        private bool isRefreshing = false;
        public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public AdvertsViewModel()
        {
            AdvertisementsListViewElements =
              new ObservableCollection<Advertisement>();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected abstract void UpdateAdvertisementCollection();
    }
}
