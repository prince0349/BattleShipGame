using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.BattleShip.UI
{
    public class InputValidatorArgs : EventArgs, IInputValidatorArgs
    {
        public string Key { get; set; }
        public IList<string> Input { get; set; }
        public bool IsInputValid { get; set; }
    }
}
