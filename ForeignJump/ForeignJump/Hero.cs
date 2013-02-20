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
        bool jump;

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
        
        public Hero() { }

        public void Initialize(float x, float y)
        {
            Position = new Vector2(x, y);
            Vitesse = new Vector2(0, 0);
            Force = new Vector2(0, 0);
            Poids = new Vector2(0, 600);
        }

        public void LoadContent(ContentManager Content, string Static, string Anime, int rows, int columns)
        {
            TextureStatic = Content.Load<Texture2D>("hero");
            TextureAnime = Content.Load<Texture2D>("heroanime");
            HeroAnime = new Animate(textureAnime, 1, 16);
        }

        public void Jump(int x, int y) //fonction sauter
        {
            jumpState = "yes";
            force.X = x;
            force.Y = y;
        } 

        public void Update(GameTime gameTime, float speed, int joueur)
        {
            heroAnime.Update(speed); //Animation

            #region Physique

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Reaction du sol
            if (Position.Y >= 494)                    //si il est au sol
                Reaction = new Vector2(0, -(Poids.Y));
            else                                      //si il est en air (reaction = 0)
                Reaction = new Vector2(0, 0);

            //acceleration = somme des forces / masse (=1kg dans l'exemple)
            Vector2 acceleration = Poids + Reaction;


            jump = KB.New.IsKeyDown(Keys.Up);
            
            if (jump)
                Jump(0,-20000);

            if (jump && Position.Y < 500 && Position.Y > 493)
            {
                //si j'apuie sur up on applique la force
                acceleration += Force + Poids;
            }

            if (Position.Y < 0)
            {
                //si je touche le bord haut le mec s'annule
                Vitesse = new Vector2(0, 0);
                acceleration = Poids + Reaction;
            }

            //si il arrive a la position initiale il ne descend pas plus bas
            if (Position.Y >= 494)
            {
                jumpState = "no";
                Vitesse = new Vector2(0, 0);
                position.Y = 494;
            }

            Vitesse += acceleration * dt;
            Position += Vitesse * dt;

            #endregion
        }


        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
          if (jumpState == "yes")
              spriteBatch.Draw(textureStatic, Position, Color.White);
          else
              heroAnime.Draw(spriteBatch, Position);
        }
    }
}
