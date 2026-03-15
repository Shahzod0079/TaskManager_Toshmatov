using TaskManager_Toshmatov.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_Toshmatov.Modell
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
                Match match = Regex.Match(value, "^.{1,50}$");
                if (match.Success)
                    MessageBox.Show("Наименование не доожно быть пустым, и не более 50 символов.",
                        "Некорректный ввод значения.");
                else
                {
                    name = value;
                    OnPropertyChanged("Name");

                } 
            }
        }
        public string Prority;

        public string Priority
        {
            get { return Priority; }
            set
            {
                Match match = Regex.Match(value, "^.{1,30}$");
                if (match.Success)
                    MessageBox.Show("Приоритет не доожно быть пустым, и не более 30 символов.",
                        "Некорректный ввод значения.");
                else
                {
                    Priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }
        private DateTime DateExecute
        {
            get { return DateExecute; }
            set
            {
                if (value.Date < DateTime.Now.Date)
                    MessageBox.Show("Дата выполнения не может быть меньше текущей.",
                        "Не корректный ввод значения.");
                else
                {
                    DateExecute = value;
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
                Match match = Regex.Match(value, "^.{1,1000}$");
                if (match.Success)
                    MessageBox.Show("Кометарий не доожно быть пустым, и не более 1000 символов.",
                        "Некорректный ввод значения.");
                else
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        public bool done;

        private string Done
        {
            get { return Done; }
            set
            {
                done = value;
                OnPropertyChanged("Done");
                OnPropertyChanged("IsDoneText");
            }
        }

        [Schema.NotMapped]
        private bool isEnable;

        [Schema.NotMapped]

        private bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");

            }
        }
        [Schema.NotMapped]
        
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";

            }
        }
        [Schema.NotMapped]
        public string IsDoneText
        {
            get
            {
                if (Done) return "Не выполнено";
                else return "Выполнено";
            }
        }
        [Schema.NotMapped]

        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsEnable = !IsEnable;

                    if (!IsEnable)
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.SaveChanges();
                });
            }
        }
        [Schema.NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить задачу?",
                        "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.Tasks.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.SaveChanges(this);

                    }
                });
            }
        }
        [Schema.NotMapped]

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
