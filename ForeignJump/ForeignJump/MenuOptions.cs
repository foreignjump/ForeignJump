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
    class MenuOptions
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

        #region langue
        private Texture2D langueToggleFR; //langue toggle FR
        private Texture2D langueToggleEN; //langue toggle EN
        private Texture2D langueToggle; //langue toggle

        private Texture2D langueTextN; //langue text not selected
        private Texture2D langueTextH; //langue text selected
        private Texture2D langueText; //langue text
        #endregion

        #region nom
        private Texture2D nomTextN; //nom text not selected
        private Texture2D nomTextH; //nom text selected
        private Texture2D nomText; //nom text
        private Texture2D nomButton; //nom button Change
        #endregion

        private int selectionFullscreen; //selection du Fullscreen
        private int selectionSound; //selection du Sound
        private int selectionLangue; //selection de la Langue

        private int selection; //selection verticale

        private Menu menu;
        private MenuAide menuaide;
        private MenuChoose menuchoose;

        public MenuOptions(Menu menu , MenuAide menuaide, MenuChoose menuchoose)
        {
            this.menu = menu;
            this.menuaide = menuaide;
            this.menuchoose = menuchoose;
        }

        public void Initialize()
        {
            //initialiser la selection à 0 sur fullscreen
            selection = 0;

            selectionFullscreen = 1; //initialiser la selection à 1 donc sur off
            if (Langue.Choisie == "fr")
                selectionLangue = 0; //initialiser à 0 donc sur FR
            else
                selectionLangue = 1;
        }

        public void LoadContent()
        {
            menubg = Ressources.GetLangue(Langue.Choisie).menuOptions;

            fullscreenTextH = Ressources.GetLangue(Langue.Choisie).fullscreenH;
            fullscreenTextN = Ressources.GetLangue(Langue.Choisie).fullscreenN;
            fullscreenText = fullscreenTextN;

            fullscreenToggleOff = Ressources.Content.Load<Texture2D>("Menu/Options/off");
            fullscreenToggleOn = Ressources.Content.Load<Texture2D>("Menu/Options/on");
            fullscreenToggle = fullscreenToggleOff;

            soundTextH = Ressources.GetLangue(Langue.Choisie).soundH;
            soundTextN = Ressources.GetLangue(Langue.Choisie).soundN;
            soundText = soundTextN;

            soundToggleOff = Ressources.Content.Load<Texture2D>("Menu/Options/off");
            soundToggleOn = Ressources.Content.Load<Texture2D>("Menu/Options/on");
            soundToggle = soundToggleOn;

            langueTextH = Ressources.GetLangue(Langue.Choisie).langueH;
            langueTextN = Ressources.GetLangue(Langue.Choisie).langueN;
            langueText = langueTextN;

            langueToggleFR = Ressources.Content.Load<Texture2D>("Menu/Options/fr");
            langueToggleEN = Ressources.Content.Load<Texture2D>("Menu/Options/en");
            if (Langue.Choisie == "fr")
                langueToggle = langueToggleFR;
            else
                langueToggle = langueToggleEN;

            nomTextH = Ressources.GetLangue(Langue.Choisie).nomH;
            nomTextN = Ressources.GetLangue(Langue.Choisie).nomN;
            nomText = nomTextN;
            nomButton = Ressources.GetLangue(Langue.Choisie).nomButton;
        }

        public void Update(GameTime gameTime, int vitesse, GraphicsDeviceManager graphics, MultiMenuChoose multimenuchoose)
        {
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
            {
                selection = 0;
                GameState.State = "initial"; //retour au menu
            }

            #region selection

            if (selection == -1) //pour que la selection ne dépasse pas les negatifs
                selection = 3;
            else
                selection = selection % 4; //pour que la selection ne dépasse pas 4

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

            if (selection == 2)
                langueText = langueTextH; //langue selectionné
            else
                langueText = langueTextN;

            if (selection == 3)
                nomText = nomTextH; //langue selectionné
            else
                nomText = nomTextN;

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
                    AudioRessources.volume = 1f;
                    soundToggle = soundToggleOn;
                }

                if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right) && selectionSound == 0)
                {
                    selectionSound = 1;
                    AudioRessources.volume = 0f;
                    soundToggle = soundToggleOff;
                }
            }
            #endregion

            #region langueToggle

            if (selection == 2) //is sound selected
            {
                if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left) && selectionLangue == 1)
                {
                    selectionLangue = 0;
                    Langue.Choisie = "fr";
                    LoadContent();
                    menu.LoadContent();
                    menuaide.LoadContent();
                    menuchoose.LoadContent();
                    multimenuchoose.LoadContent();
                    langueToggle = langueToggleFR;
                }

                if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right) && selectionLangue == 0)
                {
                    selectionLangue = 1;
                    Langue.Choisie = "en";
                    LoadContent();
                    menu.LoadContent();
                    menuaide.LoadContent();
                    menuchoose.LoadContent();
                    langueToggle = langueToggleEN;
                }
            }
            #endregion

            #region nomButton
            if (selection == 3) //is sound selected
            {
                if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
                {
                    GameState.State = "menuName";
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

            spriteBatch.Draw(langueText, new Rectangle(30, 440, langueText.Width, langueText.Height), Color.White);
            spriteBatch.Draw(langueToggle, new Rectangle(370, 420, langueToggle.Width, langueToggle.Height), Color.White);

            spriteBatch.Draw(nomText, new Rectangle(50, 560, nomText.Width, nomText.Height), Color.White);
            spriteBatch.Draw(nomButton, new Rectangle(370, 540, nomButton.Width, nomButton.Height), Color.White);
            spriteBatch.DrawString(Ressources.GetPerso("renoi").font, "Player name: " + Statistiques.Name, new Vector2(60, 650), Color.White);
        }
    }
}