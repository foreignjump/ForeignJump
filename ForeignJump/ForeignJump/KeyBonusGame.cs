using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ForeignJump
{
    class KeyBonusGame
    {
        SpriteFont font;
        char lettre;
        string VerifTouche;
        double t0, t1, t2;
        double m1;
        Random random;
        bool check = false;
        int i = 0;
        string charappui;
        bool startgame;
        bool jeufini;

        public void Initialize()
        {
            random = new Random();
            lettre = Convert.ToChar(random.Next(97, 122));

            VerifTouche = "?";

            charappui = "?";

            jeufini = false;
        }

        public void LoadContent()
        {
            font = Ressources.GetPerso(Perso.Choisi).font;
        }

        public void Update(GameTime gameTime)
        {
            if (KB.New.IsKeyDown(Keys.Enter))
            {
                if (!startgame)
                    t0 = gameTime.TotalGameTime.TotalMilliseconds;

                startgame = true;
            }

            if (startgame == true)
            {
                if (i < 10)
                {
                    if (check == true)
                    {
                        lettre = Convert.ToChar(random.Next(97, 122));
                        check = false;
                    }

                    check = false;

                    charappui = Convert.ToString(KeyToChar());

                    if (KB.IsAnyKeyPressed())
                    VerifTouche = "Appuyez sur une touche...";

                    if (lettre == KeyToChar())
                    {
                        if (!KB.IsAnyKeyPressed())
                            VerifTouche = "Vrai";    

                        check = true;
                        i++;
                    }
                    else if (lettre != KeyToChar())
                    {
                        if (!KB.IsAnyKeyPressed())
                        VerifTouche = "Faux";     
                    }
                }
                else
                {
                    if (i < 11)
                    {
                        t1 = gameTime.TotalGameTime.TotalMilliseconds;
                        t2 = t1 - t0;
                        i++;
                        m1 = t2 / 10;
                        Statistiques.Score += (int)((1 - (Math.Round((m1 / 1000), 3))) * 100);
                    }
                }

                if (i == 11)
                    jeufini = true;


                if (jeufini && KB.New.IsKeyDown(Keys.Enter))
                    GameState.State = "inGame";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).bg, new Rectangle(240, 100, 800, 600), Color.Green);

            if (startgame)
            {
                if (jeufini)
                {
                    spriteBatch.DrawString(font, "Votre temps de reaction moyen: " + Convert.ToString(Math.Round((m1 / 1000), 3)) + " secondes.", new Vector2(300, 300), Color.Black);
                    spriteBatch.DrawString(font, "Vous avez gagne: " + Convert.ToString((int)((1 - (Math.Round((m1 / 1000), 3))) * 100)) + " pieces! Bravo Morray!", new Vector2(300, 340), Color.Black);
                    spriteBatch.DrawString(font, "Appuyez sur ENTREE pour continuer le jeu.", new Vector2(300, 440), Color.Red);
                }
                else
                {
                    spriteBatch.DrawString(font, "Appuyez sur la touche: " + Convert.ToString(lettre), new Vector2(400, 200), Color.Black);
                    spriteBatch.DrawString(font, "Touche appuyee " + charappui, new Vector2(640, 300), Color.Black);

                    if (VerifTouche == "Faux")
                        spriteBatch.DrawString(font, Convert.ToString(VerifTouche), new Vector2(300, 600), Color.Red);
                    else if (VerifTouche == "Vrai")
                        spriteBatch.DrawString(font, Convert.ToString(VerifTouche), new Vector2(300, 600), Color.Green);
                    else
                        spriteBatch.DrawString(font, Convert.ToString(VerifTouche), new Vector2(300, 600), Color.White);
                }
            }
            else
            {
                spriteBatch.DrawString(font, Convert.ToString("Appuyez sur ENTREE pour commencer le jeu"), new Vector2(300, 600), Color.White);
            }
        }

        public char KeyToChar()
        {
            if (KB.New.IsKeyDown(Keys.A) && !KB.Old.IsKeyDown(Keys.A))
                return 'a';
            else if (KB.New.IsKeyDown(Keys.B) && !KB.Old.IsKeyDown(Keys.B))
                return 'b';
            else if (KB.New.IsKeyDown(Keys.C) && !KB.Old.IsKeyDown(Keys.C))
                return 'c';
            else if (KB.New.IsKeyDown(Keys.D) && !KB.Old.IsKeyDown(Keys.D))
                return 'd';
            else if (KB.New.IsKeyDown(Keys.E) && !KB.Old.IsKeyDown(Keys.E))
                return 'e';
            else if (KB.New.IsKeyDown(Keys.F) && !KB.Old.IsKeyDown(Keys.F))
                return 'f';
            else if (KB.New.IsKeyDown(Keys.G) && !KB.Old.IsKeyDown(Keys.G))
                return 'g';
            else if (KB.New.IsKeyDown(Keys.H) && !KB.Old.IsKeyDown(Keys.H))
                return 'h';
            else if (KB.New.IsKeyDown(Keys.I) && !KB.Old.IsKeyDown(Keys.I))
                return 'i';
            else if (KB.New.IsKeyDown(Keys.J) && !KB.Old.IsKeyDown(Keys.J))
                return 'j';
            else if (KB.New.IsKeyDown(Keys.K) && !KB.Old.IsKeyDown(Keys.K))
                return 'k';
            else if (KB.New.IsKeyDown(Keys.L) && !KB.Old.IsKeyDown(Keys.L))
                return 'l';
            else if (KB.New.IsKeyDown(Keys.M) && !KB.Old.IsKeyDown(Keys.M))
                return 'm';
            else if (KB.New.IsKeyDown(Keys.N) && !KB.Old.IsKeyDown(Keys.N))
                return 'n';
            else if (KB.New.IsKeyDown(Keys.O) && !KB.Old.IsKeyDown(Keys.O))
                return 'o';
            else if (KB.New.IsKeyDown(Keys.P) && !KB.Old.IsKeyDown(Keys.P))
                return 'p';
            else if (KB.New.IsKeyDown(Keys.Q) && !KB.Old.IsKeyDown(Keys.Q))
                return 'q';
            else if (KB.New.IsKeyDown(Keys.R) && !KB.Old.IsKeyDown(Keys.R))
                return 'r';
            else if (KB.New.IsKeyDown(Keys.S) && !KB.Old.IsKeyDown(Keys.S))
                return 's';
            else if (KB.New.IsKeyDown(Keys.T) && !KB.Old.IsKeyDown(Keys.T))
                return 't';
            else if (KB.New.IsKeyDown(Keys.U) && !KB.Old.IsKeyDown(Keys.U))
                return 'u';
            else if (KB.New.IsKeyDown(Keys.V) && !KB.Old.IsKeyDown(Keys.V))
                return 'v';
            else if (KB.New.IsKeyDown(Keys.W) && !KB.Old.IsKeyDown(Keys.W))
                return 'w';
            else if (KB.New.IsKeyDown(Keys.X) && !KB.Old.IsKeyDown(Keys.X))
                return 'x';
            else if (KB.New.IsKeyDown(Keys.Y) && !KB.Old.IsKeyDown(Keys.Y))
                return 'y';
            else if (KB.New.IsKeyDown(Keys.Z) && !KB.Old.IsKeyDown(Keys.Z))
                return 'z';

            else return '0';


        }
    }
}