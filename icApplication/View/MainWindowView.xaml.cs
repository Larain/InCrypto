using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
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

            mainWindowViewModel.ExaminationView = examinationViewModel;

            examinationViewModel.MainView = mainWindowViewModel;
            examinationViewModel.MatrixView = matrixValidationViewModel;
            matrixValidationViewModel.MainView = mainWindowViewModel;

            Tab1.DataContext = mainWindowViewModel;
            Tab2.DataContext = examinationViewModel;
            Tab3.DataContext = matrixValidationViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                UniformGrid myCombo = GetVisualChild<UniformGrid>(PrintableVariantsListBox);
                var oldCols = myCombo.Columns;
                //myCombo.Columns = 2;

                printDialog.PrintVisual(myCombo, "Print test variants");
                //myCombo.Columns = oldCols;
            }
        }

        private T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}