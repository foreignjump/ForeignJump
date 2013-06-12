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
    public enum TypePerso { Ennemi, Hero };

    class Personnage
    {
        //textures
        public Texture2D texture;

        public Texture2D textureAnime;

        public Texture2D textureDown;

        public Animate personnageAnime;

        public bool animate;

        //physique
        public Vector2 positionGlobale;

        public Vector2 positionLocale;

        public Vector2 positionInitiale;

        public Vector2 lastPos;

        public Vector2 vitesse;

        public Vector2 vitesseInitiale;

        public Rectangle container;

        //forces
        public Vector2 poids;

        public Vector2 force;

        public Vector2 reaction; 
        
        //type
        public TypePerso type;
    }
}
