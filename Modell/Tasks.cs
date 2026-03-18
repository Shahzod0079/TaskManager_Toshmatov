using TaskManager_Toshmatov.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager_Toshmatov.ViewModels;
using System.Xml.Linq;

namespace TaskManager_Toshmatov.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                {
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов.",
                        "Некорректный ввод значения.");
                }
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string priority;
        public string Priority
        {
            get { return priority; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
                {
                    MessageBox.Show("Приоритет не должен быть пустым, и не более 30 символов.",
                        "Некорректный ввод значения.");
                }
                else
                {
                    priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }

        private DateTime dateExecute;
        public DateTime DateExecute
        {
            get { return dateExecute; }
            set
            {
                if (value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Дата выполнения не может быть меньше текущей.",
                        "Некорректный ввод значения.");
                }
                else
                {
                    dateExecute = value;
                    OnPropertyChanged("DateExecute");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length > 1000)
                {
                    MessageBox.Show("Комментарий не более 1000 символов.",
                        "Некорректный ввод значения.");
                }
                else
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private bool done;
        public bool Done
        {
            get { return done; }
            set
            {
                done = value;
                OnPropertyChanged("Done");
                OnPropertyChanged("IsDoneText");
            }
        }

        private bool isEnable;
        [NotMapped]
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";
            }
        }

        [NotMapped]
        public string IsDoneText
        {
            get
            {
                if (Done) return "Выполнено";
                else return "Не выполнено";
            }
        }

        [NotMapped]
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsEnable = !IsEnable;

                    if (!IsEnable)
                    {
                        var vm = MainWindow.init.DataContext as VM_Pages;
                        vm?.vm_Tasks.tasksContext.SaveChanges();
                    }
                });
            }
        }

        [NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить задачу?",
                        "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var vm = MainWindow.init.DataContext as VM_Pages;
                        if (vm != null)
                        {
                            vm.vm_Tasks.Tasks.Remove(this);
                            vm.vm_Tasks.tasksContext.Tasks.Remove(this);
                            vm.vm_Tasks.tasksContext.SaveChanges();
                        }
                    }
                });
            }
        }

        [NotMapped]
        public RealyCommand OnDone
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Done = !Done;
                });
            }
        }
    }
}