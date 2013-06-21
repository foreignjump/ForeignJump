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
using System.IO;

namespace ForeignJump
{
    class Map
    {
        StreamReader stream;
        //
        public static List<Rectangle> ListPiece = new List<Rectangle>();
        public static List<Rectangle> ListBonus = new List<Rectangle>();
        public static List<Rectangle> ListBombe = new List<Rectangle>();
        public static List<Rectangle> ListACDC = new List<Rectangle>();
        //
        private Objet[,] objets;
        public Objet[,] Objets
        {
            get { return objets; }
            set { objets = value; }
        }

        public Map(string file)
        {
            stream = new StreamReader(file);
        }

        public void Load()
        {
            objets = Ressources.GetPerso(Perso.Choisi).objets;

            string line;

            int i = 0, j = 0;

            while ((line = stream.ReadLine()) != null)
            {
                foreach (char chara in line)
                {
                    Objet objet = new Objet();
                    objet.position = new Vector2(i * 45, j * 45);
                    objet.container = new Rectangle((int)objet.position.X, (int)objet.position.Y, 45, 45);

                    switch (chara)
                    {
                        case '1':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).obstacle;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '0':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).terre;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '7':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).terre2;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '9':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).terre1;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '8':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).sousterre;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '3':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).nulle;
                                objet.type = TypeCase.Eau;
                                break;
                            }
                        case '2':
                            {
                                ListPiece.Add(new Rectangle((int)objet.position.X, (int)objet.position.Y, (int)objet.container.Width, (int)objet.container.Height));
                                objet.texture = Ressources.GetPerso(Perso.Choisi).piece;
                                objet.type = TypeCase.Piece;
                                break;
                            }
                        case 'c':
                            {
                                ListBombe.Add(new Rectangle((int)objet.position.X, (int)objet.position.Y, (int)objet.container.Width, (int)objet.container.Height));
                                objet.texture = Ressources.GetPerso(Perso.Choisi).bombe;
                                objet.type = TypeCase.Bombe;
                                break;
                            }
                        case 'B':
                            {
                                ListBonus.Add(new Rectangle((int)objet.position.X, (int)objet.position.Y, (int)objet.container.Width, (int)objet.container.Height));
                                objet.texture = Ressources.GetPerso(Perso.Choisi).bonus;
                                objet.type = TypeCase.Bonus;
                                break;
                            }
                        case 'A':
                            {
                                ListACDC.Add(new Rectangle((int)objet.position.X, (int)objet.position.Y, (int)objet.container.Width, (int)objet.container.Height));
                                objet.texture = Ressources.Content.Load<Texture2D>("ACDCmap");
                                objet.type = TypeCase.ACDC;
                                break;
                            }
                        default:
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).nulle;
                                objet.type = TypeCase.Null;
                                break;
                            }
                    }
                    objets[i, j] = objet;
                    i++;
                }
                i = 0;
                j++;
            }

        }

        public void LoadMulti()
        {
            objets = new Objet[1000,19];

            string line;

            int i = 0, j = 0;

            while ((line = stream.ReadLine()) != null)
            {
                foreach (char chara in line)
                {
                    Objet objet = new Objet();
                    objet.position = new Vector2(i * 45, j * 45);
                    objet.container = new Rectangle((int)objet.position.X, (int)objet.position.Y, 45, 45);

                    switch (chara)
                    {
                        case '1':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).obstacle;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '0':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).terre;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '7':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).terre2;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '9':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).terre1;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '8':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).sousterre;
                                objet.type = TypeCase.Terre;
                                break;
                            }
                        case '3':
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).nulle;
                                objet.type = TypeCase.Eau;
                                break;
                            }
                        case '2':
                            {
                                ListPiece.Add(new Rectangle((int)objet.position.X, (int)objet.position.Y, (int)objet.container.Width, (int)objet.container.Height));
                                objet.texture = Ressources.GetPerso(Perso.Choisi).piece;
                                objet.type = TypeCase.Piece;
                                break;
                            }
                        case 'c':
                            {
                                ListBombe.Add(new Rectangle((int)objet.position.X, (int)objet.position.Y, (int)objet.container.Width, (int)objet.container.Height));
                                objet.texture = Ressources.GetPerso(Perso.Choisi).bombe;
                                objet.type = TypeCase.Bombe;
                                break;
                            }
                        default:
                            {
                                objet.texture = Ressources.GetPerso(Perso.Choisi).nulle;
                                objet.type = TypeCase.Null;
                                break;
                            }
                    }
                    objets[i, j] = objet;
                    i++;
                }
                i = 0;
                j++;
            }

        }

        public bool Valid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < objets.GetLength(0) && y < objets.GetLength(1);
        }
    }
}
