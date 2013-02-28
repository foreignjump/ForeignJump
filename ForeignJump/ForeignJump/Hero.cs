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
    class Hero : Object
    {
        bool jump;

        public Vector2 posSol;

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

        //reaction
        private Vector2 reaction;

        //Saut?
        bool jumping;

        #endregion

        #region Animation
        //Animation
        public Animate heroAnime;

        //Texture
        private Texture2D textureAnime;
        #endregion

        public Hero(int x, int y)
        {
            positionInitiale.X = x;
            positionInitiale.Y = y;

            posSol.X = x;
            posSol.Y = y;
        }

        public void Initialize()
        {
            position = new Vector2(positionInitiale.X, positionInitiale.Y);
            vitesse = new Vector2(0, 0);
            Force = new Vector2(0, 0);
            Poids = new Vector2(0, 600);
            jumping = false;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Ressources.GetPerso(Perso.Choisi).heroTexture;
            textureAnime = Ressources.GetPerso(Perso.Choisi).heroTextureAnime;
            heroAnime = Ressources.GetPerso(Perso.Choisi).heroAnime;
        }

        public void Jump(int x, int y) //fonction sauter
        {
            jumping = true;
            force.X = x;
            force.Y = y;
        }

        public void Update(GameTime gameTime, float speed)
        {
            heroAnime.Update(speed); //Animation

            container = new Rectangle(Convert.ToInt32(position.X + posSol.X), Convert.ToInt32(position.Y), 60, texture.Height);

            #region Jump

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //reaction du sol
            if (position.Y >= posSol.Y)                    //si il est au sol
                reaction = new Vector2(0, -(Poids.Y));
            else                                               //si il est en air (reaction = 0)
                reaction = new Vector2(0, 0);

            //acceleration = somme des forces / masse (=1kg dans l'exemple)
            Vector2 acceleration = Poids + reaction;

            jump = KB.New.IsKeyDown(Keys.Up);

            if (jump)
                Jump(1000, -22000);

            if (jump && position.Y < posSol.Y + 5 && position.Y > posSol.Y - 5)
            {
                //si j'apuie sur up on applique la force
                acceleration += Force + Poids;
            }

            if (position.Y < 0)
            {
                //si je touche le bord haut le mec s'annule
                vitesse = new Vector2(0, 0);
                acceleration = Poids + reaction;
            }

            //si il arrive a la position initiale il ne descend pas plus bas
            if (position.Y >= posSol.Y)
            {
                jumping = false;
                vitesse = new Vector2(0, 0);
                position.Y = posSol.Y;
            }

            vitesse += acceleration * dt;
            position += vitesse * dt;

            #endregion
        }


        public virtual void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            if (jumping)
                spriteBatch.Draw(texture, new Vector2((int)((position.X + 550 - cameraPosition.X)), (int)(position.Y - cameraPosition.Y)), Color.White);
            else
                heroAnime.Draw(spriteBatch, new Vector2((int)((position.X + 550 - cameraPosition.X)), (int)(position.Y - cameraPosition.Y)), 3);
        }
    }
}
