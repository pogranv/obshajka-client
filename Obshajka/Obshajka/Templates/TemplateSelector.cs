using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obshajka.Templates;
using Obshajka.Data;

namespace Obshajka.Templates
{
    internal class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate AdvertisementWithImageTemplate { get; set; }
        public DataTemplate AdvertisementWithoutImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Data.AdvertisementsListViewElement chat)
            {
                if (chat.Image == null)
                {
                    return AdvertisementWithoutImageTemplate;
                }
                return AdvertisementWithImageTemplate;
            }
            throw new NotImplementedException();
        }
    }
}
