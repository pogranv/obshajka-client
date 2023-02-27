using Obshajka.Pages;

namespace Obshajka;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
        Routing.RegisterRoute("ConfirmVerificationCodePage", typeof(ConfirmVerificationCodePage));
        Routing.RegisterRoute("MakeAdvertisementPage", typeof(MakeAdvertisementPage));
        MainPage = new AppShell();
	}
}
