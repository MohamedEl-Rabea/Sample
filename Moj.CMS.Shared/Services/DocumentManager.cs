using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Enums;
using Moj.CMS.Shared.Models;

namespace Moj.CMS.Shared.Services
{
    public class DocumentManager : IDocumentManager
    {
        public static string RootDirectory = "Files";
        public async Task<(string documentUrl, Guid documentId)> UploadAsync(UploadRequest request)
        {
            if (request.Data == null) 
                return (string.Empty, Guid.Empty);
            
            var streamData = new MemoryStream(request.Data);
            if (streamData.Length > 0)
            {
                var folder = request.UploadType.ToDescriptionString();
                var folderName = Path.Combine(RootDirectory, folder);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                bool exists = Directory.Exists(pathToSave);
                if (!exists)
                    Directory.CreateDirectory(pathToSave);
                var documentId = Guid.NewGuid();
                var fileName = $"{documentId}{request.Extension}";
                var absolutePath = Path.Combine(pathToSave, fileName);
                var relativePath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(absolutePath, FileMode.Create))
                {
                    await streamData.CopyToAsync(stream);
                }
                return (relativePath, documentId);
            }
            else
            {
                return (string.Empty, Guid.Empty);
            }
        }

        public async Task<Document> ManageAsync(DocumentDto documentDto)
        {
            string documentUrl = documentDto.DocumentUrl;
            var documentId = documentDto.DocumentId;
            var isNewDocument = !documentDto.DocumentId.HasValue && documentDto.UploadRequest != null;
            if (isNewDocument)
            {
                var uploadResult = await UploadAsync(documentDto.UploadRequest);
                documentId = uploadResult.documentId;
                documentUrl = uploadResult.documentUrl;
            }
            else if (documentDto.UploadRequest != null && documentDto.DocumentId.GetValueOrDefault(Guid.Empty) != Guid.Empty)
            {
                //1- Delete old document
                DeleteFile(documentDto.DocumentId.Value);

                //2- Create the new one
                var uploadResult = await UploadAsync(documentDto.UploadRequest);
                documentId = uploadResult.documentId;
                documentUrl = uploadResult.documentUrl;
            }
            else if (documentDto.UploadRequest == null && string.IsNullOrEmpty(documentDto.DocumentUrl))
            {
                DeleteFile(documentDto.DocumentId.Value);
            }

            return Document.Create(documentId, documentUrl);
        }

        private void DeleteFile(Guid documentId)
        {
            var absoluteDirectory = Path.Combine(Directory.GetCurrentDirectory(), RootDirectory);
            var searchPattern = $"{documentId}.*";
            var filePath = Directory.GetFiles(absoluteDirectory, searchPattern, SearchOption.AllDirectories).SingleOrDefault();
            if (!string.IsNullOrEmpty(filePath))
                File.Delete(filePath);
        }
    }
}