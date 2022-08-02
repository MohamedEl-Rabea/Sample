using System.Collections.Generic;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Services
{
    [TransientService]
    public interface IFileBuilder 
    {
        byte[] GenerateExcel<T>(IEnumerable<T> models, string sheetName);
        byte[] GenerateExcel<T>(IEnumerable<T> itemList, string sheetName, string[] properties);
    }
}
