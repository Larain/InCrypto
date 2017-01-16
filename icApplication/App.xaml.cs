using icApplication.View;
using System.Windows;

namespace icApplication {
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            var mw = new MainWindowView();
            mw.Show();
        }
    }
}