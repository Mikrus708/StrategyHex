using System.Collections.Generic;
using StrategyHexGame.GUI.Controls;
using StrategyHexGame.Game.Base;

namespace StrategyHexGame.Game.Players
{
    public abstract class Player
    {

        public string Name { get; private set; }
        private static int _id;
        public List<Hex> OwnedHexs { get; private set; }
        public Resource Resource { get; private set; }
        protected Player()
        {
            Name = $"Player {_id++}";
            OwnedHexs = new List<Hex>();
            Resource = default(Resource);
        }
    }
}
