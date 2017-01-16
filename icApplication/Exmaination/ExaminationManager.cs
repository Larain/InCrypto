using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;

namespace icApplication.Exmaination {
    class ExaminationManager {
        private Dictionary<int, ExaminationVariant> _variants;
        IAlphabet _alphabet;

        /// <summary>
        /// Set amount of generated variants
        /// </summary>
        /// <param name="variantsAmount"></param>
        public ExaminationManager(int variantsAmount) {
            _alphabet = new SimpleAlphabet();
            GenerateNewVariants(variantsAmount);

        }

        public List<ExaminationVariant> VariantsList {
            get { return _variants.Values.ToList(); }
        }

        private void GenerateNewVariants(int variantsAmount) {
            Random rnd = new Random();
            Dictionary<int, ExaminationVariant> varList = new Dictionary<int, ExaminationVariant>();

            for (int i = 1; i < variantsAmount + 1; i++) {
                ExaminationVariant var = new ExaminationVariant(i) {
                    Text = CryptoHelper.RandomString(5, _alphabet),
                    Key = new AffineKey(rnd.Next(1, 100), rnd.Next(1, 100))
                };
                varList.Add(i, var);
            }

            _variants = varList;
        }

        public void Add(ExaminationVariant variant) {
            _variants.Add(_variants.Count, variant);
        }

        public void Update(int number, ExaminationVariant variant) {
            _variants[number] = variant;
        }

        public void Remove(int number) {
            _variants.Remove(number);
        }
    }
}