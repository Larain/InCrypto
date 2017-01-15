using System;
using System.Collections.Generic;
using icModel.Model;
using icModel.Model.Entities;

namespace icApplication.Exmaination
{
    class ExaminationManager
    {
        /// <summary>
        /// Set amount of generated variants
        /// </summary>
        /// <param name="variantsAmount"></param>
        public ExaminationManager(int variantsAmount)
        {
            GenerateNewVariants(variantsAmount);
        }

        public Dictionary<int, ExaminationVariant> Variants { get; private set; }

        private void GenerateNewVariants(int variantsAmount)
        {
            Dictionary<int, ExaminationVariant> varList = new Dictionary<int, ExaminationVariant>();

            for (int i = 0; i < variantsAmount; i++)
            {
                ExaminationVariant var = new ExaminationVariant(i);
                varList.Add(i, var);
            }

            Variants = varList;
        }

        public void Add(ExaminationVariant variant)
        {
            Variants.Add(Variants.Count, variant);
        }

        public void Update(int number, ExaminationVariant variant)
        {
            Variants[number] = variant;
        }

        public void Remove(int number)
        {
            Variants.Remove(number);
        }
    }
}
