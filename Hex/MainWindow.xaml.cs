using Hex.Buildings;
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

namespace WpfHex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color actual = Colors.Chartreuse;
        HexagonalHexGrig hhg;
        public MainWindow()
        {
            InitializeComponent();
            initComboBuild();
            this.KeyDown += Arrows;
           // Canv.Key
           // Canv.key
            
        }



        private void Arrows(object sender, KeyEventArgs e)
        { 
            switch (e.Key)
            {
                case Key.W:
                    {
                        Canv.Margin = new Thickness(Canv.Margin.Left, Canv.Margin.Top - 10, Canv.Margin.Right, Canv.Margin.Bottom + 10);
                        break;
                    }
                case Key.S:
                    {
                        Canv.Margin = new Thickness(Canv.Margin.Left, Canv.Margin.Top + 10, Canv.Margin.Right, Canv.Margin.Bottom - 10);
                        break;
                    }
                case Key.A:
                    {
                        Canv.Margin = new Thickness(Canv.Margin.Left - 10, Canv.Margin.Top, Canv.Margin.Right + 10, Canv.Margin.Bottom);
                        break;
                    }
                case Key.D:
                    {
                        Canv.Margin = new Thickness(Canv.Margin.Left + 10, Canv.Margin.Top, Canv.Margin.Right - 10, Canv.Margin.Bottom);
                        break;
                    }
            }
        }


        private void hexdupa(object sender, RoutedEventArgs e)
        {
            Canv.Children.RemoveRange(0, Canv.Children.Count );
            int size = 35;
            hhg = new HexagonalHexGrig(6, Brushes.Purple, size, Canv, new Point(400,350));
            foreach (var tabh in hhg.hexG)
            {
                foreach (var he in tabh)
                {
                    he.MouseDown += build;
                }
            }
        }

        private void build(object sender, MouseButtonEventArgs e)
        {
            Hex h = (sender as Hex);
            h.HexField.Build(Building.FactorBuilding((BuildingType)(BuildingComboBox.SelectedItem)));
            h.bru = h.Fill = h.HexField.CombinedBrush;
        }

        private void dupaClk(object sender, RoutedEventArgs e)
        {
            
            hhg = null;
            Canv.Children.RemoveRange(0, Canv.Children.Count);
            int size = 40;
            HexGrid hg = new HexGrid(18, 5, Brushes.Chocolate, size, Canv, null);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = sender as ComboBox;
            try
            {
                actual = (Color)ColorConverter.ConvertFromString((c.SelectedItem as ComboBoxItem).Content as string);
            }
            catch { }

        }

        private void initComboBuild()
        {
            foreach (var item in Enum.GetValues(typeof(BuildingType)))
            {
                BuildingComboBox.Items.Add(item);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (hhg == null) return;
            int x;
            int y;
            int z;
            if (int.TryParse(textBox.Text, out x) &&
            int.TryParse(textBox1.Text, out y) &&
            int.TryParse(textBox2.Text, out z))
            {
                if (x + y + z == 0)
                {
                    hhg[x, y, z].Fill = hhg[x, y, z].bru = new SolidColorBrush(actual);

                }
                else
                    MessageBox.Show("Podane indeksy nie sumuja się do zera.");
            }
            else
                MessageBox.Show("Indeks musi być liczbą");
        }
    }
}
