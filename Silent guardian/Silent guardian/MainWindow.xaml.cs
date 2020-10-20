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
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace Silent_guardian
{
    public partial class MainWindow : Window
    {

        string settings = "settings3.json";
        int max_columns = 10;
        int max_rows = 5;

        //List<Entity> Entities;
        List<GroupControl> GroupControls;

        public MainWindow()
        {
            InitializeComponent();
            GetObjects();
           // CreateGrid();
            PopulateGrid();
           // StartTest();
        }

        public void StartTest()
        {
            //foreach (Entity entity in Entities)
            //{
            //    entity.StartTest();
            //}
        }

        public void PopulateGrid()
        {
            int r = 0;
            foreach (GroupControl group in GroupControls)
            {
                //RowDefinition row = new RowDefinition();
                //row.Height = new GridLength(1, GridUnitType.Star);
                //GroupGrid.RowDefinitions.Add(row);
                GroupGrid.Children.Add(group);
                //Grid.SetRow(group, r);
                //r++;
            }
        }

        public void CreateGrid()
        {
            
            for (int r = 0; r < max_rows; r++)
            {
                RowDefinition row = new RowDefinition();
                //row.Height = new GridLength(70, GridUnitType.Pixel);
               // MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int c = 0; c < max_columns; c++)
            {
                ColumnDefinition col = new ColumnDefinition();
              //  col.Width = new GridLength(120, GridUnitType.Pixel);
               //MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
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
    }
}
