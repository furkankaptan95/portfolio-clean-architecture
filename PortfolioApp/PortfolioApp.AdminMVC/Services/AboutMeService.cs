using AutoMapper;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.File;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class AboutMeService : IAboutMeService
{
    private readonly IHttpClientFactory _factory;
    private readonly IMapper _mapper;
    public AboutMeService(IHttpClientFactory factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    private HttpClient FileApiClient => _factory.CreateClient("fileApi");
    public Task<ServiceResult> CreateAboutMeAsync(AddAboutMeApiDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> CreateAboutMeAsync(AddAboutMeMvcDto dto)
    {
        string cvUrl;
        using var cvStream = dto.CvFile.OpenReadStream();
        using var cvContent = new StreamContent(cvStream);

        var cvMultipartContent = new MultipartFormDataContent();
        cvMultipartContent.Add(cvContent, "file", dto.CvFile.FileName);

        var cvResponse = await FileApiClient.PostAsync("upload", cvMultipartContent);
        var cvResult = await cvResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();
        cvUrl = cvResult.Data.FileName;

        string aboutMeImageUrl;
        using var imageStream = dto.AboutMeImage.OpenReadStream();
        using var imageContent = new StreamContent(imageStream);

        var imageMultipartContent = new MultipartFormDataContent();
        imageMultipartContent.Add(imageContent, "file", dto.AboutMeImage.FileName);

        var imageResponse = await FileApiClient.PostAsync("upload", imageMultipartContent);
        var imgResult = await imageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();
        aboutMeImageUrl = imgResult.Data.FileName;

        string heroImageUrl;
        using var heroImageStream = dto.HeroImage.OpenReadStream();
        using var heroImageContent = new StreamContent(heroImageStream);

        var heroImageMultipartContent = new MultipartFormDataContent();
        heroImageMultipartContent.Add(heroImageContent, "file", dto.HeroImage.FileName);

        var heroImageResponse = await FileApiClient.PostAsync("upload", heroImageMultipartContent);
        var heroImageResult = await heroImageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();
        heroImageUrl = heroImageResult.Data.FileName;

        var apiDto = _mapper.Map<AddAboutMeApiDto>(dto);
        apiDto.CvUrl = cvUrl;
        apiDto.AboutMeImageUrl = aboutMeImageUrl;
        apiDto.HeroImageUrl = heroImageUrl;

        var dataApiResponse = await DataApiClient.PostAsJsonAsync("aboutme/create", apiDto);

        var dataApiResult = await dataApiResponse.Content.ReadFromJsonAsync<ServiceResult>();

        return dataApiResult;
    }

    public async Task<ServiceResult<Stream>> DownloadCvAsync(string cvUrl)
    {
        var response = await FileApiClient.GetAsync($"download?fileUrl={cvUrl}");

        var stream = await response.Content.ReadAsStreamAsync();

        return new ServiceResult<Stream>(true, null, stream);
    }

    public async Task<ServiceResult<AboutMeDto>> GetAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("aboutme/get");
        var result = await apiResponse.Content.ReadFromJsonAsync<ServiceResult<AboutMeDto>>();

        if (result is null)
            return new ServiceResult<AboutMeDto>(false);

        return result;

    }

    public Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeApiDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeMVCDto dto)
    {
        var dataApiDto = _mapper.Map<UpdateAboutMeApiDto>(dto);

        if(dto.CvFile is not null)
        {
            string cvUrl;
            using var cvStream = dto.CvFile.OpenReadStream();
            using var cvContent = new StreamContent(cvStream);

            var cvMultipartContent = new MultipartFormDataContent();
            cvMultipartContent.Add(cvContent, "file", dto.CvFile.FileName);

            var cvResponse = await FileApiClient.PostAsync("upload", cvMultipartContent);
            var cvResult = await cvResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();
            cvUrl = cvResult.Data.FileName;

            dataApiDto.CvUrl = cvUrl;

            await FileApiClient.GetAsync($"delete/{dto.CvUrl}");
        }

        if (dto.AboutMeImage is not null)
        {
            string aboutMeImageUrl;
            using var imageStream = dto.AboutMeImage.OpenReadStream();
            using var imageContent = new StreamContent(imageStream);

            var imageMultipartContent = new MultipartFormDataContent();
            imageMultipartContent.Add(imageContent, "file", dto.AboutMeImage.FileName);

            var imageResponse = await FileApiClient.PostAsync("upload", imageMultipartContent);
            var imgResult = await imageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();
            aboutMeImageUrl = imgResult.Data.FileName;

            dataApiDto.AboutMeImageUrl = aboutMeImageUrl;

            await FileApiClient.GetAsync($"delete/{dto.AboutMeImageUrl}");
        }

        if (dto.HeroImage is not null)
        {
            string heroImageUrl;
            using var heroImageStream = dto.HeroImage.OpenReadStream();
            using var heroImageContent = new StreamContent(heroImageStream);

            var heroImageMultipartContent = new MultipartFormDataContent();
            heroImageMultipartContent.Add(heroImageContent, "file", dto.HeroImage.FileName);

            var heroImageResponse = await FileApiClient.PostAsync("upload", heroImageMultipartContent);
            var heroImageResult = await heroImageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();
            heroImageUrl = heroImageResult.Data.FileName;


            dataApiDto.HeroImageUrl = heroImageUrl;
            await FileApiClient.GetAsync($"delete/{dto.HeroImageUrl}");
        }

        var dataApiResponse = await DataApiClient.PostAsJsonAsync("aboutme/update", dataApiDto);

        var dataApiResult = await dataApiResponse.Content.ReadFromJsonAsync<ServiceResult>();

        return dataApiResult;
    }
}
