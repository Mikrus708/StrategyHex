using Hex.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class SteelWorks : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.SteelWorks; }
        }
        public override Cost UpkeepCost
        {
            get { return new Cost(); }
        }
        public override Image Image
        {
            get
            {
                return (Image)Resources.ResourceManager.GetObject("Icon_SteelWorks");
            }
        }
    }
}
