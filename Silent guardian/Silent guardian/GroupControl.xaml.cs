using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Silent_guardian
{
    public partial class GroupControl : UserControl
    {

        public Group group;

       // Timer timer;
        public GroupControl(Group group)
        {
            InitializeComponent();
            this.group = group;
            GroupName.Content = this.group.Name;

            foreach (Endpoint endpoint in group.endpoint)
            {
                Entity entity = new Entity(endpoint);
                GroupGrid.Children.Add(entity);
                entity.StartTest();
            }

            //timer = new System.Threading.Timer(timer_elapsed);
            //timer.Change(15000, 15000);

        }

        //private void timer_elapsed(object state)
        //{
        //    this.Dispatcher.Invoke(() => {

        //        foreach (Entity entity in GroupGrid.Children)
        //            if (!entity.result)
        //            {
        //                GroupName.Style = (Style)Resources["GroupNameNOK"];
        //                break;
        //            }
        //            else
        //            {
        //                GroupName.Style = (Style)Resources["GroupName"];
        //            }

        //    });
            


        //}
       
    }
}
