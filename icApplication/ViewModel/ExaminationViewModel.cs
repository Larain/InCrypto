using System.Collections.Generic;
using System.Collections.ObjectModel;
using icApplication.Exmaination;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Keys;

namespace icApplication.ViewModel {
    public partial class ExaminationViewModel : ViewModelBase {

        private List<ExaminationVariant> _examVariants;
        private ExaminationVariant _examinationVariant;
        private ExaminationManager _examinationManager;

        public ExaminationViewModel() {
            CreateVariants(10);
            //Number = "2";
            //Message = "ARBAIT";
            //ExamKey = new AffineKey(1, 5);
        }

        public ExaminationVariant ExaminationVariant {
            get { return _examinationVariant; }
            set {
                _examinationVariant = value;
                base.NotifyPropertyChanged("ExaminationVariant");
            }
        }

        public List<ExaminationVariant> ExaminationVariantCollection
        {
            get { return _examVariants?? (_examVariants = _examinationManager.VariantsList); }
            set {
                _examVariants = value;
                base.NotifyPropertyChanged("ExaminationVariantCollection");
            }
        }

        private void CreateVariants(int amount) {
            _examinationManager = new ExaminationManager(amount);
        }

        //private string _number;

        //public string Number {
        //    get { return _number; }
        //    set {
        //        _number = value;
        //        base.NotifyPropertyChanged("Number");
        //    }
        //}

        //private string _message;

        //public string Message {
        //    get { return _message; }
        //    set {
        //        _message = value;
        //        base.NotifyPropertyChanged("Message");
        //    }
        //}

        //private ICryptoKey _key;

        //public ICryptoKey ExamKey {
        //    get { return _key; }
        //    set {
        //        _key = value;
        //        base.NotifyPropertyChanged("ExamKey");
        //    }
        //}
    }
}