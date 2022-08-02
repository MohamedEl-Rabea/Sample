using System;
using System.Threading.Tasks;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Models;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Services
{
    [ScopedService]
    public interface IDocumentManager
    {
        Task<Document> ManageAsync(DocumentDto documentDto);

        Task<(string documentUrl, Guid documentId)> UploadAsync(UploadRequest request);
    }
}