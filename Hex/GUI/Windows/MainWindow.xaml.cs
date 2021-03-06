﻿using StrategyHexGame.GUI.Controls;
using StrategyHexGame.Game.Buildings;
using StrategyHexGame.Game.Settings;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StrategyHexGame.Game.Base;

namespace StrategyHexGame.GUI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color actual = Colors.Chartreuse;
        HexagonalHexGrid hhg;
        double zoom = 1;

        public MainWindow()
        {
            BuildingsSettings.Load("../../Game/Settings/BuildingsSettings.xml");
            ResourceSettings.Load("../../Game/Settings/ResourceSettings.xml");
            MaterialSettings.Load("../../Game/Settings/MaterialSettings.xml");
            InitializeComponent();
            initComboBuild();
            initMaterialList();
            this.KeyDown += Arrows;
            this.MouseWheel += Zoom;
            if (System.IO.File.Exists("../../Mapa.xml"))
            {
                loadMap("../../Mapa.xml");
            }

           // Canv.Key
           // Canv.key

        }

        private void Zoom(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && zoom == 4) return;
            if (e.Delta < 0 && zoom == 0.25) return;
            zoom *= Math.Pow(2,Math.Sign(e.Delta));
            ScaleTransform st = new ScaleTransform(zoom, zoom, e.GetPosition(Canv).X, e.GetPosition(Canv).Y);
            Canv.RenderTransform = st;
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
            hhg = new HexagonalHexGrid(6, Brushes.Purple, size, Canv, new Point(400,350));
            var ran = new Random();
            foreach (var he in hhg.Hexes)
            {
                he.MouseDown += reactionToHexClick;
                he.Field.Type = (FieldType)(ran).Next(0, Enum.GetValues(typeof(FieldType)).Length);
                he.Field.AddResource(new Resource((ResourceType)(ran).Next(0, Enum.GetValues(typeof(ResourceType)).Length), (uint)ran.Next(100, 1000)));
                he.bru = he.Fill = he.Field.CombinedBrush;
            }
        }

        private void reactionToHexClick(object sender, MouseButtonEventArgs e)
        {
            Hex h = (sender as Hex);
            if ((bool)checkBox.IsChecked)
            {
                h.Field.DestroyBuilding();
            }
            else
            {
                h.Field.Build(Building.FactoryBuilding((BuildingType)(BuildingComboBox.SelectedItem)));
            }
            //h.bru = h.Fill = h.Field.CombinedBrush;
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
            foreach (Hex h in hhg.Surrounding(2, 0, 0))
                h.stro = h.Stroke = Brushes.Aqua;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            BuildingComboBox.IsEnabled = false;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            BuildingComboBox.IsEnabled = true;
        }

        private void initMaterialList()
        {
            foreach (MaterialType type in Enum.GetValues(typeof(MaterialType)))
            {
                materialList.Items.Add(new Material(type, 1000));
            }
        }
        private void loadMap(string name)
        {
            hhg = HexagonalHexGrid.LoadXML(name, Brushes.Purple, 35, Canv, new Point(400, 350));
            var ran = new Random();
            foreach (var he in hhg.Hexes)
            {
                he.MouseDown += reactionToHexClick;
            }
        }
    }
}
