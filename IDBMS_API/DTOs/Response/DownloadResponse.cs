using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.DTOs.Response
{
    public class DownloadResponse
    {
        public string FileName { get; set; } = default!;
        public string FileType { get; set; } = default!;
        public FileContentResult File { get; set; } = default!;
    }
}
