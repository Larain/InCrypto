using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using icApplication.ViewModel;

namespace icApplication.View {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : IView {
        TextBox[,] _inputs;

        public MainWindowView() {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            var mainWindowViewModel = DataContext as MainWindowViewModel;
            mainWindowViewModel.View = this as IView;
            UpdateMatrix(3);
        }

        #region Methods

        public TextBox[,] UpdateMatrix(int size) {
            MatrixPanel.Children.Clear();

            _inputs = new TextBox[size, size];
            for (int i = 0; i < size; i++) {
                StackPanel sp = new StackPanel {Orientation = Orientation.Horizontal};
                for (int j = 0; j < size; j++) {
                    TextBox tb = new TextBox {
                        Margin = new Thickness(5),
                        MaxLength = 5,
                        Text = (i + j).ToString(),
                    };
                    _inputs[i, j] = tb;
                    sp.Children.Add(tb);
                }
                MatrixPanel.Children.Add(sp);
            }

            return _inputs;
        }

        public TextBox[,] UpdateMatrix(int[,] matrix) {
            MatrixPanel.Children.Clear();

            var size = matrix.Length;

            _inputs = new TextBox[size, size];
            for (int i = 0; i < size; i++) {
                StackPanel sp = new StackPanel {Orientation = Orientation.Horizontal};
                for (int j = 0; j < size; j++) {
                    TextBox tb = new TextBox {
                        Margin = new Thickness(5),
                        MaxLength = 5,
                        Text = matrix[i, j].ToString(),
                    };
                    _inputs[i, j] = tb;
                    sp.Children.Add(tb);
                }
                MatrixPanel.Children.Add(sp);
            }

            return _inputs;
        }

        #endregion
    }
}
