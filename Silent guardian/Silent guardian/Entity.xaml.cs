using System;
using System.Collections.Generic;
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
using System.Net.NetworkInformation;
using System.Threading;
using System.Security.RightsManagement;

namespace Silent_guardian
{
    public partial class Entity : UserControl
    {

        public Endpoint endpoint;
        System.Timers.Timer timer;

        public bool result;

        public Entity(Endpoint endpoint)
        {
            InitializeComponent();

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;

            timer.Interval = GlobVars.PingTimer > 0 ? GlobVars.PingTimer: 30000;

            this.endpoint = endpoint;
            lblname.Content = string.IsNullOrEmpty(endpoint.name) ? "[No name]" : endpoint.name;
            lbllocation.Content = string.IsNullOrEmpty(endpoint.location) ? "[No location]" : endpoint.location;
            lbllocation.ToolTip = string.IsNullOrEmpty(endpoint.description) ? "" : endpoint.description;

            if (!string.IsNullOrEmpty(endpoint.leftaction))
            {
                entitygrid.MouseLeftButtonDown += Entitygrid_MouseLeftButtonDown;
                endpoint.leftaction = endpoint.leftaction.Contains("{location}") ? endpoint.leftaction.Replace("{location}", endpoint.location) : endpoint.leftaction;                    
            }

        }

        private void Entitygrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) 
            { 
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C " + endpoint.leftaction;
                process.StartInfo = startInfo;
                process.Start();
            }
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;
            await Task.Run(TestConnection);
            timer.Enabled = true;
        }

        public void StartTest()
        {
            this.Timer_Elapsed(this, null);
            timer.Enabled = true;      
        }

        public void TestConnection()
        {
            bool IsConnected = false;
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(endpoint.location, 1000);

                if (reply.Status == IPStatus.Success)
                    IsConnected = true;

                entitygrid.Dispatcher.Invoke(() =>
                {
                    entitygrid.Style = IsConnected ? Resources["entityfullOK"] as Style : Resources["entityfullNOK"] as Style;
                }
                );

                lbltime.Dispatcher.Invoke(() =>
                {
                    lbltime.Content = IsConnected ? DateTime.Now.ToString("HH:mm:ss") : lbltime.Content; 
                });
            } catch (Exception ex)
            {
                entitygrid.Dispatcher.Invoke(() =>
                {
                    entitygrid.Style = Resources["entityfull"] as Style;
                }
                );
                FileLogger logs = new FileLogger();
                logs.logError(ex.Message);
            }
            finally
            {
                result = IsConnected;
            }
        }
    }
}
