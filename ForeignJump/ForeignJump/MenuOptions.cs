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
    public class MenuOptions
    {
        KeyboardState oldState; //gestion clavier

        private Texture2D menubg; //bg

        private Texture2D fullscreenOn; //fullscreenOn
        private Texture2D fullscreenOff; //fullscreenOff
        private Texture2D fullscreen; //fullscreen

        private int selectionFullscreen; //selection du Fullscreen


        public MenuOptions()
        { }

        public void Initialize()
        {
            oldState = Keyboard.GetState();

            //initialiser la selection à 1 donc sur off
            selectionFullscreen = 1;

        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("MenuOptions");

            fullscreenOff = Content.Load<Texture2D>("OptionsFullscreenOff");
            fullscreenOn = Content.Load<Texture2D>("OptionsFullscreenOn");
            fullscreen = fullscreenOff;
        }

        public void Update(GameTime gameTime, int vitesse, GraphicsDeviceManager graphics)
        {
            var newState = Keyboard.GetState(); //mettre à jour le clavier

            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                GameState.State = "initial"; //retour au menu

            if (newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left) && selectionFullscreen == 1)
            {
                selectionFullscreen = 0;
                fullscreen = fullscreenOn;
                graphics.ToggleFullScreen();
            }

            if (newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right) && selectionFullscreen == 0)
            {
                selectionFullscreen = 1;
                fullscreen = fullscreenOff;
                graphics.ToggleFullScreen();
            }

            

            oldState = newState;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);

            spriteBatch.Draw(fullscreen, new Rectangle(30,120, 697, 104), Color.White);
        }
    }
}