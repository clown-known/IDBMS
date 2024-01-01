using Firebase.Storage;
using Firebase.Auth;

namespace BLL.Services
{
    public class FirebaseService
    {
        IConfiguration config;
        public  FirebaseService()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
<<<<<<< HEAD
        public async Task<string> Upload(Stream fileStream, string fileName)
=======
        public async Task<string> UploadImage(IFormFile file)
        {
            try
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
            }catch(Exception e)
            {

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
>>>>>>> dev
        {
            var firebaseAuthLink = await SignIn();
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    config["firebase:Bucket"],
                    new FirebaseStorageOptions()
                    {
<<<<<<< HEAD
                        AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                        ThrowOnCancel = true
                    }
                ).Child(fileName)
                .PutAsync(fileStream);
            return await task;
        }
        public async void Delete
            (string fileName)
        {
            var firebaseAuthLink = await SignIn();
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    config["firebase:Bucket"],
                    new FirebaseStorageOptions()
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuthLink.FirebaseToken),
                        ThrowOnCancel = true
                    }
                ).Child(fileName)
                .DeleteAsync();
            await task;
=======
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
>>>>>>> dev
        }

        private async Task<FirebaseAuthLink> SignIn()
        {
<<<<<<< HEAD
            var auth = new FirebaseAuthProvider(new FirebaseConfig(config["firebase:ApiKey"]));
            var a = await auth.SignInWithEmailAndPasswordAsync(config["firebase:auth:email"], config["firebase:auth:password"]);
            return a;
=======

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
>>>>>>> dev
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
