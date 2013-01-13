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
    class Hero
    {
        //Texture
        public Texture2D TextureStatic
        {
            get { return textureStatic; }
            set { textureStatic = value; }
        }
        private Texture2D textureStatic;

        //Position
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 position;
        
        //Vitesse
        public Vector2 Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }
        private Vector2 vitesse;

        #region Forces

        //Poids
        public Vector2 Poids
        {
            get { return poids; }
            set { poids = value; }
        }
        private Vector2 poids;

        //Force
        public Vector2 Force
        {
            get { return force; }
            set { force = value; }
        }
        private Vector2 force;

        //Reaction
        public Vector2 Reaction
        {
            get { return reaction; }
            set { reaction = value; }
        }
        private Vector2 reaction;

        //Saut?
        string jumpState = "no";

        #endregion

        #region Animation

        //Animation
        public Animate HeroAnime
        {
            get { return heroAnime; }
            set { heroAnime = value; }
        }
        private Animate heroAnime;

        //Texture
        public Texture2D TextureAnime
        {
            get { return textureAnime; }
            set { textureAnime = value; }
        }
        private Texture2D textureAnime;

        #endregion
        
        public Hero()
        {

        }

        public void Initialize(float x, float y)
        {
            position = new Vector2(x, y); 
            position = new Vector2(488, 494);
            vitesse = new Vector2(0, 0);
            force = new Vector2(0, 0);
            poids = new Vector2(0, 600);
        }

        public void LoadContent(ContentManager Content, string Static, string Anime, int rows, int columns)
        {
            textureStatic = Content.Load<Texture2D>("hero");
            textureAnime = Content.Load<Texture2D>("heroanime");
            heroAnime = new Animate(textureAnime, 1, 16);
        }

        public void Jump(int x, int y)
        {
            jumpState = "yes";
            force.X = x;
            force.Y = y;
        }

        public void Update(GameTime gameTime, float speed)
        {
            KeyboardState newState = Keyboard.GetState(); //Gestion clavier

            heroAnime.Update(speed); //Animation

            #region Physique

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Reaction du sol
            if (position.Y >= 494)                    //si il est au sol
                reaction = new Vector2(0, -(poids.Y));
            else                                      //si il est en air (reaction = 0)
                reaction = new Vector2(0, 0);

            //acceleration = somme des forces / masse (=1kg dans l'exemple)
            Vector2 acceleration = poids + reaction;

            bool jump = newState.IsKeyDown(Keys.Up) || newState.IsKeyDown(Keys.Space);

            if (jump)
                Jump(0,-20000);

            if (jump && position.Y < 500 && position.Y > 493)
            {
                //si j'apuie sur up on applique la force
                acceleration += force + poids;
            }

            if (position.Y < 0)
            {
                //si je touche le bord haut le mec s'annule
                vitesse = new Vector2(0, 0);
                acceleration = poids + reaction;
            }

            //si il arrive a la position initiale il ne descend pas plus bas
            if (position.Y >= 494)
            {
                jumpState = "no";
                vitesse = new Vector2(0, 0);
                position.Y = 494;
            }

            vitesse += acceleration * dt;
            position += vitesse * dt;

            #endregion
        }


        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
          if (jumpState == "yes")
              spriteBatch.Draw(textureStatic, position, Color.White);
          else
              heroAnime.Draw(spriteBatch, position);
        }
    }
}
