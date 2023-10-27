using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD_UI.Services
{
    public class UploadImage
    {
        public async Task<FileResult> OpenMediaPickerAsync()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please a pick photo"
                });
                if (result.ContentType == "image/png" ||
                    result.ContentType == "image/jpeg" ||
                    result.ContentType == "image/jpg" ||
                    result.ContentType == "image/jfif")
                    return result;
                else
                    await App.Current.MainPage.DisplayAlert("Error Type Image", "Please choose new image", "Ok");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<Stream> FileResultToStream(FileResult fileResult)
        {
            if (fileResult == null)
                return null;
            return await fileResult.OpenReadAsync();
        }

        public Stream ByteArrayToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public string ByteBase64ToString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public byte[] StringToByteBase64(string text)
        {
            return Convert.FromBase64String(text);
        }

        public async Task<ImageFile> Upload(FileResult fileResult)
        {
            byte[] bytes;

            try
            {
                using (var ms = new MemoryStream())
                {
                    var stream = await FileResultToStream(fileResult);
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                return new ImageFile
                {
                    byteBase64 = ByteBase64ToString(bytes),
                    ContentType = fileResult.ContentType,
                    FileName = fileResult.FileName,
                    Path = fileResult.FullPath
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}