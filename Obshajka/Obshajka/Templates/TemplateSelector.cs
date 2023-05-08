using ObshajkaWebApi.Interfaces;

namespace Obshajka.Templates
{
    internal class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate AdvertisementWithImageTemplate { get; set; }
        public DataTemplate AdvertisementWithoutImageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is IAdvertisement chat)
            {
                if (chat.Image != null)
                {
                    return AdvertisementWithImageTemplate;
                }
                return AdvertisementWithoutImageTemplate;
            }
            throw new NotImplementedException();
        }
    }
}
