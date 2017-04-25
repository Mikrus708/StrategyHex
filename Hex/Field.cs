using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hex.Buildings;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using Hex.GameSettings;
using System.IO;

namespace Hex
{
    /// <summary>
    /// Klasa pola
    /// </summary>
    public class Field
    {
        const string xmlTypeString = "Type";
        const string xmlXString = "X";
        const string xmlYString = "Y";
        const string xmlStackString = "ResourceStack";
        const string xmlDefName = "Field";
        const string xmlBuildName = "Buildings";
        readonly int x, y;
        Resource[] stack = new Resource[ResourceSettings.NumberOfLayers];
        DrawingBrush fieldBrush = new DrawingBrush();
        DrawingBrush combinedBrush = new DrawingBrush();
        byte top;
        Building building = null;
        FieldType type;
        public Field(int _x, int _y, FieldType type = FieldType.Grass)
        {
            x = _x;
            y = _y;
            this.type = type;
            top = ResourceSettings.NumberOfLayers;
            updateBrushes();
        }
        private void updateBrushes()
        {
            updateFieldBrush();
            updateCombinedBrush();
        }
        private void updateCombinedBrush()
        {
            DrawingGroup drG = new DrawingGroup();
            int x = 100;
            int y = 100;
            GeometryDrawing fieldDraw =
                new GeometryDrawing(
                    fieldBrush,
                    null,
                    new RectangleGeometry(new System.Windows.Rect(0, 0, x, y)));
            drG.Children.Add(fieldDraw);
            if (building != null)
            {
                GeometryDrawing buildingDraw =
                    new GeometryDrawing(
                        Building.Brush,
                        null,
                        new RectangleGeometry(new System.Windows.Rect(0, 0, x, y)));
                drG.Children.Add(buildingDraw);
            }
            combinedBrush.Drawing = drG;
            combinedBrush.TileMode = TileMode.None;
        }
        private void updateFieldBrush()
        {
            ImageBrush tmp = new ImageBrush(new BitmapImage(new Uri($@"pack://application:,,,/Resources/HexFields/{type}.png")));
            DrawingGroup drG = new DrawingGroup();
            int x = 100;
            int y = 100;
            GeometryDrawing fieldDraw =
                new GeometryDrawing(
                    tmp,
                    null,
                    new RectangleGeometry(new System.Windows.Rect(0, 0, x, y)));
            drG.Children.Add(fieldDraw);
            if (top < ResourceSettings.NumberOfLayers && stack[top].Ammount > 0)
            {
                switch (stack[top].Type)
                {
                    case ResourceType.Forest:
                    case ResourceType.Fishes:
                    case ResourceType.Wheat:
                        ImageBrush brs = new ImageBrush(new BitmapImage(new Uri($@"pack://application:,,,/Resources/ResoureceFieldImage/{stack[top].Type}.png")));
                        GeometryDrawing buildingDraw =
                            new GeometryDrawing(
                                brs,
                                null,
                                new RectangleGeometry(new System.Windows.Rect(0, 0, x, y)));
                        drG.Children.Add(buildingDraw);
                        break;
                    default:
                        break;
                }
            }
            fieldBrush.Drawing = drG;
            fieldBrush.TileMode = TileMode.None;
        }
        public int X
        {
            get
            {
                return x;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }
        }
        public int Z
        {
            get
            {
                return -x - y;
            }
        }
        public Brush FieldBrush
        {
            get
            {
                return fieldBrush;
            }
        }
        public Brush CombinedBrush
        {
            get
            {
                return combinedBrush;
            }
        }
        public FieldType Type
        {
            get { return type; }
            set { type = value; updateBrushes(); }
        }
        /// <summary>
        /// Dodaje do stosu zasobów nowy zasób, o ile jest na niego jeszcze miejsce.
        /// </summary>
        /// <param name="resource">Zasób do dodania</param>
        /// <returns>Czy udało się dodać zasób</returns>
        public bool AddResource(Resource resource)
        {
            int lay = resource.Layer;
            if (stack[lay].Empty || stack[lay].Type == resource.Type)
            {
                stack[lay] = resource + stack[lay].Ammount;
                if (lay < top && stack[lay].Ammount != 0)
                {
                    top = (byte)lay;
                    updateBrushes();
                }
                return true;
            }
            return false;
        }
        public uint PeekTopAmmount
        {
            get
            {
                return top == ResourceSettings.NumberOfLayers ? 0 : stack[top].Ammount;
            }
        }
        public Resource? GatherTop()
        {
            Resource? result = top < ResourceSettings.NumberOfLayers ? (Resource?)stack[top].GatherAll() : null;
            Refresh();
            return result;
        }
        public Resource? Gather(uint ammount)
        {
            Resource? result = top < ResourceSettings.NumberOfLayers ? (Resource?)stack[top].Gather(ammount) : null;
            Refresh();
            return result;
        }
        /// <summary>
        /// Zwraca surowiec z wierzchu stosu.
        /// </summary>
        /// <returns></returns>
        public ResourceType? PeekTopType
        {
            get
            {
                Refresh();
                return top == ResourceSettings.NumberOfLayers ? null : (ResourceType?)stack[top].Type;
            }
        }
        /// <summary>
        /// Ile surowców znaduje się na polu.
        /// </summary>
        public int NumberOfResources
        {
            get
            {
                int i = 0;
                foreach (var res in stack)
                {
                    if (res.Ammount > 0)
                        ++i;
                }
                return i;
            }
        }
        public bool CanBeBuild(BuildingType btype)
        {
            if (type != FieldType.Grass)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Czy nie ma już surowców.
        /// </summary>
        public bool OutOfResources
        {
            get { return NumberOfResources == 0; }
        }
        /// <summary>
        /// Surowce znajdujące się na polu.
        /// </summary>
        public IEnumerable<Resource> Resources
        {
            get
            {
                for (int i = 0; i < ResourceSettings.NumberOfLayers; ++i)
                {
                    if (stack[i].Ammount != 0)
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
            if (this.building != null || !CanBeBuild(building.Type))
            {
                return false;
            }
            this.building = building;
            updateCombinedBrush();
            return true;
        }
        /// <summary>
        /// Niszczy budynek znajdujący się na polu.
        /// </summary>
        public void DestroyBuilding()
        {
            building = null;
            updateCombinedBrush();
        }
        /// <summary>
        /// Odswieża Brush
        /// </summary>
        public void Refresh()
        {
            while (top < ResourceSettings.NumberOfLayers && stack[top].Ammount == 0) ++top;
            updateBrushes();
        }
        public virtual XmlElement GetXmlElement(XmlDocument doc)
        {
            XmlElement result = doc.CreateElement(xmlDefName);
            result.SetAttribute(xmlTypeString, Type.ToString());
            result.SetAttribute(xmlXString, x.ToString());
            result.SetAttribute(xmlYString, y.ToString());
            XmlElement build = doc.CreateElement(xmlBuildName);
            if (building != null)
            {
                build.AppendChild(building.GetXmlElement(doc));
            }
            result.AppendChild(build);
            XmlElement resStack = doc.CreateElement(xmlStackString);
            foreach (Resource res in Resources)
            {
                resStack.AppendChild(res.GetXmlElement(doc));
            }
            result.AppendChild(resStack);
            return result;
        }
        public static Field GetFromXmlElement(XmlElement elem)
        {
            Field result = null;
            if (elem.Name == xmlDefName)
            {
                string tstr = elem.GetAttribute(xmlTypeString);
                string xstr = elem.GetAttribute(xmlXString);
                string ystr = elem.GetAttribute(xmlYString);
                FieldType _type;
                int _x, _y;
                if (Enum.TryParse(tstr, out _type) && int.TryParse(xstr, out _x) && int.TryParse(ystr, out _y))
                {
                    result = new Field(_x, _y, _type);
                    XmlElement holder = (XmlElement)elem.SelectSingleNode(xmlStackString);
                    if (holder != null)
                    {
                        foreach (XmlElement reso in holder)
                        {
                            Resource? tmp = Resource.GetFromXmlElement(reso);
                            if (tmp != null)
                            {
                                result.AddResource((Resource)tmp);
                            }
                        }
                    }
                    holder = (XmlElement)elem.SelectSingleNode(xmlBuildName);
                    if (holder != null)
                    {
                        result.building = Building.GetFromXmlElement(holder);
                    }
                }
            }
            return result;
        }
    }
    public static class ExtentionField
    {
        public static int GetFieldID(this WpfHex.HexagonalHexGrig hhg, int x, int y)
        {
            int z = -x - y;
            int n = hhg.Size;
            return z + (n * 3 + x - 2) * (n + x - 1) / 2 - (x > 0 ? x * (x - 1) : -x) + n - 1;
        }
        public static int GetFieldID(this WpfHex.HexagonalHexGrig hhg, Field field)
        {
            return hhg.GetFieldID(field.X, field.Y);
        }
    }
    /// <summary>
    /// Typy pól
    /// </summary>
    public enum FieldType : byte
    {
        Grass,
        Mountain,
        Sea
    }
}
