﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using icModel.Abstract;

namespace icModel.Model.Keys
{
    public class DefaultValidator : ICryptoKeyValidator
    {
        public bool IsValid(ICryptoKey key)
        {
            return IsValid(key.KeyCodes);
        }

        public bool IsValid(ObservableCollection<ObservableCollection<int>> key)
        {
            return key == null;
        }
    }
}