using icModel.Abstract;

namespace icApplication.ViewModel.Interface {
    public interface ICryptoView {
        void SetCryptoKey(ICryptoKey key);
        void SetEncryptoText(string text);
        void SendMessage(string text);
    }
}