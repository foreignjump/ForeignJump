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

    class MultiCamera
    {
        private Map map;
        private Hero hero;

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public MultiCamera(Map map, Hero hero)
        {
            this.map = map;
            this.hero = hero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).bg, new Rectangle(0, 0, 1280, 800), Color.White);

            foreach (Objet objet in map.Objets)
            {
                spriteBatch.Draw(objet.texture, new Rectangle((int)(objet.position.X - position.X), (int)(objet.position.Y), 45, 45), Color.White);
            }

            hero.Draw(spriteBatch, position);

        }
    }
}
