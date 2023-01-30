using Obshajka.Data;
using Obshajka.ViewModels;

namespace Obshajka.Pages;

public partial class Advertisements : ContentPage
{
    public Advertisements()
	{
		InitializeComponent();
        BindingContext = new AdvertisementsViewModel();
    }
}