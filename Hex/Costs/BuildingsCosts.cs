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

namespace Hex.Costs
{
    public static class BuildingsCosts
    {
        static Cost[] build = new Cost[Enum.GetValues(typeof(BuildingType)).Length];
        static Cost[] upkeep = new Cost[Enum.GetValues(typeof(BuildingType)).Length];
        static readonly string rootElem = "BuildingCosts";
        static readonly string ammoutAttr = "Ammount";
        public static Cost GetBuildCost(BuildingType type)
        {
            return build[(int)type];
        }
        public static Cost GetUpkeepCost(BuildingType type)
        {
            return upkeep[(int)type];
        }
        public static void SetBuildCost(BuildingType type, Cost cost)
        {
            build[(int)type] = cost;
        }
        public static void SetUpkeepCost(BuildingType type, Cost cost)
        {
            upkeep[(int)type] = cost;
        }
        public static bool SaveCosts(string name)
        {
            Random ran = new Random();
            var tab = (MaterialType[])Enum.GetValues(typeof(MaterialType));
#if DEBUG && false
#warning losowe generowanie kosztów
            for (int i = 0; i < build.Length; ++i)
            {
                Cost c = new Cost();
                for (int j = ran.Next(3); j < 8; ++j)
                {
                    var m = new Material(tab[ran.Next(tab.Length)], (uint)ran.Next(1, 4) * 50);
                    c += m;
                }
                build[i] = c;
            }
            for (int i = 0; i < upkeep.Length; ++i)
            {
                Cost c = new Cost();
                for (int j = ran.Next(2); j < 1; ++j)
                {
                    var m = new Material(tab[ran.Next(tab.Length)], (uint)ran.Next(1, 4) * 50);
                    c += m;
                }
                upkeep[i] = c;
            }
#endif
            XmlDocument doc = new XmlDocument();
            XmlElement root = (XmlElement)doc.AppendChild(doc.CreateElement(rootElem));
            foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
            {
                XmlElement building = doc.CreateElement(type.ToString());
                XmlElement bc = doc.CreateElement(CostType.Build.ToString());
                XmlElement uc = doc.CreateElement(CostType.Upkeep.ToString());
                foreach (Material mat in build[(byte)type])
                {
                    XmlElement material = doc.CreateElement(mat.Type.ToString());
                    material.SetAttribute(ammoutAttr, mat.Ammount.ToString());
                    bc.AppendChild(material);
                }
                foreach (Material mat in upkeep[(byte)type])
                {
                    XmlElement material = doc.CreateElement(mat.Type.ToString());
                    material.SetAttribute(ammoutAttr, mat.Ammount.ToString());
                    uc.AppendChild(material);
                }
                building.AppendChild(bc);
                building.AppendChild(uc);
                root.AppendChild(building);
            }
            doc.Save(name);
            return true;
        }
        public static bool LoadCosts(string name)
        {
            for (int i = 0; i < build.Length; ++i)
            {
                build[i] = new Cost();
                upkeep[i] = new Cost();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(name);
            if (doc.DocumentElement.Name != rootElem)
            {
                return false;
            } 
            foreach (XmlElement build in doc.DocumentElement)
            {
                BuildingType bud;
                if (Enum.TryParse(build.Name, out bud))
                {
                    foreach (CostType type in Enum.GetValues(typeof(CostType)))
                    {
                        foreach (XmlElement cos in build.GetElementsByTagName(type.ToString()))
                        {
                            foreach (XmlElement mater in cos)
                            {
                                MaterialType mat;
                                if (Enum.TryParse(mater.Name, out mat))
                                {
                                    uint amm;
                                    if (uint.TryParse(mater.GetAttribute(ammoutAttr), out amm))
                                    {
                                        switch (type)
                                        {
                                            case CostType.Build:
                                                BuildingsCosts.build[(int)bud] += new Material(mat, amm);
                                                break;
                                            case CostType.Upkeep:
                                                BuildingsCosts.upkeep[(int)bud] += new Material(mat, amm);
                                                break;
                                            default:
                                                throw new NotImplementedException($"Brakuje implementacji dla typu CostType.{type} w BuildingsCosts.LoadCosts()");
                                        }
                                    }
                                }
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
