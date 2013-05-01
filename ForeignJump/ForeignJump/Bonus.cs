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
                case 1:
                    {
                        GameState.State = "KeyBonusGame";
                        break;
                    }
                case 2:
                    {
                        GameState.State = "newGame";
                        break;
                    }
                case 3:
                    {
                        bonusVitesse = true;
                        break;
                    }
                default:
                    {
                        GameState.State = "GameOver";
                        break;
                    }
            }
        }
    }
}
