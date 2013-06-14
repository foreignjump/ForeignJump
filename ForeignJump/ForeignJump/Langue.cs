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
    public class Langue
    {
        static string choisie;

        public static string Choisie
        {
            get { return choisie; }
            set { choisie = value; }
        }

        //menu buttons
        public Texture2D buttonTextureStartH;
        public Texture2D buttonTextureStartI;

        public Texture2D buttonTextureOptionsH;
        public Texture2D buttonTextureOptionsI;

        public Texture2D buttonTextureHelpH;
        public Texture2D buttonTextureHelpI;

        public Texture2D buttonTextureExitH;
        public Texture2D buttonTextureExitI;

        //menu
        public Texture2D menuAide;
        public Texture2D menuChoose;

        //menuChoose
        public Texture2D gameOver;
        
        //menuOptions
        public Texture2D menuOptions;
        public Texture2D fullscreenH;
        public Texture2D fullscreenN;
        public Texture2D soundH;
        public Texture2D soundN;
        public Texture2D langueH;
        public Texture2D langueN;
        public Texture2D nomH;
        public Texture2D nomN;
        public Texture2D nomButton;

        //menuPause
        public Texture2D buttonTextureMenuH;
        public Texture2D buttonTextureMenuI;

        public Texture2D menuPauseAide;

        //keys bonus
        public Texture2D keysStart;
        public Texture2D keysOver;

        //pong bonus
        public Texture2D pongStart;
        public Texture2D pongOver;
    }
}
