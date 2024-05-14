using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ALevelSample.Config;
using ALevelSample.Dtos;
using ALevelSample.Dtos.Requests;
using ALevelSample.Dtos.Responses;
using ALevelSample.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ALevelSample.Services;

public class UserService : IUserService
{
    private readonly IInternalHttpClientService _httpClientService;
    private readonly ILogger<UserService> _logger;
    private readonly ApiOption _options;
    private readonly string _userApi = "api/users/";
    private readonly string _userApiRegister = "api/register/";
    private readonly string _userApiLogin = "api/login/";

    public UserService(
        IInternalHttpClientService httpClientService,
        IOptions<ApiOption> options,
        ILogger<UserService> logger)
    {
        _httpClientService = httpClientService;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var result = await _httpClientService.SendAsync<BaseResponse<UserDto>, object>($"{_options.Host}{_userApi}{id}", HttpMethod.Get);

        if (result?.Data != null)
        {
            _logger.LogInformation($"User with id = {result.Data.Id} was found");
        }
        else
        {
            _logger.LogInformation($"User with id = {id} was not found");
            return null;
        }

        return result?.Data;
    }

    public async Task<UserResponse> CreateUser(string name, string job)
    {
        var result = await _httpClientService.SendAsync<UserResponse, UserRequest>(
            $"{_options.Host}{_userApi}",
            HttpMethod.Post,
            new UserRequest()
            {
                Job = job,
                Name = name
            });

        if (result != null)
        {
            _logger.LogInformation($"User with id = {result?.Id} was created");
        }

        return result;
    }

    public async Task<UserRegisterResponce> RegisterUser(string email, string password)
    {
        var result = await _httpClientService.SendAsync<UserRegisterResponce, UserRegisterRequest>(
            $"{_options.Host}{_userApiRegister}",
            HttpMethod.Post,
            new UserRegisterRequest()
            {
                Email = email,
                Password = password
            });

        if (result != null)
        {
            _logger.LogInformation($"User with token {result?.Token} and id {result?.Id} was registered");
        }
        else
        {
            _logger.LogInformation($"Error! {result?.Error}");
        }

        return result;
    }

    public async Task<UserLoginResponse> LoginUser(string email, string password)
    {
        var result = await _httpClientService.SendAsync<UserLoginResponse, UserRegisterRequest>(
            $"{_options.Host}{_userApiLogin}",
            HttpMethod.Post,
            new UserRegisterRequest()
            {
                Email = email,
                Password = password
            });

        if (result != null)
        {
            _logger.LogInformation($"User with token {result?.Token} was logined");
        }
        else
        {
            _logger.LogInformation($"Error! {result?.Error}");
        }

        return result;
    }

    public async Task<UserResponseUpdate> UpdateUserPut(int id, string name, string job)
    {
        var result = await _httpClientService.SendAsync<UserResponseUpdate, UserRequest>(
            $"{_options.Host}{_userApi}{id}",
            HttpMethod.Put,
            new UserRequest()
            {
                Job = job,
                Name = name
            });

        if (result != null)
        {
            _logger.LogInformation($"User was updated at {result?.UpdatedAt}");
        }

        return result;
    }

    public async Task<UserResponseUpdate> UpdateUserPatch(int id, string name, string job)
    {
        var result = await _httpClientService.SendAsync<UserResponseUpdate, UserRequest>(
            $"{_options.Host}{_userApi}{id}",
            HttpMethod.Patch,
            new UserRequest()
            {
                Job = job,
                Name = name
            });

        if (result != null)
        {
            _logger.LogInformation($"User was updated at {result?.UpdatedAt}");
        }

        return result;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var result = await _httpClientService.SendAsync<object, object>(
            $"{_options.Host}{_userApi}{id}",
            HttpMethod.Delete);

        if (result != null)
        {
            _logger.LogInformation($"User with id = {id} was deleted");
            return true;
        }
        else
        {
            _logger.LogInformation($"Error! User with id = {id} was not deleted");
            return false;
        }
    }

    public async Task<List<UserDto>> GetUsers(string request = "")
    {
        var result = await _httpClientService.SendAsync<ListUser<UserDto>, object>($"{_options.Host}{_userApi}?{request}", HttpMethod.Get);
        if (result?.Data != null)
        {
            _logger.LogInformation($"Users are found");
        }

        return result?.Data;
    }
}