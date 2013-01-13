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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       
        KeyboardState oldState;
        MouseState mouseStateCurrent;
        
        private Hero hero;
        private Ennemi ennemi;
        private Menu menu;

        Texture2D bg;
        Vector2 bgPosition = new Vector2(30, 0);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            oldState = Keyboard.GetState();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            hero = new Hero();
            hero.Initialize(488, 494);

            ennemi = new Ennemi();
            ennemi.Initialize(0, 489);

            menu = new Menu();
            menu.Initialize(540);

            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = this.Content.Load<Texture2D>("bg");

            hero.LoadContent(Content,"hero", "heroanime", 1, 16);
            ennemi.LoadContent(Content, "voitureanime", 1, 4);
            menu.LoadContent(Content);
           }

        protected override void UnloadContent() {}

        protected override void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
            KeyboardState newState = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || newState.IsKeyDown(Keys.Escape))
                this.Exit();

            //menu
            menu.Update(gameTime, 9);
            
            //position & animation hero
            hero.Update(gameTime, 0.6f);
          
            //animation ennemi
            ennemi.Update(gameTime, 0.5f);
            
            //faire defiler la map
            bgPosition.X -= 10;

            //faire repeter la map
            if (bgPosition.X == -100)
                bgPosition.X = 0;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //DEBUT

            if (menu.realGameState.State != "inGame")
            {
                menu.Draw(spriteBatch, gameTime);
            }
            else
            {
                spriteBatch.Draw(bg, new Rectangle(0, 0, 1280, 800), Color.White);
                spriteBatch.Draw(bg, bgPosition, Color.White);

                hero.Draw(spriteBatch, gameTime);
                ennemi.Draw(spriteBatch, gameTime);
            }
           
            spriteBatch.End(); //FIN
            base.Draw(gameTime);
        }
    }
}
