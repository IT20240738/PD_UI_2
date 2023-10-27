namespace PD_UI.ViewModel.HandDrawing;

public partial class DetailPage : ContentPage
{
	public DetailPage()
	{
		InitializeComponent();
	}

    private void NextClicked(object sender, EventArgs e)
    {
        bool selectedOption1 = option1.IsChecked;
        bool selectedOption2 = option2.IsChecked;
        bool selectedOption3 = option3.IsChecked;

        if (selectedOption1 && !selectedOption2)
        {
            //resultLabel.Text = "Hello";
            // Navigate to Page 1
            // Use the navigation mechanism of your MAUI application to navigate to Page 1.
            Navigation.PushModalAsync(new ActualPredict());
        }
        else
        {
            // resultLabel.Text = "No radio button selected";
            Navigation.PushModalAsync(new TestPredict());
        }
    }
}