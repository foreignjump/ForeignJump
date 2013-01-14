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
    public class Menu
    {
        //BG
        private Texture2D menubg;
        private Texture2D buttonTextureStart;
        private Texture2D buttonTextureStartH;
        private Texture2D buttonTextureOptions;
        private Texture2D buttonTextureOptionsH;
        private Texture2D buttonTextureHelp;
        private Texture2D buttonTextureHelpH;
        private Texture2D buttonTextureExit;
        private Texture2D buttonTextureExitH;


        #region Button positions

        public Vector2 PositionStart
        {
            get { return positionStart; }
            set { positionStart = value; }
        }
        private Vector2 positionStart;

        public Vector2 PositionOptions
        {
            get { return positionOptions; }
            set { positionOptions = value; }
        }
        private Vector2 positionOptions;

        public Vector2 PositionHelp
        {
            get { return positionHelp; }
            set { positionHelp = value; }
        }
        private Vector2 positionHelp;

        public Vector2 PositionExit
        {
            get { return positionExit; }
            set { positionExit = value; }
        }
        private Vector2 positionExit;

        #endregion

        #region Buttons

        private Button buttonStart;
        private Button buttonOptions;
        private Button buttonHelp;
        private Button buttonExit;
        private Button buttonStartH;
        private Button buttonOptionsH;
        private Button buttonHelpH;
        private Button buttonExitH;

        private Button buttonPauseStart;
        private Button buttonPauseOptions;
        private Button buttonPauseHelp;
        private Button buttonPauseExit;
        private Button buttonPauseStartH;
        private Button buttonPauseOptionsH;
        private Button buttonPauseHelpH;
        private Button buttonPauseExitH;

        #endregion

        #region Parcours menu

        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }
        private int selection;

        public int Appuye
        {
            get { return appuye; }
            set { appuye = value; }
        }
        private int appuye;

        public int Relache
        {
            get { return relache; }
            set { relache = value; }
        }
        private int relache;

        #endregion

        public Menu()
        {
        }

        public void Initialize(int milieu)
        {
            positionStart = new Vector2(milieu, -104);
            positionOptions = new Vector2(milieu, -104);
            positionHelp = new Vector2(milieu, -104);
            positionExit = new Vector2(milieu, -104);

            selection = 1;
            appuye = 1;
            relache = 1;
        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("menubg");
            buttonTextureStart = Content.Load<Texture2D>("ButtonStart");
            buttonTextureStartH = Content.Load<Texture2D>("ButtonStartH");
            buttonTextureOptions = Content.Load<Texture2D>("ButtonOptions");
            buttonTextureOptionsH = Content.Load<Texture2D>("ButtonOptionsH");
            buttonTextureHelp = Content.Load<Texture2D>("ButtonHelp");
            buttonTextureHelpH = Content.Load<Texture2D>("ButtonHelpH");
            buttonTextureExit = Content.Load<Texture2D>("ButtonExit");
            buttonTextureExitH = Content.Load<Texture2D>("ButtonExitH");
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            KeyboardState newState = Keyboard.GetState(); //Gestion clavier

            buttonStart = new Button(buttonTextureStart, new Rectangle((int)positionStart.X, (int)positionStart.Y, buttonTextureStart.Width, buttonTextureStart.Height));
            buttonOptions = new Button(buttonTextureOptions, new Rectangle((int)positionOptions.X, (int)positionOptions.Y, buttonTextureOptions.Width, buttonTextureOptions.Height));
            buttonHelp = new Button(buttonTextureHelp, new Rectangle((int)positionHelp.X, (int)positionHelp.Y, buttonTextureHelp.Width, buttonTextureHelp.Height));
            buttonExit = new Button(buttonTextureExit, new Rectangle((int)positionExit.X, (int)positionExit.Y, buttonTextureExit.Width, buttonTextureExit.Height));
            buttonStartH = new Button(buttonTextureStartH, new Rectangle((int)positionStart.X, (int)positionStart.Y, buttonTextureStartH.Width, buttonTextureStartH.Height));
            buttonOptionsH = new Button(buttonTextureOptionsH, new Rectangle((int)positionOptions.X, (int)positionOptions.Y, buttonTextureOptionsH.Width, buttonTextureOptionsH.Height));
            buttonHelpH = new Button(buttonTextureHelpH, new Rectangle((int)positionHelp.X, (int)positionHelp.Y, buttonTextureHelpH.Width, buttonTextureHelpH.Height));
            buttonExitH = new Button(buttonTextureExitH, new Rectangle((int)positionExit.X, (int)positionExit.Y, buttonTextureExitH.Width, buttonTextureExitH.Height));

            buttonPauseStart = new Button(buttonTextureStart, new Rectangle((int)positionStart.X, (int)positionStart.Y, buttonTextureStart.Width, buttonTextureStart.Height));
            buttonPauseOptions = new Button(buttonTextureOptions, new Rectangle((int)positionOptions.X, (int)positionOptions.Y, buttonTextureOptions.Width, buttonTextureOptions.Height));
            buttonPauseHelp = new Button(buttonTextureHelp, new Rectangle((int)positionHelp.X, (int)positionHelp.Y, buttonTextureHelp.Width, buttonTextureHelp.Height));
            buttonPauseExit = new Button(buttonTextureExit, new Rectangle((int)positionExit.X, (int)positionExit.Y, buttonTextureExit.Width, buttonTextureExit.Height));
            buttonPauseStartH = new Button(buttonTextureStartH, new Rectangle((int)positionStart.X, (int)positionStart.Y, buttonTextureStartH.Width, buttonTextureStartH.Height));
            buttonPauseOptionsH = new Button(buttonTextureOptionsH, new Rectangle((int)positionOptions.X, (int)positionOptions.Y, buttonTextureOptionsH.Width, buttonTextureOptionsH.Height));
            buttonPauseHelpH = new Button(buttonTextureHelpH, new Rectangle((int)positionHelp.X, (int)positionHelp.Y, buttonTextureHelpH.Width, buttonTextureHelpH.Height));
            buttonPauseExitH = new Button(buttonTextureExitH, new Rectangle((int)positionExit.X, (int)positionExit.Y, buttonTextureExitH.Width, buttonTextureExitH.Height));

            #region Selection

            if (newState.IsKeyDown(Keys.Up) && appuye == 0)
            {

                if (selection == 1)
                {
                    selection = 4;
                }
                else
                {
                    selection = selection - 1;
                }
                appuye = 1;

            }
            else if (newState.IsKeyDown(Keys.Down) && relache == 0)
            {

                if (selection == 4)
                {
                    selection = 1;
                }
                else
                {
                    selection = selection + 1;
                }
                relache = 1;
            }

            if (newState.IsKeyUp(Keys.Up))
            {
                appuye = 0;
            }
            if (newState.IsKeyUp(Keys.Down))
            {
                relache = 0;
            }

            #endregion

            if (GameState.State == "pause")
            {
                if (newState.IsKeyDown(Keys.Enter) && selection == 1)
                    GameState.State = "load1";
            }
            else if (GameState.State == "initial")
            {
                if (newState.IsKeyDown(Keys.Enter) && selection == 1)
                    GameState.State = "load";
            }

            if (positionExit.Y <= -104 && GameState.State == "load1")
            {
                GameState.State = "inGame";
            }

            if (newState.IsKeyDown(Keys.Enter) && selection == 4 && GameState.State == "pause")
            {
                GameState.State = "initial";
                selection = 2;
            }
            else if (newState.IsKeyDown(Keys.Enter) && selection == 4 && GameState.State == "initial")
            {
                System.Environment.Exit(0);
            }

            if (positionExit.Y <= -104  && GameState.State == "load")
            {
                GameState.State = "inGame";
            }

            if (newState.IsKeyDown(Keys.Escape))
            {
                GameState.State = "pause";
            }

            if (GameState.State == "load" || GameState.State == "load1")
            {
                if (positionStart.Y > -104)
                {
                    positionStart.Y -= vitesse + 2;
                }

                if (positionOptions.Y > -104)
                {
                    positionOptions.Y -= vitesse + 1;
                }

                if (positionHelp.Y > -104)
                {
                    positionHelp.Y -= vitesse;
                }

                if (positionExit.Y > -104)
                {
                    positionExit.Y -= vitesse - 1;
                }
            }
            else if (GameState.State == "pause" || GameState.State != "inGame")
            {

                if (positionStart.Y <= 105)
                {
                    positionStart.Y += vitesse;
                }
                if (positionOptions.Y <= 234)
                {
                    positionOptions.Y += vitesse +1;
                }
                if (positionHelp.Y <= 363)
                {
                    positionHelp.Y += vitesse +2;
                }
                if (positionExit.Y <= 492)
                {
                    positionExit.Y += vitesse +3;
                }

            }

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool background)
        {
            if (GameState.State == "initial" || GameState.State == "load")
            {
                spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
            }
            if (GameState.State == "pause")
            {
                if (selection == 1)
                {
                    buttonPauseStartH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonPauseStart.DrawButton(spriteBatch);
                }
                if (selection == 2)
                {
                    buttonPauseOptionsH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonPauseOptions.DrawButton(spriteBatch);
                }
                if (selection == 3)
                {
                    buttonPauseHelpH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonPauseHelp.DrawButton(spriteBatch);
                }
                if (selection == 4)
                {
                    buttonPauseExitH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonPauseExit.DrawButton(spriteBatch);
                }
            }
            else
            {
                if (selection == 1)
                {
                    buttonStartH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonStart.DrawButton(spriteBatch);
                }
                if (selection == 2)
                {
                    buttonOptionsH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonOptions.DrawButton(spriteBatch);
                }
                if (selection == 3)
                {
                    buttonHelpH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonHelp.DrawButton(spriteBatch);
                }
                if (selection == 4)
                {
                    buttonExitH.DrawButton(spriteBatch);
                }
                else
                {
                    buttonExit.DrawButton(spriteBatch);
                }
            }
        }
    }
}