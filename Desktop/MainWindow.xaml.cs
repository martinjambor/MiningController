using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Desktop
{
    using Newtonsoft.Json;
    using System.Configuration;
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
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            string poolsPath = ConfigurationSettings.AppSettings.Get("poolsTextFile");
            string json = string.Empty;
            Settings items = null;
            using (StreamReader r = new StreamReader(poolsPath))
            {
                json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<Settings>(json);
            }

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

            base.OnStateChanged(e);
        }
    }
}
