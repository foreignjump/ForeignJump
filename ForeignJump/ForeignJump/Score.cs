using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ForeignJump
{
    class Score
    {
        private static List<Resultat> resultats;
        public static List<Resultat> Resultats
        {
            get { return resultats; }
            set { resultats = value; }
        }

        public static List<Resultat> Add(Resultat resultat)
        {
            if (resultats.Count == 0)
            {
                resultats.Add(resultat);
            }
            else
            {
                int i = 0;
                while (i < resultats.Count && resultats[i].Amount > resultat.Amount)
                {
                    i++;
                }
                resultats.Insert(i, resultat);
            }
            return resultats;
        }

        public static List<Resultat> String2List()
        {
            StreamReader reader = new StreamReader("score.txt");
            string text = reader.ReadToEnd();
            for (int i = 0; i < 5; i++)
            {
                string str = reader.ReadLine();
                if (str != null)
                {
                    string[] tableau = str.Split(',');
                    Add(new Resultat(tableau[0], Convert.ToInt32(tableau[1]), tableau[2]));
                }
                else
                {
                    Add(new Resultat("", Convert.ToInt32(0), ""));
                }
            }
            reader.Close();
            return resultats;
        }

        public static string List2String(List<Resultat> liste)
        {
            string str = "";
            for (int i = 0; i < 5; i++)
            {
                if (liste[i].Name != "")
                    str += liste[i].Name + "," + liste[i].Amount + "," + liste[i].Perso + Environment.NewLine;
            }
            return str;
        }
    }
}
