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

        //public Entity()
        //{
        //    InitializeComponent();
        //}

        public Entity(Endpoint endpoint)
        {
            InitializeComponent();

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 2000;


            this.endpoint = endpoint;
            lblname.Content = string.IsNullOrEmpty(endpoint.name) ? "[No name]" : endpoint.name;
            lbllocation.Content = string.IsNullOrEmpty(endpoint.location) ? "[No location]" : endpoint.location;
            lbllocation.ToolTip = string.IsNullOrEmpty(endpoint.description) ? "" : endpoint.description;
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Task test = TestConnection();
            await test;
        }

        public void StartTest()
        {
            timer.Enabled = true;      
        }

        public void StopTest()
        {
            timer.Enabled = false;
        }

        public async Task TestConnection()
        {
            await Task.Run(() =>
            {
                bool IsConnected = false;
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(endpoint.location, 5000);

                    if (reply.Status == IPStatus.Success)
                        IsConnected = true;

                    entitygrid.Dispatcher.Invoke(() =>
                    {
                        entitygrid.Style = IsConnected ? Resources["entityfullOK"] as Style : Resources["entityfullNOK"] as Style;
                    }
               );
                } catch (Exception ex)
                {
                    entitygrid.Dispatcher.Invoke(() =>
                    {
                        entitygrid.Style = Resources["entityfull"] as Style;
                    }
                    );
                }
            });
        }
    }
}
