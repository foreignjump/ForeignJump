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
    class Menu
    {
        //BG
        private Texture2D menubg;

        public Menu()
        {

        }

        public void Initialize(int milieu)
        {
        
        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("menubg");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState(); //Gestion clavier

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg,new Rectangle(0,0, 1280,800),Color.White);
        }
    }
}
