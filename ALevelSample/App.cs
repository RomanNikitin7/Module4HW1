using System.Threading.Tasks;
using ALevelSample.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ALevelSample;

public class App
{
    private readonly IUserService _userService;
    private readonly IResourceService _resourceService;
    private readonly ILogger<App> _logger;

    public App(IUserService userService, IResourceService resourceService, ILogger<App> logger)
    {
        _userService = userService;
        _resourceService = resourceService;
        _logger = logger;
    }

    public async Task Start()
    {
        var user = await _userService.GetUserById(2);
        var userInfo = await _userService.CreateUser("morpheus", "leader");
        var resources = await _resourceService.GetResources();
        _logger.LogInformation($"All resources {JsonConvert.SerializeObject(resources)}");
        var users = await _userService.GetUsers("page=2");
        _logger.LogInformation($"All users {JsonConvert.SerializeObject(users)}");
        var userNotFound = await _userService.GetUserById(23);
        var resource = await _resourceService.GetResourceById(2);
        var resourceNotFound = await _resourceService.GetResourceById(23);
        var updateUserPut = await _userService.UpdateUserPut(2, "morpheus", "zion resident");
        var updateUserPatch = await _userService.UpdateUserPatch(2, "morpheus", "zion resident");
        var deleteUser = await _userService.DeleteUser(2);
        var registerUser = await _userService.RegisterUser("eve.holt@reqres.in", "pistol");
        var registerUserUnsuccessful = await _userService.RegisterUser("sydney@fife", null);
        var loginUser = await _userService.LoginUser("eve.holt@reqres.in", "cityslicka");
        var loginUserUnsuccessful = await _userService.LoginUser("peter@klaven", null);
        var users1 = await _userService.GetUsers("delay=3");
        _logger.LogInformation($"All users {JsonConvert.SerializeObject(users1)}");
    }
}