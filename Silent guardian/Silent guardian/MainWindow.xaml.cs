﻿using System;
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
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace Silent_guardian
{
    public partial class MainWindow : Window
    {

        string settings = "settings.json";
        int max_columns = 10;
        int max_rows = 5;

        List<GroupControl> GroupControls;

        public MainWindow()
        {
            InitializeComponent();
            GetObjects();
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            foreach (GroupControl group in GroupControls)
                GroupGrid.Children.Add(group);
        }

        public void GetObjects()
        {
            JObject FullConfig = JObject.Parse(File.ReadAllText(settings));
            IList<JToken> groups = FullConfig["Group"].Children().ToList();

            GroupControls = new List<GroupControl>();

            foreach (JToken jgroup in groups)
            {
                Group group = jgroup.ToObject<Group>();
                GroupControl groupControl = new GroupControl(group);
                GroupControls.Add(groupControl);
            }
        }

        private void btnMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMax_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AdjustWindowSize();
        }

        private void btnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                btnMax.Content = "^";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                btnMax.Content = "\\/";
            }

        }

        private void lblApp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
        }
    }
}
