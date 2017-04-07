using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex
{
    /// <summary>
    /// Klasa do przechowywania kosztów
    /// </summary>
    public class Cost
    {
        MaterialType[] materials;
        uint[] costs;
        public Cost (MaterialType[] materials, uint[] costs)
        {
            this.materials = materials;
            this.costs = costs; 
        }
    }
}
