namespace ProcessChecker
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcessCheckerInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ProcessCheckerInstaller
            // 
            this.ProcessCheckerInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.ProcessCheckerInstaller.Password = null;
            this.ProcessCheckerInstaller.Username = null;
            this.ProcessCheckerInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.ProcessCheckerInstaller_AfterInstall);
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.ServiceName = "ProcessChecker";
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ProcessCheckerInstaller,
            this.serviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ProcessCheckerInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}