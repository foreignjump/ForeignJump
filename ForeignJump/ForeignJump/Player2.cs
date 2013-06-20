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
    class Player2
    {
        public Texture2D statique;
        public Texture2D animation;
        public Vector2 positionGlobale;
        public Animate animate;
        
        public Player2(Texture2D statique, Texture2D animation, Vector2 positionInitiale)
        {
            this.statique = statique;
            this.animation = animation;
            this.positionGlobale = positionInitiale;
            this.animate = new Animate(animation, 1, 12);
            
        }

        public void Update()
        {
            animate.Texture = animation;
            animate.Update(0.3f);
        }
    }
}
