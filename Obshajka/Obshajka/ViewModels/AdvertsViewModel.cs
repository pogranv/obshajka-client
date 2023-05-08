using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Obshajka.Models;

namespace Obshajka.ViewModels
{
    public abstract class AdvertsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isRefreshing = false;
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; private set; }
        public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
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
