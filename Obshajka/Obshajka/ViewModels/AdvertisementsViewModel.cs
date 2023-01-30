using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Data;

namespace Obshajka.ViewModels
{
    internal class AdvertisementsViewModel
    {
        public ObservableCollection<AdvertisementsListViewElement> AdvertisementsListViewElements { get; private set; } = 
            new ObservableCollection<AdvertisementsListViewElement>();

        public AdvertisementsViewModel()
        {
            source = new List<AdvertisementsListViewElement>();
            CreateMonkeyCollection();
            AdvertisementsListViewElements = 
                new ObservableCollection<AdvertisementsListViewElement>(source);
        }

        private List<AdvertisementsListViewElement> source;

        private void CreateMonkeyCollection()
        {
            source.Add(new AdvertisementsListViewElement
            {
                Name = "Name",
                Description = "Description",
                Price = "1000",
                Image = "resources/lamp.png"
            });
        }
    }
}
