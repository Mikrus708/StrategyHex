using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hex.Buildings;

namespace Hex
{
    public class Field
    {
        const int MAXSIZE = 8;
        Resource[] stack = new Resource[MAXSIZE];
        byte top;
        Building building = null;
        FiledType type;
        public Field(FiledType type)
        {
            this.type = type;
            top = 0;
        }
        public FiledType Type
        {
            get { return type; }
            set { type = value; }
        }
        public bool PushResource(Resource resource)
        {
            if (top < MAXSIZE)
            {
                stack[top] = resource;
                ++top;
                return true;
            }
            return false;
        }
        public void PopResource()
        {
            top -= top > 0 ? (byte)1 : (byte)0;
        }
        public Resource? PeekResource()
        {
            return top == 0 ? (Resource?)null : stack[top - 1];
        }
        public int NumberOfResources
        {
            get { return top; }
        }
        public bool Empty
        {
            get { return top == 0; }
        }
        public Resource[] Resources
        {
            get { return stack; }
        }
        public Building Building
        {
            get { return building; }
        }
        public bool Build(Building building)
        {
            if (this.building != null)
            {
                return false;
            }
            this.building = building;
            return true;
        }
    }
    public enum FiledType : byte
    {
        Grass,
        Mountain,
        Sea,
        Forest
    }
}
