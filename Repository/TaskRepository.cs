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

        public async Task<List<TaskModel>> SearchAllTask()
        {
            return await _dbContext.Tasks.Include(task => task.User).ToListAsync();
        }

        public async Task<TaskModel> FindById(int id)
        {
            return await _dbContext.Tasks.Include(task => task.User).FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<TaskModel> AddTask(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> UpdateTask(TaskModel task, int id)
        {
            TaskModel taskById = await FindById(id);

            if (taskById == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados!");
            }

            taskById.Name = task.Name;
            taskById.Description = task.Description;
            taskById.Status = task.Status;
            taskById.UserId = task.UserId;

            _dbContext.Tasks.Update(taskById);
            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> DeleteTask(int id)
        {
            TaskModel taskById = await FindById(id);

            if (taskById == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados!");
            }

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
