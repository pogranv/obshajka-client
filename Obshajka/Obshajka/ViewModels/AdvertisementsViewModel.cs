using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Helpers;
using Obshajka.Models;

namespace Obshajka.ViewModels
{
    // TODO: чекнуть изменение на странице при изменении списка и если что реализовать интерфейс INotifyPropertyChanged https://metanit.com/sharp/maui/6.7.php
    internal class AdvertisementsViewModel
    {
        // тут вместо AdvertisementsListViewElement заменить
        public ObservableCollection<Advertisement> AdvertisementsListViewElements { get; private set; } = 
            new ObservableCollection<Advertisement>();

        private List<Advertisement> source;

        public AdvertisementsViewModel()
        {
            source = new List<Advertisement>();
            CreateAdvertisementCollection();
            AdvertisementsListViewElements = 
                new ObservableCollection<Advertisement>(source);
        }

        private void CreateAdvertisementCollection()
        {
            source = Helpers.Helpers.GetAdvertisementsFromOthers().ToList();
        }
    }
}
