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
    class MenuPauseAide
    {
        KeyboardState oldState; //gestion clavier

        private Texture2D menuPauseAidebg; //image
        
        public MenuPauseAide()
        { }

        public void Initialize()
        {
            oldState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager Content)
        {
            menuPauseAidebg = Content.Load<Texture2D>("MenuPauseAide");
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            var newState = Keyboard.GetState(); //mettre à jour le clavier

            if (newState.IsKeyDown(Keys.Escape) && !oldState.IsKeyDown(Keys.Escape))
                GameState.State = "menuPause"; //retour au menu

            oldState = newState;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menuPauseAidebg, new Rectangle(440, 185, 400, 431), Color.White);
        }
    }
}