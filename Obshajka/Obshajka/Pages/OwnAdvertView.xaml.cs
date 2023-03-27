namespace Obshajka.Pages;

public partial class OwnAdvertView : ContentPage
{
	public OwnAdvertView()
	{
		InitializeComponent();
	}

    private async void deleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Удаление объявления", "Вы действительно хотите удалить это объявление?", "Да", "Нет", FlowDirection.LeftToRight);
        if (!result)
        {
            return;
        }
        Helpers.Helpers.RemoveOwnAdvert((BindingContext as Models.Advertisement).Id);
        await Navigation.PopAsync();
    }
}