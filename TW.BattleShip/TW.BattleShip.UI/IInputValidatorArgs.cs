using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.BattleShip.UI
{
    public interface IInputValidatorArgs
    {
        string Key { get; set; }
        IList<string> Input { get; set; }
        bool IsInputValid { get; set; }
    }
}
