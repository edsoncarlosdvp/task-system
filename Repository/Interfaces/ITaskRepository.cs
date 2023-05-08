using TaskSystem.Models;

namespace TaskSystem.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> SearchAllTask();
        Task<TaskModel> FindById(int id);
        Task<TaskModel> AddTask(TaskModel task);
        Task<TaskModel> UpdateTask(TaskModel task, int id);
        Task<bool> DeleteTask(int id);
    }
}
