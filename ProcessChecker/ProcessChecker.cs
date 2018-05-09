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
            // we're going to wait 5 minutes between calls to GetEmployees, so 
            // set the interval to 300000 milliseconds 
            // (1000 milliseconds = 1 second, 5 * 60 * 1000 = 300000)
            int interval = 10000; // 5 minutes    
            // this variable tracks how many milliseconds have gone by since 
            // the last call to GetEmployees. Set it to zero to indicate we're 
            // starting fresh
            int elapsed = 0;
            // because we don't want to use 100% of the CPU, we will be 
            // sleeping for 1 second between checks to see if it's time to 
            // call GetEmployees
            int waitTime = 1000; // 1 second
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
                        if (localByName.Length == 0)
                        {
                           Process.Start(ConfigurationManager.AppSettings.Get("ifNotRunningBatFile"));
                        }
                        else
                        {
                           Process.Start(ConfigurationManager.AppSettings.Get("ifRunningBatFile"));
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

