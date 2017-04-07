using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex
{
    public class Cost
    {
        Material[] materials;
        uint[] costs;
        public Cost (Material[] materials, uint[] costs)
        {
            this.materials = materials;
            this.costs = costs; 
        }
    }
}
