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

        private float distance;
        private Texture2D barre;
        private Texture2D glass;

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
            barre = Ressources.GetPerso(Perso.Choisi).barre;
            glass = Ressources.GetPerso(Perso.Choisi).glass;

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
            
            distance = hero.positionGlobale.X - ennemi.positionGlobale.X - hero.container.Width;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            camera.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Nombre de Pieces :" + Convert.ToString(Statistiques.Score), new Vector2(30, 150), Color.White);
            
            if (distance < 60)
                spriteBatch.Draw(barre, new Rectangle(30, 60, (int)distance, barre.Height), Color.Red);
            else
                spriteBatch.Draw(barre, new Rectangle(30, 60, (int)distance, barre.Height), Color.Green);

            spriteBatch.Draw(barre, new Rectangle((int)(hero.container.X - camera.Position.X), hero.container.Y, hero.container.Width, hero.container.Height), Color.White);
        }

	}
}
