namespace Obshajka.Pages;

public partial class CreateAdvertisement : ContentPage
{
	public CreateAdvertisement()
	{
		InitializeComponent();
	}

    public async void MakeAdvertisementBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("MakeAdvertisementPage");
    }
}