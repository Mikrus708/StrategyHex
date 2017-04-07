using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class StoneMine : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(new MaterialType[] { MaterialType.Wood }, new uint[] { 200 }); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.StoneMine; }
        }
    }
}
