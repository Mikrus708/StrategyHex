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


namespace WpfApplication1
{
    public class Hex : Shape
    {
        public Polygon p = new Polygon();
        public PointCollection points = null;
        public int i = 10;
        private Brush bru = Brushes.RoyalBlue;
        
        public Hex(Point center, double Size, Brush b, Canvas c=null, MouseButtonEventHandler HexFunc = null) 
        {
            double sqrt = Size * Math.Sqrt(3) / 2;
            points = new PointCollection(6);
            points.Add(new Point(Convert.ToInt32(center.X - Size / 2), Convert.ToInt32(center.Y + sqrt)));
            points.Add(new Point(Convert.ToInt32(center.X + Size / 2), Convert.ToInt32(center.Y + sqrt)));
            points.Add(new Point(Convert.ToInt32(center.X + Size), Convert.ToInt32(center.Y)));
            points.Add(new Point(Convert.ToInt32(center.X + Size / 2), Convert.ToInt32(center.Y - sqrt)));
            points.Add(new Point(Convert.ToInt32(center.X - Size / 2), Convert.ToInt32(center.Y - sqrt)));
            points.Add(new Point(Convert.ToInt32(center.X - Size), Convert.ToInt32(center.Y)));
            p.Stroke = Brushes.Black;
            p.Fill = b; 
            p.StrokeThickness = 1.7;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
            p.Points = points;
            if(c!=null) c.Children.Add(p);
            if (HexFunc != null) p.MouseDown += HexFunc;
            else p.MouseDown += Klik;
        }

        private void Klik(object sender, MouseButtonEventArgs e)
        {
            p.Fill = Brushes.Green;
            p.Fill = bru;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return p.RenderedGeometry;
            }
        }
    }

    public class HexGrid : Canvas
    {
        public Hex[,] hexG = null;

        public HexGrid(int r, int c, Brush b, int Size,Canvas Canv, MouseButtonEventHandler HexFunc = null) : base()
        {
           
            int sqrt2 = Convert.ToInt32(Size * Math.Sqrt(3) / 2);
            Hex[,] hexgrid = new Hex[30, 50];
            for (int j = 0; j < hexgrid.GetLength(1); ++j)
                for (int i = 0; i < hexgrid.GetLength(0); ++i)
                    hexgrid[i, j] = new Hex(new Point(3 * i * Size + 3 * (j % 2) * Size / 2, j * sqrt2), Size, b, Canv);
            
        }
    }
}
