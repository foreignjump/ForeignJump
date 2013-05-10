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
        double t0, t1, t2, m1;
        Random random;
        bool check;
        int i;
        string charappui;
        public bool startgame;
        bool jeufini;

        Texture2D keysStart;
        Texture2D keysBG;
        Texture2D keysYes;
        Texture2D keysNot;
        Texture2D keysOver;

        public void Initialize()
        {
            random = new Random();
            lettre = Convert.ToChar(random.Next(97, 122));
            t0 = 0;
            t1 = 0;
            t2 = 1;
            m1 = 0;
            i = 0;
            VerifTouche = "?";
            charappui = "?";
            startgame = false;
            check = false;
            jeufini = false;
        }

        public void LoadContent()
        {
            font = Ressources.GetPerso(Perso.Choisi).font;
            keysStart = Ressources.Content.Load<Texture2D>("Keys/keysStart");
            keysBG = Ressources.Content.Load<Texture2D>("Keys/keysBG");
            keysYes = Ressources.Content.Load<Texture2D>("Keys/keysYes");
            keysNot = Ressources.Content.Load<Texture2D>("Keys/keysNot");
            keysOver = Ressources.Content.Load<Texture2D>("Keys/keysOver");
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.State == "KeyBonusGame" && KB.New.IsKeyDown(Keys.Escape))
                GameState.State = "menuPause";

            if (KB.New.IsKeyDown(Keys.Enter) && !startgame)
            {
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
                        check = true;
                        i++;
                    }
                    else
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
                {
                    Initialize();
                    GameState.State = "inGame";
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (startgame)
            {
                if (jeufini)
                {
                    spriteBatch.Draw(keysOver, new Rectangle(440, 185, 400, 431), Color.White);
                    spriteBatch.DrawString(font, "Temps Moyen : " + Convert.ToString(Math.Round((m1 / 1000), 3)) + "s", new Vector2(485, 290), Color.Black);
                    spriteBatch.DrawString(font, "Pieces : " + Convert.ToString((int)((1 - (Math.Round((m1 / 1000), 3))) * 100)), new Vector2(510, 330), Color.White);
                }
                else
                {
                    spriteBatch.Draw(keysBG, new Rectangle(240, 100, 800, 600), Color.White);
                    spriteBatch.DrawString(font, "Appuyez sur la touche: " + Convert.ToString(lettre), new Vector2(390, 195), Color.Black);
                    spriteBatch.DrawString(font, Convert.ToString(VerifTouche), new Vector2(300, 600), Color.White);

                    #region Carrés qui comptent les i
                        spriteBatch.Draw(keysNot, new Rectangle(890, 150, 44, 44), Color.White);
                     if (i > 0)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 150, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 197, 44, 44), Color.White);
                     if (i > 1)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 197, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 244, 44, 44), Color.White);
                     if (i > 2)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 244, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 291, 44, 44), Color.White);
                     if (i > 3)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 291, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 338, 44, 44), Color.White);
                     if (i > 4)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 338, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 385, 44, 44), Color.White);
                     if (i > 5)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 385, 44, 44), Color.White);
                    
                        spriteBatch.Draw(keysNot, new Rectangle(890, 432, 44, 44), Color.White);
                     if (i > 6)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 432, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 479, 44, 44), Color.White);
                     if (i > 7)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 479, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 526, 44, 44), Color.White);
                     if (i > 8)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 526, 44, 44), Color.White);

                        spriteBatch.Draw(keysNot, new Rectangle(890, 574, 44, 44), Color.White);
                     if (i > 9)
                        spriteBatch.Draw(keysYes, new Rectangle(890, 574, 44, 44), Color.White);
                    #endregion
                }
            }
            else
            {
                spriteBatch.Draw(keysStart, new Rectangle(240, 100, 800, 600), Color.White);
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