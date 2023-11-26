using DocumentFormat.OpenXml.Vml;
using Firebase.Storage;
using IDBMS_API.Constants;
using Microsoft.AspNetCore.Mvc;
using Path = System.IO.Path;

namespace BLL.Services
{
    public class FirebaseService
    {
        IConfiguration config;
        private readonly string _storageBucket;
        public  FirebaseService()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            _storageBucket = config["Firebase:StorageBucket"];
        }
        public async Task<string> UploadImage([FromForm] IFormFile file)
        {
            if (file != null && file.Length != 0)
            {

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                var storageClient = new FirebaseStorage(_storageBucket);
                using (var stream = file.OpenReadStream())
                {
                    stream.Position = 0; // Reset the stream position

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        await storageClient.Child("images").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child("images/" + fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        public async Task<string> UploadByByte(byte[] file,string filename,Guid ProjectId,string fileType)
        {
            string fileName = filename+$"-{DateTime.UtcNow}.docx";
            
            var storageClient = new FirebaseStorage(_storageBucket);
            using (var stream = new MemoryStream(file))
            {
                stream.Position = 0; // Reset the stream position

                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    await storageClient.Child($"{ProjectId}").Child(fileType).Child(fileName).PutAsync(memoryStream);
                }
            }

            FirebaseStorageReference starsRef = storageClient.Child($"{ProjectId}").Child(fileType).Child(fileName);
            string downloadUrl = await starsRef.GetDownloadUrlAsync();
            return downloadUrl;
        }
        public async Task<byte[]> DownloadFile(string? fileName,Guid? projectId,string fileType,bool isSample)
        {

            var storageClient = new FirebaseStorage(_storageBucket);
            FirebaseStorageReference starsRef;
            if (isSample)
            {
                starsRef = storageClient.Child("Sample").Child($"{fileType}").Child(fileName);
            }
            else
            {
                starsRef = storageClient.Child(projectId.ToString()).Child($"{fileType}").Child(fileName);
                //starsRef = storageClient.Child($"{projectId}/" + $"{fileType}/" + fileName);
            }

            // Get the download URL
            string downloadUrl = await starsRef.GetDownloadUrlAsync();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(downloadUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    return content;
                }
            }
            return null;
        }
        public async Task<byte[]> DownloadFiletest(string? fileName)
        {

            var storageClient = new FirebaseStorage(_storageBucket);
            FirebaseStorageReference starsRef;

                starsRef = storageClient.Child("images/images/"+ fileName);
                //starsRef = storageClient.Child($"{projectId}/" + $"{fileType}/" + fileName);


            // Get the download URL
            string downloadUrl = await starsRef.GetDownloadUrlAsync();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(downloadUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    return content;
                }
            }
            return null;
        }
        public async Task<byte[]> DownloadFileByDownloadUrl(string downloadUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(downloadUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    return content;
                }
            }
            return null;
        }
    }
}
