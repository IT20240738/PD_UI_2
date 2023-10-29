namespace PD_UI.ViewModel.HandDrawing;
using PD_UI.Services;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Xml.Linq;


public partial class ActualPredict : ContentPage
{

    UploadImage uploadImage { get; set; }

    APIConn apiConn = new APIConn();
    public ImageSource ImageSource { get; set; }

    String name1;
    String age2;

    public ActualPredict(String name, String age)
    {
       InitializeComponent();
        uploadImage = new UploadImage();
        name1 = name;
        age2 = age;
        NameLabel.Text += name;
        AgeLabel.Text += age;
    }

    private async void UploadImage_Clicked(object sender, EventArgs e)
    {
        var img = await uploadImage.OpenMediaPickerAsync();
        var imagefile = await uploadImage.Upload(img);

        // Display the uploaded image path
        string imagePath = imagefile.Path;
        //ImagePathLable.Text = "Image Path: " + imagefile.Path;
        //DisplayAlert("Image Upload Path", imagePath, "OK");

        // Pass the dynamically obtained image path to the RestService
        await apiConn.RefreshDataAsync(imagePath);

        Image_Upload.Source = ImageSource.FromStream(() =>
            uploadImage.ByteArrayToStream(uploadImage.StringToByteBase64(imagefile.byteBase64))
        );

        // Call the RefreshDataAsync method and get the response
        string apiResponse = await apiConn.RefreshDataAsync(imagePath);

        if (apiResponse != null)
        {
            // Handle the API response as needed
            apiResponse = apiResponse.Replace("\"", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).Replace("prediction:", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty); ;
            ApiResponseLabel.Text = "API Response: " + apiResponse;
            Description.Text = "The 'Neura PD' application provides results, but it's not guaranteed to be 100% accurate. We recommend meeting with your doctor for more detailed information. Wishing you good health.";
            Description.LineBreakMode = LineBreakMode.WordWrap;
        }
        else
        {
            // Handle the case where the API request was not successful
        }

    }
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushModalAsync(new DetailPage());
        bool answer = await DisplayAlert("Confirmation", "Do you want to use this test again?", "Yes", "No");

        if (answer)
        {
            await Navigation.PushModalAsync(new ActualPredict(name1,age2));
        }
        else
        {
            // User clicked "No," navigate to the home page
            await Navigation.PushModalAsync(new DetailPage());
        }
    }

   
}