using System;
using System.Collections.Generic;
using StrategyHexGame.GUI.Controls;
using StrategyHexGame.Game.Players;

namespace StrategyHexGame.Game
{
    public class GameMaster
    {
        private List<Player> _players;
        private int _currentPlayer;
        private Hex[] _board;
        private List<int[]> _visibleHexs; // [gracz][ID hexa]

        public GameMaster(HexagonalHexGrid grid)
        {
            
        }

        private bool AddPlayer(Player player)
        {
            if (_players.Find(p => p.Name == player.Name) != null)
                return false;
            _players.Add(player);
            if (player.OwnedHexs.Count==0)
                throw new NotImplementedException("Dodaj graczowi na start hex z budynkiem głównym");
            _visibleHexs.Add(new int[0]); // TODO: 
            return true;
        }
        private void NextTurn(object sender, EventArgs e)
        {
            _currentPlayer += _currentPlayer + 1 == _players.Count ? -_currentPlayer : 1;
            foreach (var hex in _board)
                hex.Visibility = System.Windows.Visibility.Hidden;
            for (var i =_visibleHexs[_currentPlayer].Length;--i>=0;)
                if (_visibleHexs[_currentPlayer][i] != 0)
                    _board[i].Visibility = System.Windows.Visibility.Hidden;
            UpdateResourceBar();
        }
        private void UpdateResourceBar()
        {
            throw new NotImplementedException("Tutaj powinna być funkcja aktualizująca stan surowców widziany przez gracza");
        }

    }
}
