using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IHttpClientFactory _factory;
        public PersonalInfoService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        private HttpClient DataApiClient => _factory.CreateClient("dataApi");
        public Task<ServiceResult> AddAsync(AddPersonalInfoDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<PersonalInfoDto>> GetAsync()
        {
            var apiResponse = await DataApiClient.GetAsync("personalinfo/get");

            return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<PersonalInfoDto>>();
        }

        public Task<ServiceResult> UpdateAsync(UpdatePersonalInfoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
