using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using icModel.Abstract;
using icModel.Model.Keys;

namespace icApplication.Controls
{
    /// <summary>
    /// Interaction logic for ExaminationControl.xaml
    /// </summary>
    public partial class ExaminationControl : UserControl
    {
        public static DependencyProperty NumberProperty;
        public static DependencyProperty MessageProperty;
        public static DependencyProperty KeyProperty;

        static ExaminationControl()
        {
            NumberProperty = DependencyProperty.Register("Number", typeof(String),
                typeof(ExaminationControl), new FrameworkPropertyMetadata(null));

            MessageProperty = DependencyProperty.Register("Message", typeof(String),
                typeof(ExaminationControl), new FrameworkPropertyMetadata(null));

            KeyProperty = DependencyProperty.Register("Key", typeof(ICryptoKey),
                typeof(ExaminationControl), new FrameworkPropertyMetadata(null));
        }

        //ObservableCollection<List<int>> Try

        public ExaminationControl()
        {
            InitializeComponent();
            //ObservableCollection<ObservableCollection<int>> shit = new ObservableCollection<ObservableCollection<int>>();
            //ObservableCollection<int> fuck = new ObservableCollection<int>();
            //fuck.Add(5);
            //shit.Add(fuck);

            ////Key = new AffineKey(1, 2);
            //MatrixControlInExam.ItemsSource = shit;

            //    List<List<int>> lsts = new List<List<int>>();

            //    for (int i = 0; i < 5; i++)
            //    {
            //        lsts.Add(new List<int>());

            //        for (int j = 0; j < 5; j++)
            //        {
            //            lsts[i].Add(i * 10 + j);
            //        }
            //    }

            //    int[][] arrays = lsts.Select(a => a.ToArray()).ToArray();
            //    int[][] asd = new int[2][];
            //    asd[0] = new int[2] {1, 2};
            //    asd[1] = new int[3] {1, 2, 3};
            //    asd[1][0] = 5;

            //    InitializeComponent();

            //    MatrixControlInExam.ItemsSource = Key.KeyCodes;

        }


        public String Number
        {
            get { return GetValue(NumberProperty).ToString(); }
            set { SetValue(NumberProperty, value); }
        }


        public String Message
        {
            get { return GetValue(MessageProperty).ToString(); }
            set { SetValue(MessageProperty, value); }
        }

        public ICryptoKey Key
        {
            get { return (ICryptoKey)GetValue( KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
    }
}
