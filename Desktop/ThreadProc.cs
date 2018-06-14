using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desktop
{
    public class ProcessChecker
    {
        public void Start()
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

                        if (!File.Exists("process.setfile"))
                        {
                            File.Create("process.setfile");
                        }

                        string[] processes = File.ReadAllLines("process.setfile");

                        bool isGamingProcessRuning = false;
                        foreach (string process in processes)
                        {
                            Process[] localByName = Process.GetProcessesByName(process);
                            
                            if (localByName.Length != 0)
                            {
                                isGamingProcessRuning = true;
                                break;
                            }

                        }                  

                            if (!isGamingProcessRuning && gamingModeOn == true)
                            {
                                Process p = new Process();
                                p.StartInfo.CreateNoWindow = false;
                                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                p.StartInfo.FileName = ConfigurationSettings.AppSettings.Get("ifNotRunningBatFile");
                                p.Start();
                                gamingModeOn = false;
                            }


                            if (isGamingProcessRuning && gamingModeOn == false)
                            {
                                Process.Start(ConfigurationSettings.AppSettings.Get("ifRunningBatFile"));
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
