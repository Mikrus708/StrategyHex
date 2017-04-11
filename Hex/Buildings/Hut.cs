using Hex.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class Hut : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.Hut; }
        }
        public override Image Image
        {
            get
            {
                return (Image)Resources.ResourceManager.GetObject("Icon_Hut");
            }
        }
    }
}
