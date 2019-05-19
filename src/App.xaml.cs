using savaged.Grapher.ViewModel;
using System.Threading;
using System.Windows;

namespace savaged.Grapher
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var splashWin = new Splash
            {
                DataContext = new SplashViewModel()
            };
            splashWin.Show();
            Thread.Sleep(5000);
            
            var mainWin = new MainWindow
            {
                DataContext = new MainViewModel()
            };

            splashWin.Hide();

            mainWin.Show();
        }
    }
}
