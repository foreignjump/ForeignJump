using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Configuration;
using System.ComponentModel;
using TweetSharp;
using System.Threading;

namespace ForeignJump
{
    class GameOver
    {
        private Texture2D classementBG;
        private SpriteFont font;
        private TwitterService twitterService; //connection Twitter
        
        public static string[] top; //tableau servant à l'affichage des résultats

        public static void Die() //fonction appliquée avant de rentrer dans le GameOver State
        {
            List<Resultat> resultats = new List<Resultat>(); //création d'une nouvelle liste de résultats
            Resultat NewResultat = new Resultat(Statistiques.Name, Statistiques.Score, Perso.Choisi); //stockage du résultat actuel

            //Lecture du fichier texte
            StreamReader reader;
            reader = new StreamReader("score.txt");

            //Transfert du fichier dans une liste
            for (int i = 0; i < 5; i++)
            {
                string str = reader.ReadLine();
                if (str != null)
                {
                    string[] tableau = str.Split(','); //séparation des virgules
                    resultats.Add(new Resultat(tableau[0], Convert.ToInt32(tableau[1]), tableau[2])); //création d'un nouveau resultat et ajout dans la liste
                }
                else
                {
                    resultats.Add(null);
                }
            }
            reader.Close();

            if (resultats[0] == null)
            {
                resultats.Insert(0, NewResultat); //insertion en tête pour la liste vide
            }
            else
            {
                int i = 0;
                while (i < resultats.Count && resultats[i] != null && resultats[i].Amount > NewResultat.Amount)
                {
                    i++; //avance jusqu'à trouver la place
                }
                resultats.Insert(i, NewResultat); //insere le nouveau resultat à la bonne place
            }

            if (resultats[5] != null)
                resultats.RemoveAt(5); //efface le dernier résultat
            
            StreamWriter writer;

            writer = new StreamWriter("score.txt");

            string newStr = "";

            //Ecriture des resultats dans un fichier
            for (int i = 0; i < 5; i++)
            {
                if (resultats[i] != null)
                    newStr += resultats[i].Name + "," + Convert.ToInt32(resultats[i].Amount) + "," + resultats[i].Perso + Environment.NewLine;
            }

            writer.Write(newStr);
            writer.Close();

            //tableau pour afficher les valeurs sur la page
            top = new string[5];

            for (int i = 0; i < 5; i++)
            {
                if (resultats[i] != null)
                    top[i] = resultats[i].Name + " : " + resultats[i].Amount; //si il y a valeur, on affiche Nom : Valeur
                else
                    top[i] = "NONE";
            }
        }


        public GameOver()
        {
            classementBG = Ressources.Content.Load<Texture2D>("Menu/Classement");
            font = Ressources.Content.Load<SpriteFont>("Menu/FontGameOver");

            #region Authentification Twitter

            TwitterClientInfo twitterClientInfo = new TwitterClientInfo();

            twitterClientInfo.ConsumerKey = "TF30IyeUwXRm1Xe38Qig"; //Read ConsumerKey out of the app.config

            twitterClientInfo.ConsumerSecret = "2gUJrzq3JtSSFLkwwaflRaFKxsYvCsDHu1WikEBRcM"; //Read the ConsumerSecret out the app.config

            twitterService = new TwitterService(twitterClientInfo);

            twitterService.AuthenticateWith("1515530498-gZaKFgEILJiF4rfFsbyURF1adSv6CpuqLzrZ9AO", "a0TyYuTBEqc68uq1GiaSCCGICc5MODIlABj0JvyM");
            #endregion
        }

        public void Update()
        {
            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
            {
                //mise à jour du status en fonction du score
                string status;
                if (Statistiques.Score < 5)
                    status = "New score in " + Hero2Map(Perso.Choisi) + " ! " + Statistiques.Name.ToUpper() + " : " + Statistiques.Score + " ! Très NUL! ";
                else if (Statistiques.Score < 10)
                    status = "New score in " + Hero2Map(Perso.Choisi) + " ! " + Statistiques.Name.ToUpper() + " : " + Statistiques.Score + " ! Bof, bof... ";
                else if (Statistiques.Score < 40)
                    status = "New score in " + Hero2Map(Perso.Choisi) + " ! " + Statistiques.Name.ToUpper() + " : " + Statistiques.Score + " ! C'est pas trop mal :)";
                else if (Statistiques.Score < 100)
                    status = "New score in " + Hero2Map(Perso.Choisi) + " ! " + Statistiques.Name.ToUpper() + " : " + Statistiques.Score + " ! Bien, bien! ";
                else
                    status = "New score in " + Hero2Map(Perso.Choisi) + " ! " + Statistiques.Name.ToUpper() + " : " + Statistiques.Score + " ! Excellent! ";

                //utilisation d'un nouveau thread 
                ThreadStart threadStarter = delegate
                {
                    //twitter
                    twitterService.SendTweet(new SendTweetOptions { Status = status });
                };
                Thread loadingThread = new Thread(threadStarter);
                loadingThread.Start();

                Statistiques.Score = 0;
                GameState.State = "initial";
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.GetLangue(Langue.Choisie).gameOver, new Rectangle(440, 185, 400, 431), Color.White);
            spriteBatch.Draw(classementBG, new Rectangle(10, 185, classementBG.Width, classementBG.Height), Color.White);
            spriteBatch.DrawString(font, top[0], new Vector2(125, 320), Color.White);
            spriteBatch.DrawString(font, top[1], new Vector2(125, 415), Color.White);
            spriteBatch.DrawString(font, top[2], new Vector2(125, 508), Color.White);
            spriteBatch.DrawString(font, top[3], new Vector2(125, 604), Color.White);
            spriteBatch.DrawString(font, top[4], new Vector2(125, 700), Color.White);
        }

        private string Hero2Map(string perso)
        {
            switch (perso)
            {
                case "roumain":
                    return "Romania";
                case "renoi":
                    return "Benin";
                case "reunionnais":
                    return "Reunion Island";
                default:
                    return "India";
            }
        }
    }
}