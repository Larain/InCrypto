using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace icApplication.ViewModel {
    public class ViewModelBase : INotifyPropertyChanged {
        #region Property changed

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}