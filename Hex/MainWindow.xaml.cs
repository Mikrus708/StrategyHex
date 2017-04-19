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

namespace WpfApplication1
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
