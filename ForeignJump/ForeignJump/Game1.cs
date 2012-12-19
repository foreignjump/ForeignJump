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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D bg;
        Texture2D obstacleh;
        Texture2D obstacleb;
        Texture2D voiture;
        Texture2D hero;
        

        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 474;
            graphics.ApplyChanges();

            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bg = this.Content.Load<Texture2D>("bg");
            obstacleb = this.Content.Load<Texture2D>("obstacleb");
            obstacleh = this.Content.Load<Texture2D>("obstacleh");
            hero = this.Content.Load<Texture2D>("hero");
            voiture = this.Content.Load<Texture2D>("voiture");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Utilisation Clavier
            KeyboardState newState = Keyboard.GetState();
            //Souris
            this.IsMouseVisible = true;
            
            Physique.Update(gameTime);
            
            base.Update(gameTime);
        }
    }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            
            spriteBatch.Begin();
                spriteBatch.Draw(bg, new Rectangle(0, 0, 1200, 474), Color.White);
                spriteBatch.Draw(bg, bgPosition, Color.White);                
                spriteBatch.Draw(obstacleb, obstaclebPosition, Color.White);
                spriteBatch.Draw(obstacleh, obstaclehPosition, Color.White);
                spriteBatch.Draw(voiture, voiturePosition, Color.White);
                spriteBatch.Draw(hero, heroPosition, Color.White);
            spriteBatch.End();
        }
    }
}

