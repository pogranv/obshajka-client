namespace Obshajka.Pages;

public partial class MakeAdvertisementPage : ContentPage
{
	public MakeAdvertisementPage()
	{
		InitializeComponent();
	}

    private async void DownloadImageBtn_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image Please",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
            return;

        var stream = await result.OpenReadAsync();

        advertImage.Source = ImageSource.FromStream(() => stream);
    }

    private bool CheckCorrectAllFields()
    {
        if (string.IsNullOrEmpty(titleEntry.Text))
        {
            return false;
        }
        if (dormitoryPicker.SelectedItem == null)
        {
            return false;
        }
        return true;
    }

    private void PublishBtn_Clicked(Object sender, EventArgs e)
    {
        if (!CheckCorrectAllFields())
        {
            
            return;
        }
        var v1 = titleEntry.Text;
        var v2 = dormitoryPicker.SelectedItem;
        string v3 = dormitoryPicker.SelectedItem.ToString();
    }
}