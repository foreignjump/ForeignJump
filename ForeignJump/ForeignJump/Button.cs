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
    class Button
    {
        Rectangle countainer;
        Texture2D texture;

        public Button(Texture2D texture, Rectangle countainer)
        {
            this.countainer = countainer;
            this.texture = texture;
        }

        public Rectangle getcountainer() // RENVOI LA POSITION DU BOUTON
        {

            countainer = new Rectangle((int)countainer.X, (int)countainer.Y, ((int)countainer.Width), ((int)countainer.Height));
            return countainer;
        }

        public void DrawButton(SpriteBatch spriteBatch)//AFFICHAGE DU BOUTON
        {
            spriteBatch.Draw(texture, countainer, Color.White);
        }
    }
}
