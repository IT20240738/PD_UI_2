namespace PD_UI.ViewModel.HandDrawing;
using PD_UI.Services;
public partial class ActualPredict : ContentPage
{

    UploadImage uploadImage { get; set; }

    APIConn apiConn = new APIConn();
    public ImageSource ImageSource { get; set; }

    public ActualPredict()
    {
        InitializeComponent();
        uploadImage = new UploadImage();
    }

    private async void UploadImage_Clicked(object sender, EventArgs e)
    {
        var img = await uploadImage.OpenMediaPickerAsync();
        var imagefile = await uploadImage.Upload(img);

        // Display the uploaded image path
        string imagePath = imagefile.Path;
        ImagePathLable.Text = "Image Path: " + imagefile.Path;
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
            ApiResponseLabel.Text = "API Response: " + apiResponse;
        }
        else
        {
            // Handle the case where the API request was not successful
        }


    }
    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new DetailPage());
    }
}