using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class IronMine : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(new MaterialType[] { MaterialType.Wood, MaterialType.Stone }, new uint[] { 300, 200 }); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.IronMine; }
        }
    }
}
