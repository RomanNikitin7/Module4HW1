using System.Collections.Generic;
using System.Threading.Tasks;
using ALevelSample.Dtos;
using ALevelSample.Dtos.Responses;

namespace ALevelSample.Services.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserById(int id);
    Task<UserResponse> CreateUser(string name, string job);
    Task<UserResponseUpdate> UpdateUserPut(int id, string name, string job);
    Task<UserResponseUpdate> UpdateUserPatch(int id, string name, string job);
    Task<List<UserDto>> GetUsers(string request = "");
    Task<bool> DeleteUser(int id);
    Task<UserRegisterResponce> RegisterUser(string email, string password);
    Task<UserLoginResponse> LoginUser(string email, string password);
}