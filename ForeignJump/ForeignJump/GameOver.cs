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
    class GameOver
    {
        //Gameplay game;

        private Texture2D image; //image

        public GameOver()
        {
            image = Ressources.Content.Load<Texture2D>("gameOver");
        }

        public void Update()
        {
            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
            {
                GameState.State = "initial";
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Rectangle(440, 185, 400, 431), Color.White);
        }
    }
}