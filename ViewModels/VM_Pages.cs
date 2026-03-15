using TaskManager_Toshmatov.Classes;

namespace TaskManager_Toshmatov.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Tasks vm_Tasks = new VM_Tasks();

        public VM_Pages() {
            MainWindow.init.frame.Navigate(new Views.Main(vm_Tasks));

        }
        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.Close();
                });
            }
        }

    }
}
