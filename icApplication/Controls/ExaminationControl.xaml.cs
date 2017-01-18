using System;
using System.Windows;
using System.Windows.Controls;
using icModel.Abstract;

namespace icApplication.Controls {
    /// <summary>
    /// Interaction logic for ExaminationControl.xaml
    /// </summary>
    public partial class ExaminationControl : UserControl {
        public static DependencyProperty NumberProperty;
        public static DependencyProperty MessageProperty;
        public static DependencyProperty KeyProperty;

        static ExaminationControl() {
            NumberProperty = DependencyProperty.Register("Number", typeof (String),
                typeof (ExaminationControl), new FrameworkPropertyMetadata(null));

            MessageProperty = DependencyProperty.Register("Message", typeof (String),
                typeof (ExaminationControl), new FrameworkPropertyMetadata(null));

            KeyProperty = DependencyProperty.Register("Key", typeof (ICryptoKey),
                typeof (ExaminationControl), new FrameworkPropertyMetadata(null));
        }

        //ObservableCollection<List<int>> Try

        public ExaminationControl() {
            InitializeComponent();
        }


        public String Number {
            get { return GetValue(NumberProperty).ToString(); }
            set { SetValue(NumberProperty, value); }
        }


        public String Message {
            get { return GetValue(MessageProperty).ToString(); }
            set { SetValue(MessageProperty, value); }
        }

        public ICryptoKey Key {
            get { return (ICryptoKey) GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
    }
}