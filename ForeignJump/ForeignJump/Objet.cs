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
    public enum TypeCase {Terre, Sousterre, Eau, Null, Piece, Bonus, Obstacle};

    public class Objet
    {
        public Texture2D texture;

        public Vector2 position;

        public Rectangle container;
        
        public TypeCase type;
    }
}
