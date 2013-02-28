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
        
        
        private bool collision;

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
            
            metres = 0;
            barreWidth = 0;
        }

        public void GameOver()
        {
            GameState.State = "GameOver";
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.State == "inGame" && KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape)) //jouer
                GameState.State = "menuPause";
           
            if (collision == false)
                hero.position.X += 6;

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

            #region detection du trou

            List<Rectangle> position_trou = map.getpos_eau();

            for (int i = 0; i < map.nombre_vide; i++)
            {
                Rectangle obsTrou = position_trou[i];
                obsTrou.Y = obsTrou.Y - 2;
                obsTrou.Width = obsTrou.Width / 2;
                if ((hero.container.Intersects(obsTrou)))
                    GameOver();
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
            spriteBatch.Draw(barre, new Rectangle(860, 50, (int)barreWidth, 41), Color.White);
            spriteBatch.Draw(glass, new Rectangle(860, 50, 375, 41), Color.White);
        }
    }
}

