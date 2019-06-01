using System.Collections.Generic;
using TW.BattleShip.Models;

namespace TW.BattleShip.BusinessContracts
{
    public interface IGamePlayBoardService
    {
        void FillBoardWithShips(int[,] board, IList<Battleship> battleships);
        void GenerateGamePlayBoard(BattleshipBoard battleshipBoard, IEnumerable<Player> players);
        void PlayGame();
        void FindWinner();
        IEnumerable<string> GetGameOutput();
    }
}
