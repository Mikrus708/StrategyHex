using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class CoalMine : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(new Material[] { Material.Wood, Material.Stone }, new uint[] { 200, 100 }); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.CoalMine; }
        }
    }
}
