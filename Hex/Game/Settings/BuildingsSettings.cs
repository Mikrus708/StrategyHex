using StrategyHexGame.Game.Buildings;
using StrategyHexGame.Game.Base;
using System;
using System.Xml;

namespace StrategyHexGame.Game.Settings
{
    public static class BuildingsSettings
    {
        static Cost[][] costs;
        static uint[] range;
        const string xmlRootString = "BuildingSettings";
        const string xmlTypeString = "Type";
        const string xmlRangeString = "Range";
        static BuildingsSettings()
        {
            costs = new Cost[Enum.GetValues(typeof(CostType)).Length][];
            for (int i = 0; i < costs.Length; ++i)
            {
                costs[i] = new Cost[Enum.GetValues(typeof(BuildingType)).Length];
                for (int j = 0; j < costs[i].Length; ++j)
                {
                    costs[i][j] = new Cost();
                }
            }
            range = new uint[Enum.GetValues(typeof(BuildingType)).Length];
        }
        public static uint GetSightRange(BuildingType type)
        {
            return range[(int)type];
        }
        public static Cost GetBuildCost(BuildingType type)
        {
            return costs[(int)CostType.Build][(int)type];
        }
        public static Cost GetUpkeepCost(BuildingType type)
        {
            return costs[(int)CostType.Upkeep][(int)type];
        }
        public static void SetSightRange(BuildingType type, uint sightRange)
        {
            range[(int)type] = sightRange;
        }
        public static void SetBuildCost(BuildingType type, Cost cost)
        {
            costs[(int)CostType.Build][(int)type] = cost;
        }
        public static void SetUpkeepCost(BuildingType type, Cost cost)
        {
            costs[(int)CostType.Upkeep][(int)type] = cost;
        }
        static XmlNode writeBuildingNode(XmlDocument doc, BuildingType type)
        {
            XmlElement building = doc.CreateElement(type.ToString());
            foreach (CostType ctype in Enum.GetValues(typeof(CostType)))
            {
                var elem = costs[(int)ctype][(int)type].GetXmlElement(doc);
                elem.SetAttribute(xmlTypeString, ctype.ToString());
                building.AppendChild(elem);
            }
            building.SetAttribute(xmlRangeString, range[(int)type].ToString());
            return building;
        }
        static void readBuildingNode(XmlElement node)
        {
            BuildingType bud;
            if (Enum.TryParse(node.Name, out bud))
            {
                uint r;
                if (uint.TryParse(node.GetAttribute(xmlRangeString), out r))
                {
                    range[(int)bud] = r;
                }
                foreach (XmlElement cs in node)
                {
                    CostType ctype;
                    if (Enum.TryParse(cs.GetAttribute(xmlTypeString), out ctype))
                    {
                        Cost tmp = Cost.GetFromXmlElement(cs);
                        if (tmp != null)
                        {
                            costs[(int)ctype][(int)bud] = Cost.GetFromXmlElement(cs);
                        }
                    }
                }
            }
        }
        public static bool Save(string name)
        {
            Random ran = new Random();
            var tab = (MaterialType[])Enum.GetValues(typeof(MaterialType));
            #region test
#if DEBUG && false
#warning losowe generowanie kosztów
            for (int i = 0; i < costs[(int)CostType.Build].Length; ++i)
            {
                Cost c = new Cost();
                for (int j = ran.Next(3); j < 8; ++j)
                {
                    var m = new Material(tab[ran.Next(tab.Length)], (uint)ran.Next(1, 4) * 50);
                    c += m;
                }
                costs[(int)CostType.Build][i] = c;
            }
            for (int i = 0; i < costs[(int)CostType.Upkeep].Length; ++i)
            {
                Cost c = new Cost();
                for (int j = ran.Next(2); j < 1; ++j)
                {
                    var m = new Material(tab[ran.Next(tab.Length)], (uint)ran.Next(1, 4) * 50);
                    c += m;
                }
                costs[(int)CostType.Upkeep][i] = c;
            }
#endif
            #endregion
            XmlDocument doc = new XmlDocument();
            XmlElement root = (XmlElement)doc.AppendChild(doc.CreateElement(xmlRootString));
            foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
            {
                root.AppendChild(writeBuildingNode(doc, type));
            }
            doc.Save(name);
            return true;
        }
        public static bool Load(string name)
        {
            for (int i = 0; i < costs.Length; ++i)
            {
                for (int j = 0; j < costs[i].Length; ++j)
                {
                    costs[i][j] = new Cost();
                }
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(name);
            if (doc.DocumentElement.Name != xmlRootString)
            {
                return false;
            } 
            foreach (XmlElement build in doc.DocumentElement)
            {
                readBuildingNode(build);
            }
            return true;
        }
        private enum CostType
        {
            Build,
            Upkeep
        }
    }
}
