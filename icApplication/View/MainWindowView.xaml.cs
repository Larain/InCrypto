using System.Collections.Generic;
using System.Linq;
using System.Windows;
using icApplication.ViewModel;

namespace icApplication.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            MainWindowViewModel mwModel = new MainWindowViewModel();
            DataContext = mwModel;
        }
    }
}
