using System.Windows;
using icApplication.ViewModel;

namespace icApplication.View {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window {
        public MainWindowView() {
            InitializeComponent();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            ExaminationViewModel examinationViewModel = new ExaminationViewModel();
            MatrixValidationViewModel matrixValidationViewModel = new MatrixValidationViewModel();

            examinationViewModel.MainView = mainWindowViewModel;
            matrixValidationViewModel.MainView = mainWindowViewModel;

            Tab1.DataContext = mainWindowViewModel;
            Tab2.DataContext = examinationViewModel;
            Tab3.DataContext = matrixValidationViewModel;
        }
    }
}