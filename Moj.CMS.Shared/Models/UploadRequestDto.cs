using Moj.CMS.Shared.Enums;

namespace Moj.CMS.Shared.Models
{
    public class UploadRequest
    {
        public string Extension { get; set; }
        public UploadType UploadType { get; set; }
        public byte[] Data { get; set; }
    }
}