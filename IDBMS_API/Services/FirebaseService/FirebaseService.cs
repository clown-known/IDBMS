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
        public async Task<string> UploadImage(IFormFile file)
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
        public async Task<string> UploadTransactionImage(IFormFile file)
        {
            if (file != null && file.Length != 0)
            {

                string fileName = $"{Guid.NewGuid()}{file.FileName}";

                var storageClient = new FirebaseStorage(_storageBucket);
                using (var stream = file.OpenReadStream())
                {
                    stream.Position = 0; // Reset the stream position

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        await storageClient.Child("Transactions").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child("Transactions/" + fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        } 
        public async Task<string> UploadDocument([FromForm] IFormFile file,Guid projectid)
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
                        await storageClient.Child(projectid.ToString()).Child("Document").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child(projectid.ToString()).Child("Document").Child(fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        public async Task<string> UploadContract([FromForm] IFormFile file,Guid projectid)
        {
            if (file != null && file.Length != 0)
            {

                string fileName = $"{DateTime.Now.ToString()}{file.FileName}";

                var storageClient = new FirebaseStorage(_storageBucket);
                using (var stream = file.OpenReadStream())
                {
                    stream.Position = 0; // Reset the stream position

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        await storageClient.Child(projectid.ToString()).Child("Contract").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child(projectid.ToString()).Child("Contract").Child(fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        public async Task<string> UploadBookingDocument([FromForm] IFormFile file,string category,Guid projectid)
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
                        await storageClient.Child(projectid.ToString()).Child("BookingDocument").Child(category).Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child(projectid.ToString()).Child("BookingDocument").Child(category).Child(fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        public async Task<string> UploadInteriorItemImage([FromForm] IFormFile file)
        {
            if (file != null && file.Length != 0)
            {

                string fileName = $"{Guid.NewGuid()}{file.FileName}";

                var storageClient = new FirebaseStorage(_storageBucket);
                using (var stream = file.OpenReadStream())
                {
                    stream.Position = 0; // Reset the stream position

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        await storageClient.Child("InteriorItem").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child("InteriorItem/" + fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        public async Task<string> UploadFile([FromForm] IFormFile file)
        {
            if (file != null && file.Length != 0)
            {

                string fileName = $"{Guid.NewGuid()}{file.FileName}";

                var storageClient = new FirebaseStorage(_storageBucket);
                using (var stream = file.OpenReadStream())
                {
                    stream.Position = 0; // Reset the stream position

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        await storageClient.Child("files").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child("files/" + fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        public async Task<string> UploadCommentFile([FromForm] IFormFile file,Guid projectid,Guid projecttaskid)
        {
            if (file != null && file.Length != 0)
            {

                string fileName = $"{Guid.NewGuid()}{file.FileName}";

                var storageClient = new FirebaseStorage(_storageBucket);
                using (var stream = file.OpenReadStream())
                {
                    stream.Position = 0; // Reset the stream position

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        await storageClient.Child(projectid.ToString()).Child(projecttaskid.ToString()).Child("CommentFiles").Child(fileName).PutAsync(memoryStream);
                    }
                }

                FirebaseStorageReference starsRef = storageClient.Child(projectid.ToString()).Child(projecttaskid.ToString()).Child("CommentFiles").Child(fileName);
                string link = await starsRef.GetDownloadUrlAsync();
                return link;
            }
            return null;
        }
        //public async Task<bool> DeleteFile(string fileName)
        //{
        //    try
        //    {
        //        var storageClient = new FirebaseStorage(_storageBucket);

        //        await storageClient.Child("files").Child(fileName).DeleteAsync();

        //        return true;
        //    }
        //    catch (Firebase.Storage.FirebaseStorageException ex)
        //    {
        //        // Log or handle the exception appropriately
        //        Console.WriteLine($"Firebase Storage error: {ex.Message}");
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception appropriately
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        return false;
        //    }
        //}
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
