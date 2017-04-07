using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class FishermanHut : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(new Material[] { Material.Wood }, new uint[] { 125 }); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.FishermanHut; }
        }
    }
}
