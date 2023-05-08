using System.ComponentModel;

namespace TaskSystem.Enums
{
    public enum TaskStatus
    {
        [Description("To Do")]
        ToDo = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Complete")]
        Complete = 3
    }
}
