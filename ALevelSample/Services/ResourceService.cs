using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ALevelSample.Config;
using ALevelSample.Dtos.Responses;
using ALevelSample.Dtos;
using ALevelSample.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ALevelSample.Dtos.Requests;

namespace ALevelSample.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IInternalHttpClientService _httpClientService;
        private readonly string _resourceApi = "api/unknown/";
        private readonly ILogger<ResourceService> _logger;
        private readonly ApiOption _options;

        public ResourceService(IInternalHttpClientService httpClientService, IOptions<ApiOption> options, ILogger<ResourceService> logger)
        {
            _httpClientService = httpClientService;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<ResourceDto> GetResourceById(int id)
        {
            var result = await _httpClientService.SendAsync<BaseResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}{id}", HttpMethod.Get);

            if (result?.Data != null)
            {
                _logger.LogInformation($"Resource with id = {result.Data.Id} {result.Data.Name} was found");
            }
            else
            {
                _logger.LogInformation($"Resource with id = {id} was not found");
                return null;
            }

            return result?.Data;
        }

        public async Task<List<ResourceDto>> GetResources()
        {
            var result = await _httpClientService.SendAsync<ListResponse<ResourceDto>, object>($"{_options.Host}{_resourceApi}", HttpMethod.Get);

            if (result?.Data != null)
            {
                _logger.LogInformation($"Resources is found");
            }

            return result?.Data;
        }
    }
}
