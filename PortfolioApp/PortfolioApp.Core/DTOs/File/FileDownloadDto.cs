namespace PortfolioApp.Core.DTOs.File;
public class FileDownloadDto
{
    public byte[] FileBytes { get; set; }
    public string FileName { get; set; }

    public FileDownloadDto(byte[] fileBytes, string fileName)
    {
        FileBytes = fileBytes;
        FileName = fileName;
    }
}
