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

        private float metres; //metres parcourus

        private Texture2D green;
        private float greenWidth;
        private Texture2D glass;
        
        private Texture2D bg;
        private Vector2 bgPosition;

        public void Initialize()
        {
            metres = 0;
            greenWidth = 0;

            bgPosition = new Vector2(30, 0);

            hero = new Hero();
            hero.Initialize(488, 494);

            ennemi = new Ennemi();
            ennemi.Initialize(0, 489);
        }

        public void LoadContent(ContentManager Content)
        {
            bg = Content.Load<Texture2D>("bg");
            green = Content.Load<Texture2D>("green");
            glass = Content.Load<Texture2D>("glass");
            font = Content.Load<SpriteFont>("Font");

            hero.LoadContent(Content, "hero", "heroanime", 1, 16);
            ennemi.LoadContent(Content, "voitureanime", 1, 4);
        }

        public void NewGame()
        {
            ennemi.Initialize(0, 489);
            hero.Initialize(488, 494);

            metres = 0;
            greenWidth = 0;

            bgPosition = new Vector2(30, 0);
        }
    
        public void GameOver()
        {
            if (greenWidth >= 375)
            {
                GameState.State = "GameOver";
            }
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.State == "inGame" && KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape)) //jouer
                GameState.State = "menuPause";

            //position & animation hero
            hero.Update(gameTime, 0.6f, 1);

            //animation ennemi
            ennemi.Update(gameTime, 0.5f);

            //faire defiler la map
            bgPosition.X -= 10;

            //faire repeter la map
            if (bgPosition.X == -100)
                bgPosition.X = 0;

            metres += 1f; //mettre à jour les metres parcourus
            if (greenWidth <= 375)
                greenWidth += 1f;

            GameOver();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(bg, new Rectangle(0, 0, 1280, 800), Color.Black);
            spriteBatch.Draw(bg, bgPosition, Color.White);

            hero.Draw(spriteBatch, gameTime);
            ennemi.Draw(spriteBatch, gameTime);

            spriteBatch.DrawString(font, "Distance: " + (int)metres + " m", new Vector2(20, 45), Color.White);
            spriteBatch.Draw(green, new Rectangle(860, 50, (int)greenWidth, 41), Color.White);
            spriteBatch.Draw(glass, new Rectangle(860, 50, 375, 41), Color.White);
        }
    }
}
