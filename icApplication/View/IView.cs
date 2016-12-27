using System.Windows.Controls;

namespace icApplication.View {
    public interface IView {
        TextBox[,] UpdateMatrix(int[,] matrix);
        TextBox[,] UpdateMatrix(int size);
    }
}