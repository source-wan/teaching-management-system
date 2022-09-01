using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.App.Common.Interface
{
    public interface IAppFileUpload
    {
        Task<IEnumerable<string>> UploadFile(IFormCollection files);

        Task<FileContentResult> GetFileById(Guid id);
    }
}