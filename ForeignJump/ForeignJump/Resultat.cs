using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForeignJump
{
    class Resultat
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Perso { get; set; }

        public Resultat(string name, int score, string perso)
        {
            Name = name;
            Amount = score;
            Perso = perso;
        }
    }
}
