using TaskManager_Toshmatov.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_Toshmatov.Models
{
    public class Priority : Notification
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }

        public virtual ObservableCollection<Tasks> Tasks { get; set; }

        public Priority()
        {
            Tasks = new ObservableCollection<Tasks>();
        }
    }
}