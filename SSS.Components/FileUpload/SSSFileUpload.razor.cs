using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SSS.Components.FileUpload
{
    public partial class SSSFileUpload
    {
        [Parameter]
        public EventCallback<UploadFileInfo> OnFileSelect { get; set; }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var buffer = new byte[_file.Size];
                var extension = Path.GetExtension(_file.Name);
                var format = "application/octet-stream";
                await _file.OpenReadStream(_file.Size).ReadAsync(buffer);
                var URL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                var selectedFileInfo = new UploadFileInfo() { Data = buffer, Extension = extension, URL = URL };
                await OnFileSelect.InvokeAsync(selectedFileInfo);
            }
        }
    }
}
