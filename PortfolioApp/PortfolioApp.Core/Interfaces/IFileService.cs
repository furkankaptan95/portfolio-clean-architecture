using Microsoft.AspNetCore.Http;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.File;

namespace PortfolioApp.Core.Interfaces;
public interface IFileService
{
    Task<ServiceResult<FileNameDto>> UploadFileAsync(IFormFile file);
    ServiceResult DeleteFile(string fileName);
}
