using System;
using System.Xml.Serialization;
using icModel.Abstract;

namespace icModel.Model.Entities
{
    [Serializable]
    public class ExaminationVariant
    {
        public int Number { get; set; }
        public ICryptoKey Key { get; set; }
        public string Text { get; set; }
    }
}