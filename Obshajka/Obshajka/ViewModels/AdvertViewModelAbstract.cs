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
        // TODO: тут вместо AdvertisementsListViewElement заменить
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; internal set; } =
            new ObservableCollection<Advertisement>();

        internal List<Advertisement> source;

        public AdvertViewModelAbstract()
        {
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
