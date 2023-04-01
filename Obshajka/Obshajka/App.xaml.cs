using Obshajka.Pages;

namespace Obshajka;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
        MainPage = new AppShell();
	}
}
