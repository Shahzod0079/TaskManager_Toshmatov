using System;
using System.Collections.ObjectModel;
using System.Linq;
using TaskManager_Toshmatov.Classes;
using TaskManager_Toshmatov.Context;
using TaskManager_Toshmatov.Models;

namespace TaskManager_Toshmatov.ViewModels
{
    public class VM_Tasks : Notification
    {
        public TasksContext tasksContext = new TasksContext();

        public ObservableCollection<Tasks> Tasks { get; set; }

        public VM_Tasks()
        {
            Tasks = new ObservableCollection<Tasks>(tasksContext.Tasks.OrderBy(x => x.Done));
        }

        public RealyCommand OnAddTask
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