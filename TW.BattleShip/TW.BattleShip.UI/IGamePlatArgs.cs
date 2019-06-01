using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.BattleShip.UI
{
    public interface IGamePlatArgs
    {
        IEnumerable<string> Output { get; set; }
    }
}
