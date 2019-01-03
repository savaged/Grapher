using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using savaged.Grapher.Model;

namespace savaged.Grapher.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Curve = new Curve();
            ShowCmd = new RelayCommand(OnShow);
        }

        public RelayCommand ShowCmd { get; }

        public Curve Curve { get; }        

        private void OnShow()
        {
            Curve.Refresh();
        }
    }
}