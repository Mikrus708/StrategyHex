﻿using System;
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
        public static Building FactorBuilding(BuildingType typ)
        {
            switch (typ)
            {
                case BuildingType.Hut:
                    return new Hut();
                case BuildingType.TownHall:
                    return new TownHall();
                case BuildingType.SmallTower:
                    return new SmallTower();
                case BuildingType.Sawmill:
                    return new Sawmill();
                case BuildingType.StoneMine:
                    return new StoneMine();
                case BuildingType.Farm:
                    return new Farm();
                case BuildingType.IronMine:
                    return new IronMine();
                case BuildingType.CoalMine:
                    return new CoalMine();
                case BuildingType.SteelWorks:
                    return new SteelWorks();
                case BuildingType.FishermanHut:
                    return new FishermanHut();
                case BuildingType.GoldMine:
                    return new GoldMine();
                case BuildingType.LumberjackHut:
                    return new LumberjackHut();
                default:
                    throw new NotImplementedException("Nie zaimplementowano w FactorBuilding danego typu chuju.");
            }
        }
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
