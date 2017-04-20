using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex
{
    /// <summary>
    /// Klasa przechowująca typy zasobów oraz ich ilości.
    /// </summary>
    public struct Resource
    {
        readonly ResourceType type;
        uint ammount;
        /// <summary>
        /// Tworzy strukture Resource.
        /// </summary>
        /// <param name="type">Typ zasobu</param>
        /// <param name="ammount">Ilość zasobu</param>
        public Resource (ResourceType type, uint ammount)
        {
            this.type = type;
            this.ammount = ammount;
        }
        /// <summary>
        /// Tworzy strukture Resource na podstawie drugiej struktury.
        /// </summary>
        /// <param name="resource"></param>
        public Resource (Resource resource)
        {
            type = resource.type;
            ammount = resource.ammount;
        }
        /// <summary>
        /// Typ zasobu.
        /// </summary>
        public ResourceType Type
        {
            get { return type; }
        }
        /// <summary>
        /// Ilość zasobu.
        /// </summary>
        public uint Ammount
        {
            get { return ammount; }
            set { ammount = value; }
        }
        /// <summary>
        /// Zwiększa ilość zasobu o ammount. Nie sprawdza możliwości przepełnienia unit!
        /// </summary>
        /// <param name="resource">Zasób do zwiększenia</param>
        /// <param name="ammount">O ile zwiększyć</param>
        /// <returns>Powiększony zasób</returns>
        public static Resource operator+(Resource resource, uint ammount)
        {
            return new Resource(resource.type, resource.ammount + ammount);
        }
        /// <summary>
        /// Zmniejsza ilość zasobu o ammount. Jeżeli ilość zasobu jest mniejsza od podanej do usunięcia, wówczas ilość ustawiana jest na 0!
        /// </summary>
        /// <param name="resource">Zasób do zmniejszenia</param>
        /// <param name="ammount">O ile zmniejszyć</param>
        /// <returns>Pomninejszony zasób</returns>
        public static Resource operator-(Resource resource, uint ammount)
        {
            return new Resource(resource.type, resource.ammount > ammount ? resource.ammount - ammount : 0);
        }
        /// <summary>
        /// Pomniejsza zasób o ammount, ale nie więcej niż wynosi jego aktualna ilość. Zwraca nową strukturę zasobu o tym samym typie i ilości takiej jaką udało się usunąć.
        /// </summary>
        /// <param name="ammount">Ile pobrać</param>
        /// <returns>Nowa struktura z pobraną wartością</returns>
        public Resource Gather(uint ammount)
        {
            Resource result = new Resource(type, Math.Min(this.ammount, ammount));
            this.ammount -= Math.Min(this.ammount, ammount);
            return result;
        }
        static public int GetLayer(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Forest:
                case ResourceType.Grain:
                case ResourceType.Fishes:
                    return 0;
                case ResourceType.Coal:
                case ResourceType.Stone:
                    return 1;
                case ResourceType.Iron:
                    return 2;
                case ResourceType.Gold:
                    return 3;
                default:
                    throw new NotImplementedException($"Brakuje implenetacji dla typu {type} w GetLayer()");
            }
        }
        /// <summary>
        /// Czy zasób jest pusty.
        /// </summary>
        public bool Empty
        {
            get { return ammount == 0; }
        }
    }
    /// <summary>
    /// Type surowców (ID)
    /// </summary>
    public enum ResourceType : byte
    {
        Forest,
        Stone,
        Grain,
        Fishes,
        Gold,
        Iron,
        Coal
    }
}
