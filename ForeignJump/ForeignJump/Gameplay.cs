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
        private Hero hero;
        private Ennemi ennemi;

        Texture2D bg;
        Vector2 bgPosition = new Vector2(30, 0);

        public void Initialize()
        {
            hero = new Hero();
            hero.Initialize(488, 494);

            ennemi = new Ennemi();
            ennemi.Initialize(0, 489);

        }

        public void LoadContent(ContentManager Content)
        {
            bg = Content.Load<Texture2D>("bg");

            hero.LoadContent(Content, "hero", "heroanime", 1, 16);
            ennemi.LoadContent(Content, "voitureanime", 1, 4);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();

                //position & animation hero
                hero.Update(gameTime, 0.6f, 1);

                //animation ennemi
                ennemi.Update(gameTime, 0.5f);

                //faire defiler la map
                bgPosition.X -= 10;

                //faire repeter la map
                if (bgPosition.X == -100)
                    bgPosition.X = 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
                spriteBatch.Draw(bg, new Rectangle(0, 0, 1280, 800), Color.Black);
                spriteBatch.Draw(bg, bgPosition, Color.White);

                hero.Draw(spriteBatch, gameTime);
                ennemi.Draw(spriteBatch, gameTime);        
       }
   }
}
