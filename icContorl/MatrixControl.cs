using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace icContorl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:icContorl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:icContorl;assembly=icContorl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class MatrixControl : Selector {
        private int[,] _matrix;
        private TextBox[] _inputs;
        DataTable _table;
        private DataGridRow[] _rows;
        private DataGridColumn[] _columns;

        static MatrixControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatrixControl), new FrameworkPropertyMetadata(typeof(MatrixControl)));
            MatrixSizeProperty = DependencyProperty.Register("MatrixSize", typeof(int), typeof(MatrixControl),
                new FrameworkPropertyMetadata(2, new PropertyChangedCallback(OnMatrixSizeChanged)));
            IsFixedProperty = DependencyProperty.Register("IsFixed", typeof(bool), typeof(MatrixControl),
                new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnIsFixedChanged)));
        }

        public static DependencyProperty MatrixSizeProperty;
        public int MatrixSize
        {
            get { return (int)GetValue(MatrixSizeProperty); }
            set { SetValue(MatrixSizeProperty, value); }
        }
        private static void OnMatrixSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            MatrixControl control = (MatrixControl)d;

            _table = new DataTable();
            for (int i = 0; i < control.MatrixSize; i++)
            {
                table.Columns.Add(i.ToString(), typeof(double));
            }

            for (int row = 0; row < control.MatrixSize; row++)
            {
                DataRow dr = table.NewRow();
                for (int col = 0; col < control.MatrixSize; col++)
                {
                    TextBox tb = new TextBox();
                    dr[col] = tb;
                }
                table.Rows.Add(dr);
            }

            //myDataGrid.ItemsSource = dt.DefaultView;
        }

        public static DependencyProperty IsFixedProperty;
        public bool IsFixed
        {
            get { return (bool)GetValue(IsFixedProperty); }
            set { SetValue(IsFixedProperty, value); }
        }
        private static void OnIsFixedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }



    }
}
