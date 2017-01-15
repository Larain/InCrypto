using icModel.Abstract;

namespace icModel.Model.Entities
{
    public class ExaminationVariant
    {
        public ExaminationVariant(int number)
        {
            Number = number;
        }
        public int Number { get; private set; }
        public ICryptoKey Key { get; set; }
        public string Text { get; set; }
    }
}