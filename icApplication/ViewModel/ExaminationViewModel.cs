﻿using icModel.Abstract;
using icModel.Model.Entities;
using icModel.Model.Keys;

namespace icApplication.ViewModel
{
    public partial class ExaminationViewModel : ViewModelBase
    {
        public ExaminationViewModel()
        {
            Number = "2";
            Message = "ARBAIT";
            ExamKey = new AffineKey(1, 5);
        }

        private ExaminationVariant _examinationVariant;
        public ExaminationVariant ExaminationVariant
        {
            get { return _examinationVariant; }
            set
            {
                _examinationVariant = value;
                base.NotifyPropertyChanged("ExaminationVariant");
            }
        }

        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                base.NotifyPropertyChanged("Number");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                base.NotifyPropertyChanged("Message");
            }
        }

        private ICryptoKey _key;
        public ICryptoKey ExamKey
        {
            get { return _key; }
            set
            {
                _key = value;
                base.NotifyPropertyChanged("ExamKey");
            }
        }
    }
}