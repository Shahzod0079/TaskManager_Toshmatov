using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager_Toshmatov.Classes;
using TaskManager_Toshmatov.Context;

namespace TaskManager_Toshmatov.ViewModels
{
    public class VM_Tasks : Motification
    {
        public TasksContext tasksContext = new TasksContext();

        public ObservableCollectionExtensions<Tasks> Tasks {get; set;}

        public VM_Tasks() => 
            Tasks = new ObservableCollectionExtensions<Tasks>(tasksContext.Tasks.OrderBy(x => x.done));

        public RealyCommand onAddTask
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Tasks newTask = new Tasks()
                    {
                        DateExecute = DateTime.Now
                    };
                    Tasks.Add(newTask);
                    tasksContext.Tasks.Add(newTask);
                    tasksContext.SaveChanges();
                });
            }
        }
    }
}
