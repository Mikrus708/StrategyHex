﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class SteelWorks : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(new Material[] { Material.Wood, Material.Stone }, new uint[] { 300, 200 }); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.SteelWorks; }
        }
        public override Cost UpkeepCost
        {
            get { return new Cost(new Material[] { Material.Coal }, new uint[] { 5 }); }
        }
    }
}
