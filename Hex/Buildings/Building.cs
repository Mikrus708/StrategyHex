using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex.Buildings
{
    /// <summary>
    /// Abstrakcyjna klasa budynków.
    /// </summary>
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
        public abstract Image Image { get; }
    }
    /// <summary>
    /// Typy budynków (ID)
    /// </summary>
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
