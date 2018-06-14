namespace Desktop
{
    using Newtonsoft.Json;
    using System.Configuration;
    using System;
    using System.IO;
    using System.Windows;
    using System.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Icons\\main.ico");
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };

            Settings settings = ReadJson();
            if (settings != null)
            {
                Wallet.Text = settings.pool_list[0].wallet_address;
            }

            if (!File.Exists("process.setfile"))
            {
                File.Create("process.setfile");
            }

            string[] processes = File.ReadAllLines("process.setfile");

            foreach (string process in processes)
            {
                Processes.Items.Add(process);
            }

            this.Hide();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string poolsPath = ConfigurationSettings.AppSettings.Get("poolsValidTextFile");
            Settings settings = ReadJson();

            settings.pool_list[0].wallet_address = Wallet.Text;

            using (StreamWriter file = File.CreateText(poolsPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, settings);


                StringWriter sw = new StringWriter();
                serializer.Serialize(sw, settings);
                string jsonString = sw.ToString();

                jsonString = jsonString.Replace("\"pool_weight\":1}", "\"pool_weight\":1},");
                jsonString = jsonString.Substring(1, jsonString.Length - 2);
                jsonString = jsonString + ",";

                File.WriteAllText(ConfigurationSettings.AppSettings.Get("poolsTextFile"), jsonString);


                MessageBox.Show("Wallet settings updated.");
            }

            using (StreamWriter file = new StreamWriter("process.setfile"))
            {
                foreach (string process in Processes.Items)
                {
                    file.WriteLine(process);
                }
            }

        }

        private void InitializeValues()
        {

        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            Settings settings = ReadJson();
            if (settings != null)
            {
                Wallet.Text = settings.pool_list[0].wallet_address;
            }

            base.OnStateChanged(e);
        }

        private Settings ReadJson()
        {
            string poolsPath = ConfigurationSettings.AppSettings.Get("poolsValidTextFile");
            string json = string.Empty;
            Settings items = null;
            using (StreamReader r = new StreamReader(poolsPath))
            {
                json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Settings>(json);
            }

            return items;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var processName = Process.Text.Trim();
            if (!string.IsNullOrEmpty(processName) && !Processes.Items.Contains(processName))
            {
                Processes.Items.Add(processName);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Processes.Items.Remove(Processes.SelectedItem);
        }
    }
}
