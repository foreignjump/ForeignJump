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

        KeyboardState oldState; //gestion clavier
        MouseState mouseStateCurrent;

        private Menu menu; //déclaration de menu initial
        private Menupause menupause; //déclaration de menu pause
        private Gameplay game; //déclaration du gameplay

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;

            //this.graphics.IsFullScreen = true;

            graphics.ApplyChanges();
            oldState = Keyboard.GetState(); //initialisation clavier
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            menu = new Menu();
            menu.Initialize(-37, 0); //initialisation menu

            game = new Gameplay();
            game.Initialize(); //initialisation game

            menupause = new Menupause();
            menupause.Initialize(450, 0); //initialisation menu pause

            GameState.State = 0; //mise à l'état initial

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent(Content); //charger menu
            game.LoadContent(Content); //charger game
            menupause.LoadContent(Content); //charger menu pause
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState(); //gestion souris
            KeyboardState newState = Keyboard.GetState(); //verification clavier

            if (GameState.State == 1 && newState.IsKeyDown(Keys.Escape)) //jouer
                GameState.State = 2;

            if (GameState.State == 0) //mise à jour menu
                menu.Update(gameTime, 10);

            if (GameState.State == 1) //mise à jour game
                game.Update(gameTime);

            if (GameState.State == 2) //mise à jour menu pause
                menupause.Update(gameTime, 5);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //DEBUT

            menu.Draw(spriteBatch, gameTime, true); //afficher menu

            if (GameState.State == 1 || GameState.State == 2) //afficher jeu
                game.Draw(spriteBatch, gameTime);

            if (GameState.State == 2) //afficher menu pause
                menupause.Draw(spriteBatch, gameTime);

            spriteBatch.End(); //FIN
            
            base.Draw(gameTime);
        }



    }
}