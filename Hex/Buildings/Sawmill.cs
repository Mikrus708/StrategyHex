﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class Sawmill : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(new Material[] { Material.Wood, Material.Stone }, new uint[] { 150, 50 }); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.Sawmill; }
        }
    }
}
