using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex
{
    public struct Resource
    {
        ResourceType type;
        uint ammount;
        public Resource (ResourceType type, uint ammount)
        {
            this.type = type;
            this.ammount = ammount;
        }
        public ResourceType Type
        {
            get { return type; }
        }
        public uint Ammount
        {
            get { return ammount; }
        }
        public uint Gather(uint ammount)
        {
            uint result = Math.Min(this.ammount, ammount);
            this.ammount -= result;
            return result;
        }
        public uint Add(uint ammount)
        {
            return this.ammount += ammount;
        }
        public bool Empty
        {
            get { return ammount == 0; }
        }
    }
    public enum ResourceType : byte
    {
        Forest,
        Stone,
        Grain,
        Fishes,
        Gold,
        Iron,
        Coal
    }
}
