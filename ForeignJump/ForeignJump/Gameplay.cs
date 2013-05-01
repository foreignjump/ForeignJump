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

        public void Initialize()
        {
            map = new Map("map.txt");
            hero = new Hero(Ressources.GetPerso(Perso.Choisi).heroAnime, new Vector2(0, 500), new Vector2(200, 0), 90, map);
            ennemi = new Ennemi(Ressources.GetPerso(Perso.Choisi).ennemiAnime, new Vector2(-100, 500), hero, map);
            hero.ennemi = ennemi;
            camera = new Camera(map, hero, ennemi);
            ennemi.camera = this.camera;
            font = Ressources.GetPerso(Perso.Choisi).font;
        }

        public void LoadContent()
        {
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
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            camera.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Nombre de Pieces :" + Convert.ToString(Statistiques.Score), new Vector2(30, 40), Color.White);
        }

	}
}
