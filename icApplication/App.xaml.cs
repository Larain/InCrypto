using icApplication.View;
using icApplication.ViewModel;
using System.Windows;

namespace icApplication
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var mw = new MainWindowView
            {
                DataContext = new MainWindowViewModel()
            };

            mw.Show();
        }
    }
}
