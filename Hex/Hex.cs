using Hex;
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

    public class Hex : Shape
    {
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(Double), typeof(Hex));
       
        public PointCollection points = null;
        public int i = 10;
        public Brush bru = Brushes.RoyalBlue;
        public Brush stro = Brushes.Black;
        private double size;
        private Field hexField;
        public Hex() { }
        public Hex(Point center, double Size, Brush b, Field field = null, Canvas c = null, MouseButtonEventHandler HexFunc = null)
        {
            hexField = field;
            size = Size;
            double sqrt = Size * Math.Sqrt(3) / 2;
            points = new PointCollection(6);
            points.Add(new Point(center.X - Size / 2, center.Y + sqrt));
            points.Add(new Point(center.X + Size / 2, center.Y + sqrt));
            points.Add(new Point(center.X + Size, center.Y));
            points.Add(new Point(center.X + Size / 2, center.Y - sqrt));
            points.Add(new Point(center.X - Size / 2, center.Y - sqrt));
            points.Add(new Point(center.X - Size, center.Y));

            bru = b;
            //if (c != null) c.Children.Add(p);
            if (c != null) c.Children.Add(this);
            if (HexFunc != null) this.MouseDown += HexFunc;
            else MouseDown += Klik;

            MouseEnter += Highlight;
            MouseLeave += Default;

            Fill = bru;                         //kolor środka           
            Stroke = stro;                      //kolor ramki
            StrokeThickness = 1.2;              //grubośc ramki
        }
        public Field Field
        {
            set
            {
                hexField = value;
            }
            get
            {
                return hexField;
            }
        }
        private void Default(object sender, MouseEventArgs e)
        {
            //Fill = bru;
            StrokeThickness /= 2;
            Stroke = stro;
        }

        private void Highlight(object sender, MouseEventArgs e)
        {
            Stroke = Brushes.Gold;
            StrokeThickness *= 2;
        }

        private void Klik(object sender, MouseButtonEventArgs e)
        {
            //bru = Fill = Brushes.Green;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                #region nietykać
                var g = new PathGeometry();
                var figure = new PathFigure();
                figure.StartPoint = points[0];
               // figure.Segments.Add(new LineSegment(points[0], true));
                figure.Segments.Add(new LineSegment(points[1], true));
                figure.Segments.Add(new LineSegment(points[2], true));
                figure.Segments.Add(new LineSegment(points[3], true));
                figure.Segments.Add(new LineSegment(points[4], true));
                figure.Segments.Add(new LineSegment(points[5], true));
                
                figure.IsFilled = true;
                figure.IsClosed = true;

                g.Figures.Add(figure);
                #endregion

                
                return g;
            }
        }
    }
    public class BoardDontUse : Canvas
    {
        public BoardDontUse ()
        {
            this.KeyDown += Arrows;
        }

        private void Arrows(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Up:
                {
                        this.Margin = new Thickness(Margin.Left, Margin.Top - 10, Margin.Right, Margin.Bottom + 10);
                    break;
                }
                case Key.Down:
                    {
                        this.Margin = new Thickness(Margin.Left, Margin.Top + 10, Margin.Right, Margin.Bottom - 10);
                        break;
                    }
                case Key.Left:
                    {
                        this.Margin = new Thickness(Margin.Left - 10, Margin.Top, Margin.Right + 10, Margin.Bottom);
                        break;
                    }
                case Key.Right:
                    {
                        this.Margin = new Thickness(Margin.Left + 10, Margin.Top , Margin.Right - 10, Margin.Bottom);
                        break;
                    }
            }
        }
    }
    public class HexGrid : BoardDontUse
    {
        public Hex[,] hexG = null;
        public Hex center;

        public HexGrid(int r, int c, Brush b, int Size, Canvas Canv, MouseButtonEventHandler HexFunc = null) : base()
        {
            Size++;
            Size++;
            int sqrt2 = Convert.ToInt32(Size * Math.Sqrt(3) / 2);
            Hex[,] hexgrid = new Hex[c, r];
            for (int j = 0; j < hexgrid.GetLength(1); ++j)
                for (int i = 0; i < hexgrid.GetLength(0); ++i)
                    hexgrid[i, j] = new Hex(new Point(Size + 3 * i * Size + 3 * (j % 2) * Size / 2, Size + j * sqrt2), Size-2, b, new Field(i, j), Canv);
            hexG = hexgrid;

        }
    }

    public class HexagonalHexGrig : BoardDontUse
    {
        public Hex[][] hexG;
        public Hex Center;

        private int size;
        public HexagonalHexGrig(int n, Brush b, int Size, Canvas Canv, Point Center) : base()
        {
            //this.Center = Center;
            size = n;
            Polygon p = new Polygon();
            p.Points = new PointCollection { Center, new Point(Center.X + 3, Center.Y), new Point(Center.X + 3, Center.Y + 3), new Point(Center.X, Center.Y + 3) };
            p.Fill = Brushes.Red;

            Canv.Children.Add(p);
            double sqrt = (Size + 2) * Math.Sqrt(3) / 2;
            hexG = new Hex[2 * n - 1][];
            for (int i = 0; i < n; i++)
            {
                hexG[i] = new Hex[n + i];
                hexG[hexG.Length - 1 - i] = new Hex[n + i];
            }
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < hexG[i].Length; j++)
                {
                    hexG[i][j] = new Hex(new Point(Center.X - (n - 1 - i) * (Size + 2) * 3 / 2 , Center.Y - (n - 1 + i * 2) * sqrt + 2 * j * sqrt + i % 2 * sqrt + 2 * (i / 2 * sqrt)), Size, b, new Field(i, j), Canv);
                    hexG[hexG.Length - 1 - i][j] = new Hex(new Point(Center.X + (n - 1 - i) * (Size + 2) * 3 / 2 , Center.Y - (n - 1 + i * 2) * sqrt + 2 * j * sqrt + i % 2 * sqrt + 2 * (i / 2 * sqrt)), Size, b, new Field(hexG.Length - 1 - i, j), Canv);
                }
            for (int i = 0; i < 2 * n - 1; i++)
            {
                hexG[n - 1][i] = new Hex(new Point(Center.X, Center.Y + 2 * (-n + i + 1) * sqrt), Size, b, new Field(n - 1, i), Canv);
            }
        }
        public IEnumerable<Hex> Hexes
        {
            get
            {
                foreach (var hexTab in hexG)
                {
                    foreach (var h in hexTab)
                    {
                        yield return h;
                    }
                }
                yield break;
            }
        }
        public Hex this[int x, int y, int z]
        {
            get
            {

                try
                {
                    return hexG[size - 1 + x][x > 0 ? size + z - 1 : size - y - 1];
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Index was outside the bounds of the array.")
                    {
                        MessageBox.Show("Pod podanym indeksem nie isnieje żadne pole.\n Zwracam cnetrum.");
                        return this[0, 0, 0];
                    }
                    else
                        MessageBox.Show("Wystąpił nieznany błąd. \nAplikacja moze teraz nie działać poprawnie.");
                    return null;
                }
            }
        }
    }


   
}

            

         

    // more properties

    

