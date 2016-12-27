using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using icApplication.View;

namespace icApplication.ViewModel {
    public partial class MainWindowViewModel {
        #region Dependency propery

        public static DependencyProperty MatrixSizeProperty = DependencyProperty.Register("MarixSize", typeof (int),
            typeof (MainWindowViewModel),
            new FrameworkPropertyMetadata(3, OnMatrixSizeChanged));

        public int MatrixSize {
            get { return (int) GetValue(MatrixSizeProperty); }
            set { SetValue(MatrixSizeProperty, value); }
        }

        #endregion

        #region Methods

        private static void OnMatrixSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var model = (MainWindowViewModel) d;

            model.View.UpdateMatrix(model.MatrixSize);
        }

        #endregion
    }
}