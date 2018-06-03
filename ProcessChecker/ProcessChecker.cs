using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessChecker
{
    public partial class ProcessChecker : ServiceBase
    {
        public ProcessChecker()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadProc();
        }

        protected override void OnStop()
        {
        }

        public void ThreadProc()
        {
            int interval = 10000;  
            int elapsed = 0;
            int waitTime = 1000;
            bool gamingModeOn = false;

            try
            {
                // do this loop forever (or until the service is stopped)
                while (true)
                {
                    // if enough time has passed
                    if (interval >= elapsed)
                    {
                        // reset how much time has passed to zero
                        elapsed = 0;
                        Process[] localByName = Process.GetProcessesByName(ConfigurationManager.AppSettings.Get("checkedProcessName"));
                        if (localByName.Length == 0 && gamingModeOn == true)
                        {
                           Process.Start(ConfigurationManager.AppSettings.Get("ifNotRunningBatFile"));
                            gamingModeOn = false;
                        }


                        if (localByName.Length > 0 && gamingModeOn == false)
                        {
                           Process.Start(ConfigurationManager.AppSettings.Get("ifRunningBatFile"));
                            gamingModeOn = true;
                        }
                    }
                    // Sleep for 1 second
                    Thread.Sleep(waitTime);
                    // indicate that 1 additional second has passed
                    elapsed += waitTime;
                }
            }
            catch (ThreadAbortException)
            {
            }
        }
    }
}

