using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hex.Buildings;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hex
{
    /// <summary>
    /// Klasa pola
    /// </summary>
    public class Field
    {
        const int MAXSIZE = 8;
        Resource[] stack = new Resource[MAXSIZE];
        byte top;
        Building building = null;
        FiledType type;
        public Field()
        {
            top = 0;
        }
        public Field(FiledType type)
        {
            this.type = type;
            top = 0;
        }
        public System.Windows.Media.Brush FieldBrush
        {
            get
            {
                return new SolidColorBrush(colorByType());
            }
        }
        public System.Windows.Media.Brush CombinedBrush
        {
            get
            {
                DrawingBrush brush = new DrawingBrush();
                DrawingGroup checkersDrawingGroup = new DrawingGroup();
                int x = 100;
                int y = 100;
                GeometryDrawing fieldDraw =
                    new GeometryDrawing(
                        FieldBrush,
                        null,
                        new RectangleGeometry(new System.Windows.Rect(0, 0, x, y)));
                checkersDrawingGroup.Children.Add(fieldDraw);
                if (building != null)
                {
                    GeometryDrawing buildingDraw =
                        new GeometryDrawing(
                            Building.Brush,
                            null,
                            new RectangleGeometry(new System.Windows.Rect(0, 0, x, y)));
                    checkersDrawingGroup.Children.Add(buildingDraw);
                }

                brush.Drawing = checkersDrawingGroup;
                //myBrush.Viewport = new System.Windows.Rect(0, 0, 0.25, 0.25);
                brush.TileMode = TileMode.None;
                return brush;
            }
        }
        private System.Windows.Media.Color colorByType()
        {
            System.Drawing.Color col;
            switch (Type)
            {
                case FiledType.Grass:
                    col = System.Drawing.Color.LightGreen;
                    break;
                case FiledType.Mountain:
                    col = System.Drawing.Color.Gray;
                    break;
                case FiledType.Sea:
                    col = System.Drawing.Color.Blue;
                    break;
                case FiledType.Forest:
                    col = System.Drawing.Color.DarkGreen;
                    break;
                default:
                    throw new NotImplementedException("Dany typ pola nie został zaimplementowany w funkji colorByType w klasie Field");
            }
            return System.Windows.Media.Color.FromRgb(col.R, col.G, col.B);
        }
        public FiledType Type
        {
            get { return type; }
            set { type = value; }
        }
        /// <summary>
        /// Dodaje do stosu zasobów nowy zasób, o ile jest na niego jeszcze miejsce.
        /// </summary>
        /// <param name="resource">Zasób do dodania</param>
        /// <returns>Czy udało się dodać zasób</returns>
        public bool PushResource(Resource resource)
        {
            if (top < MAXSIZE)
            {
                stack[top] = resource;
                ++top;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Usuwa zasób z wierchu stosu.
        /// </summary>
        public void PopResource()
        {
            top -= top > 0 ? (byte)1 : (byte)0;
        }
        /// <summary>
        /// Zwraca surowiec z wierzchu stosu.
        /// </summary>
        /// <returns></returns>
        public Resource? PeekResource()
        {
            return top == 0 ? (Resource?)null : stack[top - 1];
        }
        /// <summary>
        /// Ile surowców znaduje się na polu.
        /// </summary>
        public int NumberOfResources
        {
            get { return top; }
        }
        /// <summary>
        /// Czy nie ma już surowców.
        /// </summary>
        public bool OutOfResources
        {
            get { return top == 0; }
        }
        /// <summary>
        /// Surowce znajdujące się na polu.
        /// </summary>
        public IEnumerable<Resource> Resources
        {
            get
            {
                for (int i = top - 1; i >= 0; --i)
                {
                    yield return stack[i];
                }
                yield break;
            }
        }
        /// <summary>
        /// Aktualnie postawiona budowla.
        /// </summary>
        public Building Building
        {
            get { return building; }
        }
        /// <summary>
        /// Dodaje dany budynek do pola jeżeli nie ma jeszcze żadnego postawiongo i zwraca true, wpp nie zmienia aktualnego budynku i zwraca false.
        /// </summary>
        /// <param name="building">Budynek do wybudowania</param>
        /// <returns>Czy udało się dodać budynek</returns>
        public bool Build(Building building)
        {
            if (this.building != null)
            {
                return false;
            }
            this.building = building;
            return true;
        }
        /// <summary>
        /// Niszczy budynek znajdujący się na polu.
        /// </summary>
        public void DestroyBuilding()
        {
            building = null;
        }
    }
    /// <summary>
    /// Typy pól
    /// </summary>
    public enum FiledType : byte
    {
        Grass,
        Mountain,
        Sea,
        Forest
    }
}
