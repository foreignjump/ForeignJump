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
    public class MenuPause
    {
        Gameplay game;

        #region animationButtons

        bool EntreeButtons;
        bool ButtonsIn;
        bool SortieButtons;
        bool ButtonsOut;

        #endregion

        #region Déclaration buttons

        private Button buttonStart;
        private Button buttonOptions;
        private Button buttonHelp;
        private Button buttonExit;
        private Button buttonStartH;
        private Button buttonOptionsH;
        private Button buttonHelpH;
        private Button buttonExitH;

        #endregion

        #region textures
        //BG        
        private Texture2D menubg;
        //buttons
        private Texture2D buttonTextureStart;
        private Texture2D buttonTextureStartH;
        private Texture2D buttonTextureStartI;

        private Texture2D buttonTextureOptions;
        private Texture2D buttonTextureOptionsH;
        private Texture2D buttonTextureOptionsI;

        private Texture2D buttonTextureHelp;
        private Texture2D buttonTextureHelpH;
        private Texture2D buttonTextureHelpI;

        private Texture2D buttonTextureExit;
        private Texture2D buttonTextureExitH;
        private Texture2D buttonTextureExitI;

        #endregion

        #region positions

        private Vector2 positionStart;
        private Vector2 positionOptions;
        private Vector2 positionHelp;
        private Vector2 positionExit;

        #endregion

        private int selection; //selection du button actuel

        public MenuPause(Gameplay game)
        {
            this.game = game;
        }

        public void Initialize(int x, int y)
        {
            positionStart = new Vector2(x, y);
            positionOptions = new Vector2(x, y);
            positionHelp = new Vector2(x, y);
            positionExit = new Vector2(x, y);

            //initialiser la selection à 0 donc sur start
            selection = 0;

            //entrée des buttons
            EntreeButtons = true;
            ButtonsIn = false;
            SortieButtons = false;
            ButtonsOut = false;
        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("Menu/menubg");

            buttonTextureStartH = Content.Load<Texture2D>("Menu/ButtonStartH");
            buttonTextureStartI = Content.Load<Texture2D>("Menu/ButtonStart");
            buttonTextureOptionsH = Content.Load<Texture2D>("Menu/ButtonOptionsH");
            buttonTextureOptionsI = Content.Load<Texture2D>("Menu/ButtonOptions");
            buttonTextureHelpH = Content.Load<Texture2D>("Menu/ButtonHelpH");
            buttonTextureHelpI = Content.Load<Texture2D>("Menu/ButtonHelp");
            buttonTextureExitH = Content.Load<Texture2D>("Menu/ButtonExitH");
            buttonTextureExitI = Content.Load<Texture2D>("Menu/ButtonExit");

            buttonTextureStart = buttonTextureStartI;
            buttonTextureOptions = buttonTextureOptionsI;
            buttonTextureHelp = buttonTextureHelpI;
            buttonTextureExit = buttonTextureExitI;
        }

        public void Update(GameTime gameTime, int vitesse)
        {

            #region Déclaration de bouttons
            buttonStart = new Button(buttonTextureStart, (int)positionStart.X, (int)positionStart.Y);
            buttonOptions = new Button(buttonTextureOptions, (int)positionOptions.X, (int)positionOptions.Y);
            buttonHelp = new Button(buttonTextureHelp, (int)positionHelp.X, (int)positionHelp.Y);
            buttonExit = new Button(buttonTextureExit, (int)positionExit.X, (int)positionExit.Y);
            buttonStartH = new Button(buttonTextureStartH, (int)positionStart.X, (int)positionStart.Y);
            buttonOptionsH = new Button(buttonTextureOptionsH, (int)positionOptions.X, (int)positionOptions.Y);
            buttonHelpH = new Button(buttonTextureHelpH, (int)positionHelp.X, (int)positionHelp.Y);
            buttonExitH = new Button(buttonTextureExitH, (int)positionExit.X, (int)positionExit.Y);
            #endregion

            #region Survoler le menu

            if (selection == -1) //pour que la selection ne dépasse pas les negatifs
                selection = 3;
            else
                selection = selection % 4; //pour que la selection ne dépasse pas 4

            if (KB.New.IsKeyDown(Keys.Down) && !KB.Old.IsKeyDown(Keys.Down))
                selection++;

            if (KB.New.IsKeyDown(Keys.Up) && !KB.Old.IsKeyDown(Keys.Up))
                selection--;

            #endregion

            #region Changer la texture du bouton survolé

            if (selection == 0)
                buttonTextureStart = buttonTextureStartH;
            else
                buttonTextureStart = buttonTextureStartI;

            if (selection == 1)
                buttonTextureOptions = buttonTextureOptionsH;
            else
                buttonTextureOptions = buttonTextureOptionsI;

            if (selection == 2)
                buttonTextureHelp = buttonTextureHelpH;
            else
                buttonTextureHelp = buttonTextureHelpI;

            if (selection == 3)
                buttonTextureExit = buttonTextureExitH;
            else
                buttonTextureExit = buttonTextureExitI;

            #endregion

            #region Entrée sortie slide buttons

            if (EntreeButtons && !ButtonsIn) //entree
            {
                if (positionStart.Y <= 105)
                    positionStart.Y += vitesse;

                if (positionOptions.Y <= 234)
                    positionOptions.Y += vitesse + 6;

                if (positionHelp.Y <= 363)
                    positionHelp.Y += vitesse + 8;

                if (positionExit.Y <= 492)
                    positionExit.Y += vitesse + 12;

                if (positionExit.Y >= 492)
                    ButtonsIn = true;
            }

            if (SortieButtons && !ButtonsOut) //sortie
            {
                if (positionStart.Y >= -buttonTextureExit.Height)
                    positionStart.Y -= vitesse;

                if (positionOptions.Y >= -buttonTextureExit.Height)
                    positionOptions.Y -= vitesse + 6;

                if (positionHelp.Y >= -buttonTextureExit.Height)
                    positionHelp.Y -= vitesse + 8;

                if (positionExit.Y >= -buttonTextureExit.Height)
                    positionExit.Y -= vitesse + 12;

                if (positionExit.Y <= -buttonTextureExit.Height)
                    ButtonsOut = true;
            }
            #endregion

            #region Entrée

            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter)) //confirmation
            {
                EntreeButtons = false;
                SortieButtons = true;
            }

            if (ButtonsOut) //si les buttons sont sortis
            {
                if (selection == 0) //si c'est sur start
                {
                    GameState.State = "inGame";

                    //mise à 0 des variables
                    EntreeButtons = true;
                    ButtonsIn = false;
                    SortieButtons = false;
                    ButtonsOut = false;
                    selection = 0;
                }

                if (selection == 2) //si c'est sur aide
                {
                    GameState.State = "menuPauseAide";

                    //mise à 0 des variables
                    EntreeButtons = true;
                    ButtonsIn = false;
                    SortieButtons = false;
                    ButtonsOut = false;
                    selection = 0;
                }

                if (selection == 3) //si c'est sur exit (back to menu dans ce cas)
                {
                    GameState.State = "initial";
                    game.NewGame();
                    //mise à 0 des variables
                    EntreeButtons = true;
                    ButtonsIn = false;
                    SortieButtons = false;
                    ButtonsOut = false;
                    selection = 0;
                }

            }

            #endregion
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            buttonStart.Draw(spriteBatch);
            buttonOptions.Draw(spriteBatch);
            buttonHelp.Draw(spriteBatch);
            buttonExit.Draw(spriteBatch);
        }
    }
}