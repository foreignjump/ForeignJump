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
        Texture2D texture;
        Rectangle rectangle;

        public Button(Texture2D texture, int posX, int posY)
        {
            this.texture = texture; //prend la texture du parametre
            rectangle = new Rectangle(posX, posY, texture.Width, texture.Height); //prend la position du button
        }

        public void Draw(SpriteBatch spriteBatch) //AFFICHAGE DU BOUTON
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
