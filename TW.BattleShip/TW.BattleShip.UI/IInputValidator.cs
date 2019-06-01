using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.BattleShip.UI
{
    public interface IInputValidator
    {
        void Validate(string key, IList<string> input);
    }
}
