using Hex.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class TownHall : Building
    {
        public override BuildingType Type
        {
            get { return BuildingType.TownHall; }
        }
    }
}
