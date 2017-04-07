using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex
{
    public class Cost : IEnumerable<Material>
    {
        uint[] values;
        MaterialType[] types;
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
            this.values = values;
            this.types = types;
        }
        public Cost (params Material[] materials)
        {
            values = materials.Select((x) => x.Ammount).ToArray();
            types = materials.Select((x) => x.Type).ToArray();
        }
        public Cost()
        {
            values = new uint[0];
            types = new MaterialType[0];
        }
        public IEnumerator<Material> GetEnumerator()
        {
            for (int i = 0; i < types.Length; ++i)
            {
                yield return new Material(types[i], values[i]);
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
                int index = Array.BinarySearch(types, key);
                if (index < 0)
                {
                    return 0;
                }
                return values[index];
            }
            set
            {
                int index = Array.BinarySearch(types, key);
                if (index < 0)
                {
                    var nValues = new uint[values.Length];
                    Array.Copy(values, nValues, values.Length);
                    var nTypes = new MaterialType[types.Length];
                    Array.Copy(types, nTypes, types.Length);
                    values = nValues;
                    types = nTypes;
                    index = types.Length - 1;
                    types[index] = key;
                }
                values[index] = value;
            }
        }
    }
}
