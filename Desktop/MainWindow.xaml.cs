namespace Desktop
{
    using Newtonsoft.Json;
    using System.Configuration;
    using System;
    using System.IO;
    using System.Windows;
    
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

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string poolsPath = ConfigurationSettings.AppSettings.Get("poolsTextFile");
            Settings items = ReadJson();

            items.pool_list[0].wallet_address = Wallet.Text;

            using (StreamWriter file = File.CreateText(poolsPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, items);
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
            string poolsPath = ConfigurationSettings.AppSettings.Get("poolsTextFile");
            string json = string.Empty;
            Settings items = null;
            using (StreamReader r = new StreamReader(poolsPath))
            {
                json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Settings>(json);
            }

            return items;
        }
    }
}
