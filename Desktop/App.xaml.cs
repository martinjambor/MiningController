using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Thread th;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            th.Abort();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool aIsNewInstance = false;
            Mutex myMutex = new Mutex(true, "Desktop", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("Already an instance is running...");
                App.Current.Shutdown();
            }

            ProcessChecker tp = new ProcessChecker();
            th = new Thread(new ThreadStart(tp.Start));
            th.Start();
        } 
    }
}
