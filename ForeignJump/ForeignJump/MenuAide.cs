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
    public class MenuAide
    {
        KeyboardState oldState; //gestion clavier

        private Texture2D menubg; //image
        
        public MenuAide()
        { }

        public void Initialize()
        {
            oldState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("MenuAide");
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            var newState = Keyboard.GetState(); //mettre à jour le clavier

            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                GameState.State = "initial"; //retour au menu

            oldState = newState;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
        }
    }
}