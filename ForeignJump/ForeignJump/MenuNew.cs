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
    public class MenuNew
    {
        //BG
        private Texture2D menubg;


        public MenuNew()
        {
        }

        public void Initialize(int milieu)
        {
            
        }

        public void LoadContent(ContentManager Content)
        {
            
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            KeyboardState newState = Keyboard.GetState(); //Gestion clavier
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool background)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
        }
    }
}