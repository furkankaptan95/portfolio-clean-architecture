using AutoMapper;
using Microsoft.AspNetCore.Rewrite;
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
        var cvResponse = await FileApiClient.PostAsync("upload", content: new MultipartFormDataContent
          {
             { new StreamContent(dto.CvFile.OpenReadStream()), "file", dto.CvFile.FileName }
         });

        var cvResult = await cvResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();

        var imageResponse = await FileApiClient.PostAsync("upload", content: new MultipartFormDataContent
        {
             { new StreamContent(dto.AboutMeImage.OpenReadStream()), "file", dto.AboutMeImage.FileName }
        });

        var imgResult = await imageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();

        var apiDto = _mapper.Map<AddAboutMeApiDto>(dto);

        apiDto.CvUrl = cvResult.Data.FileName;
        apiDto.AboutMeImageUrl = imgResult.Data.FileName;

        var dataApiResponse = await DataApiClient.PostAsJsonAsync("aboutme/create", apiDto);

        var dataApiResult = await dataApiResponse.Content.ReadFromJsonAsync<ServiceResult>();

        return dataApiResult;
    }

    public async Task<ServiceResult<byte[]>> DownloadCvAsync(string cvUrl)
    {
        var response = await FileApiClient.GetAsync($"download?fileUrl={cvUrl}");

        var result = await response.Content.ReadAsByteArrayAsync();

        return new ServiceResult<byte[]>(true,null,result);
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
            var cvResponse = await FileApiClient.PostAsync("upload", content: new MultipartFormDataContent
              {
                 { new StreamContent(dto.CvFile.OpenReadStream()), "file", dto.CvFile.FileName }
             });

            var cvResult = await cvResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();

            dataApiDto.CvUrl = cvResult.Data.FileName;

            await FileApiClient.GetAsync($"delete/{dto.CvUrl}");
        }

        if (dto.AboutMeImage is not null)
        {
            var imageResponse = await FileApiClient.PostAsync("upload", content: new MultipartFormDataContent
            {
                 { new StreamContent(dto.AboutMeImage.OpenReadStream()), "file", dto.AboutMeImage.FileName }
            });

            var imgResult = await imageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();

            dataApiDto.AboutMeImageUrl = imgResult.Data.FileName;

            await FileApiClient.GetAsync($"delete/{dto.AboutMeImageUrl}");
        }

        var dataApiResponse = await DataApiClient.PostAsJsonAsync("aboutme/update", dataApiDto);

        var dataApiResult = await dataApiResponse.Content.ReadFromJsonAsync<ServiceResult>();

        return dataApiResult;
    }
}
