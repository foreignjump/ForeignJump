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
        private MenuPause menupause; //déclaration de menu pause
        private MenuPauseAide menupauseaide; //déclaration de menu pause
        private MenuAide menuaide; //déclaration du menu aide
        private Gameplay game; //déclaration du gameplay

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;

            //this.graphics.IsFullScreen = true; //fullscreen

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

            menupause = new MenuPause();
            menupause.Initialize(450, 0); //initialisation menu pause

            menupauseaide = new MenuPauseAide();
            menupauseaide.Initialize(); //initialisation menu aide

            menuaide = new MenuAide();
            menuaide.Initialize(); //initialisation menu aide

            GameState.State = "initial"; //mise à l'état initial

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent(Content); //charger menu
            game.LoadContent(Content); //charger game
            menupause.LoadContent(Content); //charger menu pause
            menupauseaide.LoadContent(Content); //charger menu pause aide
            menuaide.LoadContent(Content); //charger menu aide
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState(); //gestion souris
            KeyboardState newState = Keyboard.GetState(); //verification clavier

            if (GameState.State == "inGame" && newState.IsKeyDown(Keys.Escape)) //jouer
                GameState.State = "menuPause";

            if (GameState.State == "initial") //mise à jour menu
                menu.Update(gameTime, 15);

            if (GameState.State == "inGame") //mise à jour game
                game.Update(gameTime);

            //menuPause
            if (GameState.State == "menuPause") //mise à jour menu pause
                menupause.Update(gameTime, 5);

            if (GameState.State == "menuPauseAide") //mise à jour menu pause aide
                menupauseaide.Update(gameTime, 5);
                
            //menuPause

            if (GameState.State == "menuAide") //mise à jour menu aide
                menuaide.Update(gameTime, 5);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //DEBUT

            menu.Draw(spriteBatch, gameTime, true); //afficher menu

            if (GameState.State == "inGame" || GameState.State == "menuPause") //afficher jeu
                game.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuPause") //afficher menu pause
                menupause.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuPauseAide") //afficher menu pause
            {
                game.Draw(spriteBatch, gameTime);
                menupauseaide.Draw(spriteBatch, gameTime);
            }


            if (GameState.State == "menuAide") //afficher menu pause
                menuaide.Draw(spriteBatch, gameTime);

            spriteBatch.End(); //FIN
            
            base.Draw(gameTime);
        }



    }
}