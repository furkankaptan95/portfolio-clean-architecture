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

    public ServiceResult DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_uploadsFolder, fileName);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);

            return new ServiceResult(true,"Dosya başarıyla silindi.");
        }

        return new ServiceResult(false, "Dosya silinirken bir hatayla karşılaşıldı.");
    }

    public async Task<ServiceResult<FileDownloadDto>> DownloadAsync(string fileUrl)
    {
        var filePath = Path.Combine(_uploadsFolder, fileUrl);

        if (!System.IO.File.Exists(filePath))
        {
            return new ServiceResult<FileDownloadDto>(false,"Dosya bulunamadı.",null);
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        var fileName = Path.GetFileName(filePath) ?? "downloaded_file";

        return new ServiceResult<FileDownloadDto>(true,"Dosya başarıyla indirildi.", new FileDownloadDto(fileBytes, fileName));
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
