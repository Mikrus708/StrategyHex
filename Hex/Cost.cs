using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hex
{
    public class Cost : IEnumerable<Material>
    {
        const string xmlDefaultNameString = "Cost";
        uint[] values = new uint[Enum.GetValues(typeof(MaterialType)).Length];
        public Cost (MaterialType[] types, uint[] values)
        {
            if (types == null || values == null)
            {
                throw new ArgumentNullException(types == null ? "types" : "values", "Tablica nie może być null");
            }
            if (types.Length != values.Length)
            {
                throw new ArgumentException("Tablica maja inny rozmiar");
            }
            for (int i = 0; i < types.Length; ++i)
            {
                this.values[(int)types[i]] += values[i];
            }
        }
        public Cost (params Material[] materials)
        {
            foreach (Material m in materials)
            {
                values[(int)m.Type] += m.Ammount;
            }
        }
        public Cost() { }
        public Cost(XmlElement elem)
        {
            foreach (XmlElement mater in elem)
            {
                bool accept;
                Material mat = new Material(mater, out accept);
                if (accept)
                {
                    this[mat.Type] += mat.Ammount;
                }
            }
        }
        /// <summary>
        /// Tworzy XmlElement na podstawie kosztu.
        /// </summary>
        /// <param name="doc">Dokument dla którego towrzony jest element</param>
        /// <param name="name">Nazwa z jaką element ma być utworzony</param>
        /// <returns></returns>
        public XmlElement GetXmlElement(XmlDocument doc)
        {
            XmlElement result = doc.CreateElement(xmlDefaultNameString);
            foreach (MaterialType mat in Enum.GetValues(typeof(MaterialType)))
            {
                if (this[mat] > 0)
                {
                    result.AppendChild(new Material(mat, this[mat]).GetXmlElement(doc));
                }
            }
            return result;
        }
        public IEnumerator<Material> GetEnumerator()
        {
            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] > 0)
                {
                    yield return new Material((MaterialType)i, values[i]);
                }
            }
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public static Cost operator+(Cost c, Material mat)
        {
            c[mat.Type] += mat.Ammount;
            return c;
        }
        public static Cost operator-(Cost c, Material mat)
        {
            c[mat.Type] -= mat.Ammount;
            return c;
        }
        public uint this[MaterialType key]
        {
            get
            {
                return values[(int)key];
            }
            set
            {
                values[(int)key] = value;
            }
        }
    }
}
