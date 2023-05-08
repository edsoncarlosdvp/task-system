using Microsoft.EntityFrameworkCore;
using TaskSystem.Data;
using TaskSystem.Models;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskSystemDBContext _dbContext;

        public UserRepository(TaskSystemDBContext taskSystemDBContext) {
            _dbContext = taskSystemDBContext;
        }

        public async Task<List<UserModel>> SearchAllUser()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> FindById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> UpdateUser(UserModel user, int id)
        {
            UserModel userById = await FindById(id);

            if (userById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados!");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel userById = await FindById(id);

            if (userById == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados!");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
