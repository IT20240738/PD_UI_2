namespace PD_UI;

using PD_UI.ViewModel.HandDrawing;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new DetailPage();
	}
}
