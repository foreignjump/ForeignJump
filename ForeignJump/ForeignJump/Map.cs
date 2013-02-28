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
        public Texture2D obstacle, nuage, nulle, terre, sousterre, piece1, terre1, terre2;
        public Object[,] objets;
        //
        public List<Rectangle> list_obstacle;
        public List<Rectangle> list_eau;
        public int nombreobs;
        public int nombre_vide;
        //
        public List<Rectangle> Piece
        {
            get { return piece; }
            set { piece = value; }
        }
        private List<Rectangle> piece;
        public int nombre_de_piece;

        public void LoadContent(ContentManager Content, string name)
        {
            //
            list_obstacle = new List<Rectangle>();
            list_eau = new List<Rectangle>();
            piece = new List<Rectangle>();

            //tableau d'obstacles
            objets = Ressources.GetPerso(Perso.Choisi).objets;

            #region Textures
            obstacle = Ressources.GetPerso(Perso.Choisi).obstacle;
            nuage = Ressources.GetPerso(Perso.Choisi).nuage;
            nulle = Ressources.GetPerso(Perso.Choisi).nulle;
            terre = Ressources.GetPerso(Perso.Choisi).terre;
            sousterre = Ressources.GetPerso(Perso.Choisi).sousterre;
            piece1 = Ressources.GetPerso(Perso.Choisi).piece;
            terre1 = Ressources.GetPerso(Perso.Choisi).terre1;
            terre2 = Content.Load<Texture2D>("terre2");
            #endregion

            //Traitement du texte
            StreamReader rd = new StreamReader(name);
            string line;

            int posX = 0, posY = 0;
            while ((line = rd.ReadLine()) != null)
            {
                foreach (char chara in line)
                {
                    Object objet = new Object();
                    objet.position = new Vector2(posX * 45, posY * 45);
                    objets[posX, posY] = objet;

                    switch (chara)
                    {
                        case '1':
                            {
                                objet.texture = obstacle;
                                list_obstacle.Add(new Rectangle(Convert.ToInt32(objet.position.X), Convert.ToInt32(objet.position.Y), 10, 45));
                                nombreobs++;
                                break;
                            }
                        case '0':
                            {
                                objet.texture = terre;
                                break;
                            }
                        case '7':
                            {
                                objet.texture = terre2;
                                break;
                            }
                        case '9':
                            {
                                objet.texture = terre1;
                                break;
                            }
                        case '8':
                            {
                                objet.texture = sousterre;
                                break;
                            }
                        case '3':
                            {
                                objet.texture = nulle;
                                list_eau.Add(new Rectangle(Convert.ToInt32(objet.position.X), Convert.ToInt32(objet.position.Y), 45, 45));
                                nombre_vide = nombre_vide + 1;
                                break;
                            }
                        case '2':
                            {

                                objet.texture = piece1;
                                piece.Add(new Rectangle(Convert.ToInt32(objet.position.X), Convert.ToInt32(objet.position.Y), 45, 45));
                                nombre_de_piece = nombre_de_piece + 1;
                                break;
                            }
                        default:
                            {
                                objet.texture = nulle;
                                break;
                            }
                    }
                    posX++;
                }
                posX = 0;
                posY++;
            }
        }

        public List<Rectangle> getpos_obstacle()
        {
            return list_obstacle;
        }
        public List<Rectangle> getpos_eau()
        {
            return list_eau;
        }
        public int nombobs()
        {
            return nombreobs;
        }
        public int nombobs_vide()
        {
            return nombre_vide;
        }
    }
}
