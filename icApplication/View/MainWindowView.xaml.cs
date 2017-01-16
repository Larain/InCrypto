using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            List<List<int>> lsts = new List<List<int>>();

            for (int i = 0; i < 5; i++)
            {
                lsts.Add(new List<int>());

                for (int j = 0; j < 5; j++)
                {
                    lsts[i].Add(i * 10 + j);
                }
            }

            int[][] arrays = lsts.Select(a => a.ToArray()).ToArray();

            InitializeComponent();

            //Matrix.ItemsSource = arrays;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            
        }
    }
}
