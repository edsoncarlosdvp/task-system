using Microsoft.EntityFrameworkCore;
using TaskSystem.Data;
using TaskSystem.Models;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskSystemDBContext _dbContext;

        public TaskRepository(TaskSystemDBContext taskSystemDBContext) {
            _dbContext = taskSystemDBContext;
        }

        public async Task<List<TaskModel>> SearchAllUser()
        {
            return await _dbContext.tasks.Include(user => user.user).ToListAsync();
        }

        public async Task<TaskModel> FindById(int id)
        {
            return await _dbContext.tasks.Include(user => user.user).FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<TaskModel> AddUser(TaskModel task)
        {
            await _dbContext.tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> UpdateUser(TaskModel task, int id)
        {
            TaskModel taskById = await FindById(id);

            if (taskById == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados!");
            }

            taskById.Name = task.Name;
            taskById.description = task.description;
            taskById.status = task.status;
            taskById.userId = task.userId;

            _dbContext.tasks.Update(taskById);
            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> DeleteUser(int id)
        {
            TaskModel taskById = await FindById(id);

            if (taskById == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados!");
            }

            _dbContext.tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
