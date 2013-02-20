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
        private Texture2D menubg; //bg

        #region fullscreen
        private Texture2D fullscreenToggleOn; //fullscreen toggle On
        private Texture2D fullscreenToggleOff; //fullscreen toggle Off
        private Texture2D fullscreenToggle; //fullscreen toggle

        private Texture2D fullscreenTextN; //fullscreen text not selected
        private Texture2D fullscreenTextH; //fullscreen text selected
        private Texture2D fullscreenText; //fullscreen text
        #endregion

        #region sound
        private Texture2D soundToggleOn; //sound toggle On
        private Texture2D soundToggleOff; //sound toggle Off
        private Texture2D soundToggle; //sound toggle

        private Texture2D soundTextN; //sound text not selected
        private Texture2D soundTextH; //sound text selected
        private Texture2D soundText; //sound text
        #endregion

        private int selectionFullscreen; //selection du Fullscreen
        private int selectionSound; //selection du Sound

        private int selection; //selection verticale


        public MenuOptions()
        { }

        public void Initialize()
        {
            //initialiser la selection à 0 sur fullscreen
            selection = 0;

            //initialiser la selection à 1 donc sur off
            selectionFullscreen = 1;
        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("Menu/Options/MenuOptions");

            fullscreenTextH = Content.Load<Texture2D>("Menu/Options/fullscreenH");
            fullscreenTextN = Content.Load<Texture2D>("Menu/Options/fullscreenN");
            fullscreenText = fullscreenTextN;

            fullscreenToggleOff = Content.Load<Texture2D>("Menu/Options/off");
            fullscreenToggleOn = Content.Load<Texture2D>("Menu/Options/on");
            fullscreenToggle = fullscreenToggleOff;

            soundTextH = Content.Load<Texture2D>("Menu/Options/soundH");
            soundTextN = Content.Load<Texture2D>("Menu/Options/soundN");
            soundText = soundTextN;

            soundToggleOff = Content.Load<Texture2D>("Menu/Options/off");
            soundToggleOn = Content.Load<Texture2D>("Menu/Options/on");
            soundToggle = soundToggleOn;
        }

        public void Update(GameTime gameTime, int vitesse, GraphicsDeviceManager graphics)
        {
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
                GameState.State = "initial"; //retour au menu

            #region selection

            if (selection == -1) //pour que la selection ne dépasse pas les negatifs
                selection = 1;
            else
                selection = selection % 2; //pour que la selection ne dépasse pas 2

            if (KB.New.IsKeyDown(Keys.Down) && !KB.Old.IsKeyDown(Keys.Down))
                selection++;

            if (KB.New.IsKeyDown(Keys.Up) && !KB.Old.IsKeyDown(Keys.Up))
                selection--;

            #endregion

            #region survoler le menu

            if (selection == 0)
                fullscreenText = fullscreenTextH; //fullscreen selectionné
            else
                fullscreenText = fullscreenTextN;

            if (selection == 1)
                soundText = soundTextH; //sound selectionné
            else
                soundText = soundTextN;

            #endregion

            #region fullscreenToggle
            
            if (selection == 0) //si fullscreen selectionné
            {
                if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left) && selectionFullscreen == 1) //si appuye gauche
                {
                    selectionFullscreen = 0; 
                    fullscreenToggle = fullscreenToggleOn;
                    graphics.ToggleFullScreen(); //changer fullscreen
                }

                if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right) && selectionFullscreen == 0) //si appuye droite
                {
                    selectionFullscreen = 1;
                    fullscreenToggle = fullscreenToggleOff;
                    graphics.ToggleFullScreen(); //changer fullscreen
                }
            }

            #endregion

            #region soundToggle

            if (selection == 1) //is sound selected
            {
                if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left) && selectionSound == 1)
                {
                    selectionSound = 0;
                    soundToggle = soundToggleOn;
                }

                if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right) && selectionSound == 0)
                {
                    selectionSound = 1;
                    soundToggle = soundToggleOff;
                }
            }
            #endregion
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);

            spriteBatch.Draw(fullscreenText, new Rectangle(50, 140, fullscreenText.Width, fullscreenText.Height), Color.White);
            spriteBatch.Draw(fullscreenToggle, new Rectangle(370, 120, fullscreenToggle.Width, fullscreenToggle.Height), Color.White);

            spriteBatch.Draw(soundText, new Rectangle(50, 290, soundText.Width, soundText.Height), Color.White);
            spriteBatch.Draw(soundToggle, new Rectangle(370, 270, soundToggle.Width, soundToggle.Height), Color.White);
            
        }
    }
}