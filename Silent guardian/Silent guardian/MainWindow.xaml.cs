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

        string settings = "settings.json";
        int max_columns = 7;
        int max_rows = 5;

        List<Entity> Entities;
        public MainWindow()
        {
            InitializeComponent();
            GetObjects();
            CreateGrid();
            PopulateGrid();
            StartTest();
        }

        public void StartTest()
        {
            foreach (Entity entity in Entities)
            {
                entity.StartTest();
            }
        }

        public void PopulateGrid()
        {
            int r = 0, c = 0;

            foreach (Entity entity in Entities)
            {

                MainGrid.Children.Add(entity);
                Grid.SetColumn(entity, c);
                Grid.SetRow(entity, r);

                c = c < max_columns ? c+1 : 0;
                r = c == max_columns ? r+1 : r;

            }
        }

        public void CreateGrid()
        {
            
            for (int r = 0; r < max_rows; r++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int c = 0; c < max_columns; c++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(2, GridUnitType.Star);
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void GetObjects()
        {
            JObject FullConfig = JObject.Parse(File.ReadAllText(settings));
            IList<JToken> endpoints = FullConfig["Endpoint"].Children().ToList();

            // IList<Endpoint> EndPoints = new List<Endpoint>();

            Entities = new List<Entity>();

            foreach (JToken jEndPoint in endpoints)
            {
                Endpoint endpoint = jEndPoint.ToObject<Endpoint>();
                Entity entity = new Entity(endpoint);
                //entity.Style = Resources["entityfull"] as Style;
                Entities.Add(entity);
            }
        }
    }
}
