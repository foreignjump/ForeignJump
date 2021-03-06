﻿using System;
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
        private Texture2D menubg; //image
        
        public MenuAide()
        { }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            menubg = Ressources.GetLangue(Langue.Choisie).menuAide;
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
                GameState.State = "initial"; //retour au menu
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
        }
    }
}