using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncTaskHandler
{
    public class TaskListener : List<Func<Task>>
    {
        public int Order { get; private set; }
        public Func<Task> Action { get; private set; }
        public bool IsImportant { get; private set; }

        public TaskListener(int order,bool isImportant, Func<Task> action)
        {
            Order = order;
            Action = action;
            IsImportant = isImportant;
        }
        public TaskListener()
        {

        }
        public TaskListener(bool isImportant)
        {
            IsImportant = isImportant;
        }
    }
}
