using System;
using System.Collections.Generic;
using System.Data;
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

namespace SampleDragAndDropEnabledGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Part1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Path", Part1.Source);
                data.SetData("ItemName", "Flexiable Linkage Hose");
                data.SetData("ItemPrice", "$5.00");

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }

        private void Part2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Path", Part2.Source);
                data.SetData("ItemName", "Sprinkler Valve");
                data.SetData("ItemPrice", "$10.00");

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }

        private void Part3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Path", Part3.Source);
                data.SetData("ItemName", "Faucet Depot");
                data.SetData("ItemPrice", "$15.00");

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }

        }

        private void Part4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Path", Part4.Source);
                data.SetData("ItemName", "Solenoid Replacement Kit");
                data.SetData("ItemPrice", "$20.00");

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }

        private void MainGrid_Drop(object sender, DragEventArgs e)
        {
            Image img = new Image();
            img.Height = 41;
            img.Width = 41;
            Uri uri = new Uri(e.Data.GetData("Path").ToString());
            BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage(uri);

            MainGrid.AutoGenerateColumns = false;
            
            if (MainGrid.Columns.Count == 0)
            {
                DataGridTemplateColumn col1 = (DataGridTemplateColumn)this.FindResource("dgt");
                col1.Width = new DataGridLength(20, DataGridLengthUnitType.Star);
                MainGrid.Columns.Add(col1);

                DataGridTextColumn col2 = new DataGridTextColumn();
                col2.Header = "Name";
                col2.Width = new DataGridLength(40, DataGridLengthUnitType.Star);
                col2.Binding = new Binding("Column2");
                MainGrid.Columns.Add(col2);

                DataGridTextColumn col3 = new DataGridTextColumn();
                col3.Header = "Price";
                col3.Width = new DataGridLength(40, DataGridLengthUnitType.Star);
                col3.Binding = new Binding("Column3");
                MainGrid.Columns.Add(col3);

                DataTable dt = new DataTable();
                dt.Columns.Add("Column1", typeof(BitmapImage));
                dt.Columns.Add("Column2");
                dt.Columns.Add("Column3");

                DataRow dr = dt.NewRow();
                dr[0] = bmp;
                dr[1] = e.Data.GetData("ItemName");
                dr[2] = e.Data.GetData("ItemPrice");
                dt.Rows.Add(dr);

                DataRow totalsRow = dt.NewRow();
                totalsRow[2] = e.Data.GetData("ItemPrice");
                dt.Rows.Add(totalsRow);

                MainGrid.ItemsSource = dt.DefaultView;
            }
            else
            {
                DataTable dt = ((DataView)MainGrid.ItemsSource).ToTable();

                DataRow dr = dt.NewRow();
                dr[0] = bmp;
                dr[1] = e.Data.GetData("ItemName");
                dr[2] = e.Data.GetData("ItemPrice");
                dt.Rows.InsertAt(dr, dt.Rows.Count - 1);

                decimal currentTotal = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1][2].ToString().Remove(0, 1)) + 
                    Convert.ToDecimal(e.Data.GetData("ItemPrice").ToString().Remove(0, 1));
                dt.Rows[dt.Rows.Count - 1][2] = "$" + currentTotal.ToString();
                

                MainGrid.ItemsSource = dt.DefaultView;
            }

        }
    }
}
