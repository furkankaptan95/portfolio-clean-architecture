using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PortfolioApp.Application.Common.Configurations;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.File;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class FileService : IFileService
{
    private readonly string _uploadsFolder; 
    public FileService(IOptions<FileSettings> fileSettings)
    {
        _uploadsFolder = fileSettings.Value.UploadsFolder;

        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        }
    }
    public async Task<ServiceResult<FileNameDto>> UploadFileAsync(IFormFile file)
    {
        var result = await SaveFileAsync(file);

        var dto = new FileNameDto
        {
            FileName = result,
        };

        return new ServiceResult<FileNameDto>(true, "Dosya başarıyla kaydedildi." , dto);
    }

    private async Task<string> SaveFileAsync(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName);
        string uniqueFileName = $"{Guid.NewGuid()}{extension}";
        string filePath = Path.Combine(_uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;
    }
}
