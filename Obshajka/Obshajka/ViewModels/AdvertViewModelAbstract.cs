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
    public abstract class AdvertViewModelAbstract : INotifyPropertyChanged
    {
        // TODO: тут вместо AdvertisementsListViewElement заменить
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; private set; } =
            new ObservableCollection<Advertisement>();

        // internal List<Advertisement> source;

        public AdvertViewModelAbstract()
        {
            AdvertisementsListViewElements =
              new ObservableCollection<Advertisement>();
        }

        public ICommand RefreshAdvertisementsCommand => new Command(UpdateAdvertisementCollection);
        protected abstract void UpdateAdvertisementCollection();
        // #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // #endregion
    }
}
