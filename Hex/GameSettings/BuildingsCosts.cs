using Hex.Buildings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Hex.GameSettings
{
    public static class BuildingsCosts
    {
        static Cost[][] costs;
        const string xmlRootString = "BuildingCosts";
        const string xmlTypeString = "Type";
        static BuildingsCosts()
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
        }
        public static Cost GetBuildCost(BuildingType type)
        {
            return costs[(int)CostType.Build][(int)type];
        }
        public static Cost GetUpkeepCost(BuildingType type)
        {
            return costs[(int)CostType.Upkeep][(int)type];
        }
        public static void SetBuildCost(BuildingType type, Cost cost)
        {
            costs[(int)CostType.Build][(int)type] = cost;
        }
        public static void SetUpkeepCost(BuildingType type, Cost cost)
        {
            costs[(int)CostType.Upkeep][(int)type] = cost;
        }
        public static bool SaveCosts(string name)
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
                XmlElement building = doc.CreateElement(type.ToString());
                foreach (CostType ctype in Enum.GetValues(typeof(CostType)))
                {
                    var elem = costs[(int)ctype][(int)type].GetXmlElement(doc);
                    elem.SetAttribute(xmlTypeString, ctype.ToString());
                    building.AppendChild(elem);
                }
                root.AppendChild(building);
            }
            doc.Save(name);
            return true;
        }
        public static bool LoadCosts(string name)
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
                BuildingType bud;
                if (Enum.TryParse(build.Name, out bud))
                {
                    foreach (XmlElement cs in build)
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
            return true;
        }

        private enum CostType
        {
            Build,
            Upkeep
        }
    }
}
