using System.Collections.Generic;
using TW.BattleShip.Models;

namespace TW.BattleShip.BusinessContracts
{
    public interface IBattleshipBoardService
    {
        IEnumerable<Player> GetPlayers();

        BattleshipBoard GetBattleshipBoard();
    }
}
