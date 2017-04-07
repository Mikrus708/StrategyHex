using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    public abstract class Building
    {
        protected uint baseRange;
        public virtual Cost BuildCost
        {
            get { return null; }
        }
        public virtual Cost UpkeepCost
        {
            get { return null; }
        }
        public abstract BuildingType Type { get; }
        public virtual uint Range
        {
            get { return baseRange; }
        }
    }

    public enum BuildingType : ushort
    {
        Hut,
        TownHall,
        SmallTower,
        Sawmill,
        StoneMine,
        Farm,
        IronMine,
        CoalMine,
        SteelWorks,
        FishermanHut,
        GoldMine,
        LumberjackHut
    }
}
