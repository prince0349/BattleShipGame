using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.BattleShip.Models
{
    public class Player
    {
        public IList<Battleship> Battleships { get; set; } = new List<Battleship>();
        public IList<string> TargetPositionString { get; set; } = new List<string>();
    }
}
