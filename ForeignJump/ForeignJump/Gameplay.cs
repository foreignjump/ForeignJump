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
	class Gameplay
	{
        public SpriteFont font;

        private Map map;
        private Hero hero;
        private Ennemi ennemi;
        private Camera camera;

        #region Barre distance Textures
        private Texture2D barregreenleft;
        private Texture2D barregreencenter;
        private Texture2D barregreenright;
        private Texture2D barreredleft;
        private Texture2D barreredcenter;
        private Texture2D barreredright;
        #endregion

        private float distance;

        public Gameplay()
        {

        }

        public void Initialize()
        {
            map = new Map("map.txt");
            hero = new Hero(Ressources.GetPerso(Perso.Choisi).heroAnime, new Vector2(450, 500), new Vector2(200, 0), 90, map);
            ennemi = new Ennemi(Ressources.GetPerso(Perso.Choisi).ennemiAnime, new Vector2(250, 500), hero, map);
            hero.ennemi = ennemi;
            camera = new Camera(map, hero, ennemi);
            ennemi.camera = this.camera;
            distance = hero.positionGlobale.X - ennemi.positionGlobale.X;
        }

        public void LoadContent()
        {
            font = Ressources.GetPerso(Perso.Choisi).font;
            barregreencenter = Ressources.GetPerso(Perso.Choisi).barregreencenter;
            barregreenleft = Ressources.GetPerso(Perso.Choisi).barregreenleft;
            barregreenright = Ressources.GetPerso(Perso.Choisi).barregreenright;
            barreredcenter = Ressources.GetPerso(Perso.Choisi).barreredcenter;
            barreredleft = Ressources.GetPerso(Perso.Choisi).barreredleft;
            barreredright = Ressources.GetPerso(Perso.Choisi).barreredright;
            map.Load();
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.State == "inGame" && KB.New.IsKeyDown(Keys.Escape))
                GameState.State = "menuPause";

            camera.Position = new Vector2(hero.positionGlobale.X - 500, camera.Position.Y);
            hero.positionLocale.X = hero.positionGlobale.X - camera.Position.X + hero.positionInitiale.X;
            hero.positionLocale.Y = hero.positionGlobale.Y;
            hero.Update(gameTime, 0.3f);
            ennemi.Update(gameTime, 0.3f);

            //mise à jour de la distance entre l'ennemi et le hero
            if (distance > 10)
                distance = hero.positionGlobale.X - ennemi.positionGlobale.X - hero.container.Width;
            else if (distance < 10)
                distance = 10;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            camera.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Nombre de Pieces :" + Convert.ToString(Statistiques.Score), new Vector2(60, 100), Color.Black);


            //afficher la barre en rouge si distance en danger
            if (distance < 80)
            {
                spriteBatch.Draw(barreredleft, new Rectangle(60, 30, barreredleft.Width, barreredleft.Height), Color.White);
                spriteBatch.Draw(barreredcenter, new Rectangle(60 + barreredleft.Width, 30, (int)distance + 10, barreredcenter.Height), Color.White);
                spriteBatch.Draw(barreredright, new Rectangle((int)distance + 10 + 60 + barreredleft.Width, 30, barreredright.Width, barreredright.Height), Color.White);
            }
            else //afficher la barre en vert sinon
            {
                spriteBatch.Draw(barregreenleft, new Rectangle(60, 30,barregreenleft.Width, barregreenleft.Height), Color.White);
                spriteBatch.Draw(barregreencenter, new Rectangle(60 + barregreenleft.Width, 30, (int)distance + 10, barregreencenter.Height), Color.White);
                spriteBatch.Draw(barregreenright, new Rectangle((int)distance + + 10 + 60 + barregreenleft.Width, 30, barregreenright.Width, barregreenright.Height), Color.White);
            }

        }

	}
}
