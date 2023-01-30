using Obshajka.Pages;

namespace Obshajka;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        // Routing.RegisterRoute("goto", typeof(Goto));

        MainPage = new AppShell();
	}
}
