using TaskSystem.Models;

namespace TaskSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> SearchAllUser();
        Task<UserModel> FindById(int id);
        Task<UserModel> AddUser(UserModel user);
        Task<UserModel> UpdateUser(UserModel user, int id);
        Task<bool> DeleteUser(int id);
    }
}
