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
    class Ennemi
    {
        //Texture
        private Texture2D texture;

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

        private Animate ennemiAnime;

        public Ennemi()
        {

        }

        public void Initialize(float x, float y)
        {
            position = new Vector2(x, y);
            vitesse = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager Content, string nom, int rows, int columns)
        {
            texture = Content.Load<Texture2D>(nom);
            ennemiAnime = new Animate(texture, rows, columns);
        }

        public void Update(GameTime gameTime, float vitesse)
        {
            ennemiAnime.Update(vitesse);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            ennemiAnime.Draw(spriteBatch, position);
        }
    }
}
