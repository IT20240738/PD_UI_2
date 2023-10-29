namespace PD_UI.ViewModel.HandDrawing;
using Microsoft.Maui.Controls;
using Windows.Devices.Display.Core;


public partial class DetailPage : ContentPage
{
	public DetailPage()
	{
		InitializeComponent();
	}

    private  void NextClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string age = AgeEntry.Text;
        bool isMale = option6.IsChecked;
        bool isFemale = option7.IsChecked;
        bool hasHandTremor = option1.IsChecked;
        bool hasLossOfSmell = option2.IsChecked;
        bool hasLossOfTaste = option3.IsChecked;
        bool hasTroubleSleeping = option4.IsChecked;
        bool hasDizzinessOrFainting = option5.IsChecked;
        int age1;

        // Check for validation errors
        if (string.IsNullOrWhiteSpace(name) || !int.TryParse(age, out age1) || age1 < 18 || (!isMale && !isFemale)
            || (!hasHandTremor && !hasLossOfSmell && !hasLossOfTaste && !hasTroubleSleeping && !hasDizzinessOrFainting))
        {
            DisplayAlert("Validation Error", "Please fill in all required information.", "OK");
            AgeErrorLabel.IsVisible = true; // Display error message
            return;
        }
        if (!int.TryParse(age, out age1) || age1 < 18)
        {
             AgeErrorLabel.IsVisible = true;
        }
        else
        {
            AgeErrorLabel.IsVisible = false; // Hide error message
            // Process the data here.
            Navigation.PushModalAsync(new ActualPredict(NameEntry.Text, AgeEntry.Text));
            //Navigation.PushModalAsync(actualPredict);

         

        }


    }

  
}