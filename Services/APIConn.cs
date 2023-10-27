using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PD_UI.ViewModel;

namespace PD_UI.Services
{
 
    public class APIConn
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        string imagePathAPI;
        

        public APIConn()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            
        }
        public class test
        {
            public string something = "Hello";
        }

        public async Task<string> RefreshDataAsync(string imagePath)
        {
            //test T = new test();
            //string json = JsonSerializer.Serialize("Hello", _serializerOptions);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            using var form = new MultipartFormDataContent();
            String filepath = imagePath;
            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filepath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(fileContent, "file", Path.GetFileName(filepath));
            form.Add(new StringContent("789"), "userId");
            form.Add(new StringContent("some comments"), "comment");
            form.Add(new StringContent("true"), "isPrimary");

            Uri uri = new Uri(string.Format("http://localhost:8000/predictSpiral", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.PostAsync(uri, form);
                if (response.IsSuccessStatusCode)
                {
                    string respone = await response.Content.ReadAsStringAsync();
                    //Items = JsonSerializer.Deserialize<List<TodoItem>>(content, _serializerOptions);
                    return respone;

                }
                else
                {
                    // Handle error responses if needed
                    Debug.WriteLine("API request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in API request", ex.Message);
            }
            return null;

        }
    }
}
