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
using System.Threading;

namespace ForeignJump
{
    class MenuMode
    {
        private Texture2D menubg; //bg
        private Texture2D xbox; //necessite xbox live

        #region nom
        private Texture2D singleplayerN;
        private Texture2D singleplayerH;
        private Texture2D singleplayer;
        
        private Texture2D multiplayerN;
        private Texture2D multiplayerH;
        private Texture2D multiplayer;
        #endregion

        private int selection; //selection verticale

        public MenuMode()
        {        }

        public void Initialize()
        {
            //initialiser la selection à 0 sur fullscreen
            selection = 0;
        }

        public void LoadContent()
        {
            xbox = Ressources.Content.Load<Texture2D>("Menu/Mode/xboxlive");

            menubg = Ressources.Content.Load<Texture2D>("Menu/Mode/menuMode");

            singleplayerN = Ressources.Content.Load<Texture2D>("Menu/Mode/singleplayerN");
            singleplayerH = Ressources.Content.Load<Texture2D>("Menu/Mode/singleplayerH");
            singleplayer = singleplayerH;

            multiplayerN = Ressources.Content.Load<Texture2D>("Menu/Mode/multiplayerN");
            multiplayerH = Ressources.Content.Load<Texture2D>("Menu/Mode/multiplayerH");
            multiplayer = multiplayerN;
        }

        public void Update()
        {
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
            {
                selection = 0;
                GameState.State = "initial"; //retour au menu
            }

            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
            {
                if (selection == 0) //play
                {
                    selection = 0;
                    //utilisation d'un nouveau thread 
                    GameState.State = "menuChoose";
                    //initialiser la selection à 0 donc sur start
                }

                if (selection == 1) //play
                {
                    selection = 0;
                    //utilisation d'un nouveau thread 
                    GameState.State = "multiMenuChoose";
                    //initialiser la selection à 0 donc sur start
                }
            }

            #region selection

            if (selection == -1) //pour que la selection ne dépasse pas les negatifs
                selection = 1;
            else
                selection = selection % 2; //pour que la selection ne dépasse pas 4

            if (KB.New.IsKeyDown(Keys.Down) && !KB.Old.IsKeyDown(Keys.Down))
                selection++;

            if (KB.New.IsKeyDown(Keys.Up) && !KB.Old.IsKeyDown(Keys.Up))
                selection--;

            #endregion

            #region survoler le menu

            if (selection == 0)
                singleplayer = singleplayerH; //fullscreen selectionné
            else
                singleplayer = singleplayerN;

            if (selection == 1)
                multiplayer = multiplayerH; //sound selectionné
            else
                multiplayer = multiplayerN;
            #endregion
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);

            spriteBatch.Draw(xbox, new Rectangle(170 ,500 , xbox.Width, xbox.Height), Color.White);
            spriteBatch.Draw(singleplayer, new Rectangle(120, 200, singleplayer.Width, singleplayer.Height), Color.White);
            spriteBatch.Draw(multiplayer, new Rectangle(120, 415, multiplayer.Width, multiplayer.Height), Color.White);
        }
    }
}