using GalaSoft.MvvmLight;
using savaged.Grapher.Model;

namespace savaged.Grapher.ViewModel
{
    public class SplashViewModel : ViewModelBase
    {
        public SplashViewModel()
        {
            Circle = new Circle();
        }

        public Circle Circle { get; }
    }
}
