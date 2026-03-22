using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TaskManager_Toshmatov.Classes;
using TaskManager_Toshmatov.Context;
using TaskManager_Toshmatov.Models;
using System.Collections.Generic;

namespace TaskManager_Toshmatov.ViewModels
{
    public class VM_Tasks : Notification
    {
        public TasksContext tasksContext = new TasksContext();
        public ObservableCollection<Tasks> Tasks { get; set; }
        private ObservableCollection<Tasks> allTasks; 

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                SearchTasks();
            }
        }

        public VM_Tasks()
        {
            tasksContext.Database.EnsureCreated();
            allTasks = new ObservableCollection<Tasks>(tasksContext.Tasks.OrderBy(x => x.Done));
            Tasks = new ObservableCollection<Tasks>(allTasks);
        }

        public RealyCommand OnSearch
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    SearchTasks();
                });
            }
        }

        private void SearchTasks()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Tasks.Clear();
                foreach (var task in allTasks)
                {
                    Tasks.Add(task);
                }
            }
            else
            {
                var filteredTasks = allTasks.Where(x =>
                    x.Name != null &&
                    x.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();

                Tasks.Clear();
                foreach (var task in filteredTasks)
                {
                    Tasks.Add(task);
                }
            }
        }

        public RealyCommand OnAddTask
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    try
                    {
                        Tasks newTask = new Tasks()
                        {
                            DateExecute = DateTime.Now
                        };

                        Tasks.Add(newTask);
                        allTasks.Add(newTask);
                        tasksContext.Tasks.Add(newTask);
                        tasksContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                    }
                });
            }
        }
    }
}