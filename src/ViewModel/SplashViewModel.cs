using GalaSoft.MvvmLight;
using savaged.Grapher.Model;

namespace savaged.Grapher.ViewModel
{
    public class SplashViewModel : ViewModelBase
    {
        public SplashViewModel()
        {
            Circle = new Circle(20, 30, 50);
        }

        public Circle Circle { get; }
    }
}
