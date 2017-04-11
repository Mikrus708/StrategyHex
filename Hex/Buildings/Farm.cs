﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public class Farm : Building
    {
        public override Cost BuildCost
        {
            get { return new Cost(); }
        }
        public override BuildingType Type
        {
            get { return BuildingType.Farm; }
        }
    }
}
