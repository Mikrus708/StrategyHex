using StrategyHexGame.Game.Base;
using System;
using System.Xml;

namespace StrategyHexGame.Game.Settings
{
    public static class MaterialSettings
    {
        const string xmlRootName = "MaterialSettings";
        const string xmlTransNode = "GetFrom";
        static MaterialType[] transform;
        static MaterialSettings()
        {
            transform = new MaterialType[Enum.GetValues(typeof(ResourceType)).Length];
        }
        public static MaterialType GetMaterialFromResource(ResourceType resource)
        {
            return transform[(int)resource];
        }
        public static void SetMaterialFromResource(ResourceType resource, MaterialType type)
        {
            transform[(int)resource] = type;
        }
        static XmlNode writeTransformationNode(XmlDocument doc, MaterialType type)
        {
            XmlElement node = doc.CreateElement(xmlTransNode);
            foreach (ResourceType res in Enum.GetValues(typeof(ResourceType)))
            {
                if (transform[(int)res] == type)
                {
                    XmlElement trans = doc.CreateElement(res.ToString());
                    node.AppendChild(trans);
                }
            }
            return node;
        }
        static void readTransformationNode(XmlElement root, MaterialType type)
        {
            XmlElement node = (XmlElement)root.SelectSingleNode(xmlTransNode);
            if (node != null)
            {
                foreach (XmlElement elem in node)
                {
                    ResourceType rt;
                    if (Enum.TryParse(elem.Name, out rt))
                    {
                        transform[(int)rt] = type;
                    }
                }
            }
        }
        static XmlElement writeMaterialElement(XmlDocument doc, MaterialType type)
        {
            XmlElement elem = doc.CreateElement(type.ToString());
            elem.AppendChild(writeTransformationNode(doc, type));
            return elem;
        }
        static void readMaterialElement(XmlElement root, MaterialType type)
        {
            XmlElement node = (XmlElement)root.SelectSingleNode(type.ToString());
            if (node != null)
            {
                readTransformationNode(node, type);
            }
        }
        public static bool Save(string name)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(xmlRootName);
            foreach (MaterialType type in Enum.GetValues(typeof(MaterialType)))
            {
                root.AppendChild(writeMaterialElement(doc, type));
            }
            doc.AppendChild(root);
            doc.Save(name);
            return true;
        }
        public static bool Load(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(name);
            XmlElement root = doc.DocumentElement;
            if (root.Name != xmlRootName)
            {
                return false;
            }
            foreach (MaterialType type in Enum.GetValues(typeof(MaterialType)))
            {
                readMaterialElement(root, type);
            }
            return true;
        }
    }
}
