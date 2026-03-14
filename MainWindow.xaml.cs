using System.Windows;
using TaskManager_Toshmatov.ViewModels;

namespace TaskManager_Toshmatov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            DataContext = new VM_Pages();
        }
    }
}
