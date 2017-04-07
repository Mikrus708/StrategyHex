using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex
{
    /// <summary>
    /// Klasa przecowująca typy surowców oraz ich ilości.
    /// </summary>
    public struct Material
    {
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
        /// Tworzy strukturę surowca będącą kopią podanego argumentu.
        /// </summary>
        /// <param name="material">Struktura do skopiowania</param>
        public Material (Material material)
        {
            type = material.type;
            ammount = material.ammount;
        }
        /// <summary>
        /// Tworzy strukturę surowca na podstawie danego zasobu. Zachowuje ilość i przekształca typ zasobu do odpiwiedniego typu surowca.
        /// </summary>
        /// <param name="resource">Zasób do przekształcenia</param>
        public Material (Resource resource)
        {
            type = GetType(resource.Type);
            ammount = resource.Ammount;
        }
        /// <summary>
        /// Zwraca typ surowca odpowiadający danemu zasobowi.
        /// </summary>
        /// <param name="resource">Zasób do przekształcenia</param>
        /// <returns>Odpowiadający typ surowca</returns>
        public static MaterialType GetType(Resource resource)
        {
            return GetType(resource.Type);
        }
        /// <summary>
        /// Zwraca typ surowca odpowiadający danemu typowi zasobu.
        /// </summary>
        /// <param name="type">Typ zasobu</param>
        /// <returns>Odpowiadający typ surowca</returns>
        public static MaterialType GetType(ResourceType type)
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
            }
            throw new Exception("ResourceType niezdefiniowany w Material.GetType()");
        }
        /// <summary>
        /// Typ surowca.
        /// </summary>
        public MaterialType Type
        {
            get { return type; }
        }
        /// <summary>
        /// Ilość surowca.
        /// </summary>
        public uint Ammount
        {
            get { return ammount; }
            set { ammount = value; }
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
