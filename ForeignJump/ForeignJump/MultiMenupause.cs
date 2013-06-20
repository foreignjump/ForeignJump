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
    class MultiMenuPause
    {
        #region animationButtons

        bool EntreeButtons;
        bool ButtonsIn;
        bool SortieButtons;
        bool ButtonsOut;

        #endregion

        #region Déclaration buttons

        private Button buttonBack;
        private Button buttonHelp;
        private Button buttonMenu;
        private Button buttonBackH;
        private Button buttonHelpH;
        private Button buttonMenuH;

        #endregion

        #region textures

        //buttons
        private Texture2D buttonTextureStart;
        private Texture2D buttonTextureStartH;
        private Texture2D buttonTextureStartI;

        private Texture2D buttonTextureHelp;
        private Texture2D buttonTextureHelpH;
        private Texture2D buttonTextureHelpI;

        private Texture2D buttonTextureMenu;
        private Texture2D buttonTextureMenuH;
        private Texture2D buttonTextureMenuI;

        #endregion

        #region positions

        private Vector2 positionStart;
        private Vector2 positionHelp;
        private Vector2 positionMenu;

        #endregion

        private int selection; //selection du button actuel

        public MultiMenuPause()
        { }

        public void Initialize(int x, int y)
        {
            positionStart = new Vector2(x, y);
            positionHelp = new Vector2(x, y);
            positionMenu = new Vector2(x, y);

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
            buttonTextureStartH = Ressources.GetLangue(Langue.Choisie).buttonTextureStartH;
            buttonTextureStartI = Ressources.GetLangue(Langue.Choisie).buttonTextureStartI;
            buttonTextureHelpH = Ressources.GetLangue(Langue.Choisie).buttonTextureHelpH;
            buttonTextureHelpI = Ressources.GetLangue(Langue.Choisie).buttonTextureHelpI;
            buttonTextureMenuH = Ressources.GetLangue(Langue.Choisie).buttonTextureMenuH;
            buttonTextureMenuI = Ressources.GetLangue(Langue.Choisie).buttonTextureMenuI;

            buttonTextureStart = buttonTextureStartI;
            buttonTextureHelp = buttonTextureHelpI;
            buttonTextureMenu = buttonTextureMenuI;

            buttonBack = new Button(buttonTextureStart, (int)positionStart.X, (int)positionStart.Y);
            buttonHelp = new Button(buttonTextureHelp, (int)positionHelp.X, (int)positionHelp.Y);
            buttonMenu = new Button(buttonTextureMenu, (int)positionMenu.X, (int)positionMenu.Y);
            buttonBackH = new Button(buttonTextureStartH, (int)positionStart.X, (int)positionStart.Y);
            buttonHelpH = new Button(buttonTextureHelpH, (int)positionHelp.X, (int)positionHelp.Y);
            buttonMenuH = new Button(buttonTextureMenuH, (int)positionMenu.X, (int)positionMenu.Y);
        }

        public void Update(GameTime gameTime, int vitesse)
        {

            #region Déclaration de bouttons
            buttonBack = new Button(buttonTextureStart, (int)positionStart.X, (int)positionStart.Y);
            buttonHelp = new Button(buttonTextureHelp, (int)positionHelp.X, (int)positionHelp.Y);
            buttonMenu = new Button(buttonTextureMenu, (int)positionMenu.X, (int)positionMenu.Y);
            buttonBackH = new Button(buttonTextureStartH, (int)positionStart.X, (int)positionStart.Y);
            buttonHelpH = new Button(buttonTextureHelpH, (int)positionHelp.X, (int)positionHelp.Y);
            buttonMenuH = new Button(buttonTextureMenuH, (int)positionMenu.X, (int)positionMenu.Y);
            #endregion

            #region Survoler le menu

            if (selection == -1) //pour que la selection ne dépasse pas les negatifs
                selection = 2;
            else
                selection = selection % 3; //pour que la selection ne dépasse pas 4

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
                buttonTextureHelp = buttonTextureHelpH;
            else
                buttonTextureHelp = buttonTextureHelpI;

            if (selection == 2)
                buttonTextureMenu = buttonTextureMenuH;
            else
                buttonTextureMenu = buttonTextureMenuI;

            #endregion

            #region Entrée sortie slide buttons

            if (EntreeButtons && !ButtonsIn) //entree
            {
                if (positionStart.Y <= 105)
                    positionStart.Y += vitesse;

                if (positionHelp.Y <= 234)
                    positionHelp.Y += vitesse + 6;

                if (positionMenu.Y <= 363)
                    positionMenu.Y += vitesse + 8;

                if (positionMenu.Y >= 363)
                    ButtonsIn = true;
            }

            if (SortieButtons && !ButtonsOut) //sortie
            {
                if (positionStart.Y >= -buttonTextureMenu.Height)
                    positionStart.Y -= vitesse;

                if (positionHelp.Y >= -buttonTextureMenu.Height)
                    positionHelp.Y -= vitesse + 6;

                if (positionMenu.Y >= -buttonTextureMenu.Height)
                    positionMenu.Y -= vitesse + 8;

                if (positionMenu.Y <= -buttonTextureMenu.Height)
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
                    GameState.State = "multiInGame";

                    //mise à 0 des variables
                    EntreeButtons = true;
                    ButtonsIn = false;
                    SortieButtons = false;
                    ButtonsOut = false;
                    selection = 0;
                }

                if (selection == 1) //si c'est sur aide
                {
                    GameState.State = "menuPauseAide";

                    //mise à 0 des variables
                    EntreeButtons = true;
                    ButtonsIn = false;
                    SortieButtons = false;
                    ButtonsOut = false;
                    selection = 0;
                }

                if (selection == 2) //si c'est sur Menu (back to menu dans ce cas)
                {
                    GameState.State = "initial";

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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            buttonBack.Draw(spriteBatch);
            buttonHelp.Draw(spriteBatch);
            buttonMenu.Draw(spriteBatch);
        }
    }
}