using Obshajka.Pages;

namespace Obshajka;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("Registration", typeof(Registration));
        MainPage = new AppShell();
	}
}
