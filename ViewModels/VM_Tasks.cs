using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TaskManager_Toshmatov.Classes;
using TaskManager_Toshmatov.Context;
using TaskManager_Toshmatov.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManager_Toshmatov.ViewModels
{
    public class VM_Tasks : Notification
    {
        public TasksContext tasksContext = new TasksContext();
        public ObservableCollection<Tasks> Tasks { get; set; }
        public ObservableCollection<Priority> Priorities { get; set; }

        private ObservableCollection<Tasks> allTasks;
        private string searchText;
        private Priority selectedPriority;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                ApplyFilters();
            }
        }

        public Priority SelectedPriority
        {
            get { return selectedPriority; }
            set
            {
                selectedPriority = value;
                OnPropertyChanged("SelectedPriority");
                ApplyFilters();
            }
        }

        public VM_Tasks()
        {
            tasksContext.Database.EnsureCreated();

            // Загружаем с учетом связей
            allTasks = new ObservableCollection<Tasks>(
                tasksContext.Tasks
                    .Include(t => t.Priority)
                    .OrderBy(t => t.Done)
                    .ThenBy(t => t.Priority.Level)
                    .ToList());

            Tasks = new ObservableCollection<Tasks>(allTasks);

            // Загружаем приоритетов
            Priorities = new ObservableCollection<Priority>(
                tasksContext.Priorities.OrderBy(p => p.Level).ToList());
        }

        public void ApplyFilters()
        {
            var filtered = allTasks.AsEnumerable();

            // Фильтр по поиску
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(x =>
                    x.Name != null &&
                    x.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // Фильтр по приоритету
            if (SelectedPriority != null)
            {
                filtered = filtered.Where(x => x.PriorityId == SelectedPriority.Id);
            }

            Tasks.Clear();
            foreach (var task in filtered)
            {
                Tasks.Add(task);
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
                            DateExecute = DateTime.Now,
                            PriorityId = 2 
                        };

                        Tasks.Add(newTask);
                        allTasks.Add(newTask);
                        tasksContext.Tasks.Add(newTask);
                        tasksContext.SaveChanges();

                        // Обновляем приоритет для нового объекта
                        newTask.Priority = tasksContext.Priorities.Find(newTask.PriorityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
                    }
                });
               
            }            
        }
        public RealyCommand OnSearch
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    SearchText = string.Empty;
                    SelectedPriority = null;
                });
            }
        }
    }
}