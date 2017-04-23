using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Hex
{
    /// <summary>
    /// Klasa przecowująca typy surowców oraz ich ilości.
    /// </summary>
    public struct Material
    {
        const string xmlTypeString = "Type";
        const string xmlDefName = "Material";
        readonly MaterialType type;
        uint ammount;
        /// <summary>
        /// Tworzy strukturę surowca o podanym type i ilości.
        /// </summary>
        /// <param name="type">Typ surowca</param>
        /// <param name="ammount">Ilość surowca</param>
        public Material (MaterialType type, uint ammount)
        {
            this.type = type;
            this.ammount = ammount;
        }
        /// <summary>
        /// Pomniejsza surowiec o ammount, ale nie więcej niż wynosi jego aktualna ilość. Zwraca nową strukturę surowca o tym samym typie i ilości takiej jaką udało się usunąć.
        /// </summary>
        /// <param name="ammount">Ile pobrać</param>
        /// <returns>Nowa struktura z pobraną wartością</returns>
        public Material Take(uint ammount)
        {
            Material result = new Material(type, Math.Min(this.ammount, ammount));
            this.ammount -= Math.Min(this.ammount, ammount);
            return result;
        }
        /// <summary>
        /// Tworzy strukturę surowca będącą kopią podanego argumentu.
        /// </summary>
        /// <param name="material">Struktura do skopiowania</param>
        public Material (Material material)
        {
            type = material.type;
            ammount = material.ammount;
        }
        public static Material? GetFromXmlElement (XmlElement elem)
        {
            if (elem.Name == xmlDefName)
            {
                MaterialType typ;
                if (Enum.TryParse(elem.GetAttribute(xmlTypeString), out typ))
                {
                    uint amm;
                    if (uint.TryParse(elem.InnerText, out amm))
                    {
                        return new Material(typ, amm);
                    }
                }
            }
            return null;
        }
        public static Material operator +(Material material, uint ammount)
        {
            return new Material(material.type, material.ammount + ammount);
        }
        public static Material operator -(Material material, uint ammount)
        {
            return new Material(material.type, material.ammount > ammount ? material.ammount - ammount : 0);
        }
        /// <summary>
        /// Tworzy strukturę surowca na podstawie danego zasobu. Zachowuje ilość i przekształca typ zasobu do odpiwiedniego typu surowca.
        /// </summary>
        /// <param name="resource">Zasób do przekształcenia</param>
        public Material (Resource resource)
        {
            type = GetTypeOfMaterial(resource.Type);
            ammount = resource.Ammount;
        }
        /// <summary>
        /// Zwraca typ surowca odpowiadający danemu zasobowi.
        /// </summary>
        /// <param name="resource">Zasób do przekształcenia</param>
        /// <returns>Odpowiadający typ surowca</returns>
        public static MaterialType GetTypeOfMaterial(Resource resource)
        {
            return GetTypeOfMaterial(resource.Type);
        }
        /// <summary>
        /// Zwraca typ surowca odpowiadający danemu typowi zasobu.
        /// </summary>
        /// <param name="type">Typ zasobu</param>
        /// <returns>Odpowiadający typ surowca</returns>
        public static MaterialType GetTypeOfMaterial(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Forest:
                    return MaterialType.Wood;
                case ResourceType.Stone:
                    return MaterialType.Stone;
                case ResourceType.Grain:
                case ResourceType.Fishes:
                    return MaterialType.Food;
                case ResourceType.Gold:
                    return MaterialType.GoldOre;
                case ResourceType.Iron:
                    return MaterialType.IronOre;
                case ResourceType.Coal:
                    return MaterialType.Coal;
                default:
                    throw new NotImplementedException($"ResourceType.{type.ToString()} niezaimplementowany w Material.GetType()");
            }
        }
        public Brush Brush
        {
            get
            {
                return new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri($@"pack://application:,,,/Resources/MaterialImage/{Type}.png"))
                };
            }
        }
        public System.Drawing.Bitmap Bitmap
        {
            get
            {
                return new System.Drawing.Bitmap($@"pack://application:,,,/Resources/MaterialImage/{Type}.png");
            }
        }
        public Uri ImageUri
        {
            get
            {
                return new Uri($@"pack://application:,,,/Resources/MaterialImage/{Type}.png");
            }
        }
        public MaterialType Type
        {
            get { return type; }
        }
        public uint Ammount
        {
            get { return ammount; }
            set { ammount = value; }
        }
        public XmlElement GetXmlElement(XmlDocument doc)
        {
            XmlElement result = doc.CreateElement(xmlDefName);
            result.SetAttribute(xmlTypeString, Type.ToString());
            result.InnerText = ammount.ToString();
            return result;
        }
    }
    /// <summary>
    /// Typu surowców (ID)
    /// </summary>
    public enum MaterialType : byte
    {
        Food,
        Wood,
        Stone,
        Gold,
        Population,
        IronOre,
        Coal,
        IronBar,
        GoldOre
    }
}
