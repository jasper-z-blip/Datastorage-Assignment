using Data.Entities;
using Data.Repositories;


namespace WebApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<UserEntity?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task AddUserAsync(UserEntity user)
    {
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        await _userRepository.DeleteAsync(id);
        return true;
    }
}
// Har byggt upp och tanken är att införa en USER i applikationen om jag hinner.