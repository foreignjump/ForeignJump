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
    public class Gameplay
    {
        SpriteFont font; //police pour afficher texte

        private Hero hero;
        private Ennemi ennemi;
        private Map map;
        private Camera camera;

        //barre de parcours
        private Texture2D barre;
        private float barreWidth;
        private Texture2D glass;
        private float metres; //metres parcourus

        //collision
        private bool toucheTop; //collision avec le haut de l'obstacle
        private int a; //obstacle entré en collision

        //piece
        public int nombre_de_piece;
        Rectangle poss;
        //
        public List<Rectangle> Piece
        {
            get { return piece; }
            set { piece = value; }
        }
        private List<Rectangle> piece;
        //
        private bool collision;
        private bool vatefairefoutre; //quand ça tombe ça avance pas
        private bool tombe; //s'il n'est pas tombé

        private Texture2D bg;


        public void Initialize()
        {
            metres = 0;
            barreWidth = 0;

            hero = new Hero(550, 511);
            hero.Initialize();

            ennemi = new Ennemi(0, 506);
            ennemi.Initialize();

            map = new Map();

            collision = false;
        }

        public void LoadContent(ContentManager Content)
        {
            bg = Ressources.GetPerso(Perso.Choisi).bg;
            barre = Ressources.GetPerso(Perso.Choisi).barre;
            glass = Ressources.GetPerso(Perso.Choisi).glass;
            font = Ressources.GetPerso(Perso.Choisi).font;

            hero.LoadContent(Content);
            ennemi.LoadContent(Content);

            map.LoadContent(Content, Ressources.GetPerso(Perso.Choisi).path);

            camera = new Camera(map, hero, ennemi, new Vector2(0,0), hero.heroAnime);
        }

        public void NewGame()
        {
            hero.Initialize();
            ennemi.Initialize();
            hero.posSol.Y = 511;
            tombe = false;
            metres = 0;
            barreWidth = 0;
            nombre_de_piece = 0;
        }

        public void GameOver()
        {
            GameState.State = "GameOver";
        }

        public void Update(GameTime gameTime)
        {
            poss = new Rectangle(Convert.ToInt32(hero.position.X + 550), Convert.ToInt32(hero.position.Y), 60, 163);
 
            if (GameState.State == "inGame" && KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape)) //jouer
                GameState.State = "menuPause";

            if ((tombe == false))
                hero.position.X += 4;

            //position & animation hero
            hero.Update(gameTime, 0.6f);
            //animation ennemi
            ennemi.Update(0.5f);

            //mettre à jour les metres parcourus
            metres += 1f;

            //avancement de la barre
            if (barreWidth <= 375)
                barreWidth += 1f;

            //mise a jour de la camera en fonction du personnage
            camera.position.X = hero.position.X;

            #region detection des obstacles

            List<Rectangle> rectObstacles = map.getpos_obstacle();

            for (int i = 0; i < map.nombobs(); i++)
            {
                //si touche lateralement
                if ((hero.container.Intersects(rectObstacles[i])) && ((hero.container.Y == hero.posSol.Y) || hero.container.Y == (rectObstacles[a].Y - hero.container.Height)) && toucheTop == false)
                {
                    //GameOver();
                    collision = true;
                }
                //si il monte sur l'obstacle
                else if (hero.container.Intersects(rectObstacles[i]) && (hero.container.X + 115 >= rectObstacles[i].X) && (hero.container.Y + 163 > rectObstacles[i].Y))
                {
                    toucheTop = true;
                    a = i;
                    i = map.nombobs(); //forcer de sortir de la boucle (pour ne pas tester le reste)

                }
                else
                {
                    toucheTop = false;
                }
            }


            //si c'est en haut, placer le personnage sur l'obstacle
            if (toucheTop)
            {
                hero.posSol.Y = rectObstacles[a].Y - hero.container.Height;
            }

            //si il retombe
            if (!((hero.container.Intersects(rectObstacles[a]))) && hero.container.X >= rectObstacles[a].X && toucheTop == true)
            {
                hero.posSol.Y = hero.positionInitiale.Y;
                toucheTop = false;
            }
            #endregion

            if ((toucheTop == false) && tombe == false)
            {
                hero.posSol.Y = 511;
            }

            #region detection du trou

            List<Rectangle> postion_eau = map.getpos_eau();
            for (int i = 0; i < map.nombre_vide; i++)
            {
                Rectangle obseau = map.list_eau[i];
                obseau.Y = obseau.Y - 3;
                obseau.Width = obseau.Width / 2;
                if ((poss.Intersects(obseau)))
                {

                    hero.posSol.Y = 1100;
                    vatefairefoutre = true;
                    tombe = true;

                }
            }
            if ((hero.position.Y >= 1000) || collision)
            {
                GameOver();
            }

            #endregion

            #region détection de la pièce

            for (int i = 0; i < map.nombre_de_piece; i++)
            {
                if ((poss.Intersects(map.Piece[i])))
                {
                    map.objets[(map.Piece[i].X) / 45, (map.Piece[i].Y) / 45] = map.objets[10, 10];
                    map.Piece[i] = new Rectangle(Convert.ToInt32(32), Convert.ToInt32(32), 45, 45);
                    AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                    nombre_de_piece = nombre_de_piece + 1;
                }
            }
            #endregion
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(bg, new Rectangle(0, 0, 1280, 800), Color.White);

            //affiche TOUT
            camera.Draw(spriteBatch);

            //affiche STATS
            spriteBatch.DrawString(font, "Distance: " + (int)((hero.position.X - hero.positionInitiale.X) / 25) + " m", new Vector2(20, 45), Color.White);
            spriteBatch.DrawString(font, "Pieces: " + nombre_de_piece, new Vector2(20, 85), Color.White);
            spriteBatch.Draw(barre, new Rectangle(860, 50, (int)barreWidth, 41), Color.White);
            spriteBatch.Draw(glass, new Rectangle(860, 50, 375, 41), Color.White);
        }
    }
}

