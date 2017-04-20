using Hex.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
    }
}
