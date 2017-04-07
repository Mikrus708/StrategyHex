using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hex.Buildings;

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
        public Field(FiledType type)
        {
            this.type = type;
            top = 0;
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
