using System.Collections.Generic;

namespace Hex.Players
{
    public abstract class Player
    {

        public string Name { get; private set; }
        private static int _id;
        public List<WpfHex.Hex> OwnedHexs { get; private set; }
        public Resource Resource { get; private set; }
        protected Player()
        {
            Name = $"Player {_id++}";
            OwnedHexs = new List<WpfHex.Hex>();
            Resource = default(Resource);
        }
    }
}
