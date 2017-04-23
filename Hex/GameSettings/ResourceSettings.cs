using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hex.GameSettings
{
    public static class ResourceSettings
    {
        static byte[] lay;
        const byte NUMOFLAYERS = 8;
        const string xmlRootName = "ResourceSettings";
        const string xmlLayerName = "Layer";
        const string xmlLayersNode = "Layers";
        static ResourceSettings()
        {
            lay = new byte[Enum.GetValues(typeof(ResourceType)).Length];
        }
        public static int GetLayer(ResourceType type)
        {
            return lay[(int)type];
        }
        public static void SetLayer(ResourceType type, byte layer)
        {
            lay[(int)type] = layer;
        }
        private static XmlElement writeResourceElement(XmlDocument doc, ResourceType res)
        {
            XmlElement elem = doc.CreateElement(res.ToString());
            elem.SetAttribute(xmlLayerName, lay[(int)res].ToString());
            return elem;
        }
        private static void readResourceElement(XmlElement elem)
        {
            ResourceType typ;
            byte l;
            if (Enum.TryParse(elem.Name, out typ) && byte.TryParse(elem.GetAttribute(xmlLayerName), out l))
            {
                lay[(int)typ] = l >= NUMOFLAYERS ? (byte)(NUMOFLAYERS - 1) : l;
            }
        }
        public static bool Save(string name)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(xmlRootName);
            foreach (ResourceType res in Enum.GetValues(typeof(ResourceType)))
            {
                root.AppendChild(writeResourceElement(doc, res));
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
            foreach (XmlElement elem in doc.DocumentElement)
            {
                readResourceElement(elem);
            }
            return true;
        }
        public static byte NumberOfLayers
        {
            get { return NUMOFLAYERS; }
        }
    }
}
