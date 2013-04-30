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
    public class Niveau
    {   
        //menuChoose
        public Texture2D persoMenu;
        public Texture2D nameMenu;
        public Texture2D drapeauMenu;
        public string description;

        //map
        public Objet[,] objets;
        public Texture2D obstacle;
        public Texture2D nuage;
        public Texture2D nulle;
        public Texture2D terre;
        public Texture2D terre1;
        public Texture2D terre2;
        public Texture2D piece;
        public Texture2D sousterre;
        public String path;
        
        //hero
        public Texture2D heroTexture;
        public Texture2D heroTextureAnime;
        public Animate heroAnime;

        //game
        public Texture2D bg;
        public Texture2D barre;
        public Texture2D glass;
        public SpriteFont font;

        //ennemi
        public Texture2D texture;
        public Animate ennemiAnime;

        
    }
}
