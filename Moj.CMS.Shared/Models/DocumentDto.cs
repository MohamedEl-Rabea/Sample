using System;

namespace Moj.CMS.Shared.Models
{
    public class DocumentDto
    {
        public Guid? DocumentId { get; set; }
        public string DocumentUrl { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }
}