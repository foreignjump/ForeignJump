using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForeignJump
{
    class Bonus
    {
        public static void Execute(int random, ref bool bonusVitesse)
        {
            switch (random)
            {
                case 0:
                    {
                        GameState.State = "KeyBonusGame";
                        break;
                    }
                case 1:
                    {
                        GameState.State = "newGame";
                        break;
                    }
                case 2:
                    {
                        bonusVitesse = true;
                        break;
                    }
            }
        }
    }
}
