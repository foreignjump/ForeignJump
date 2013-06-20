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

    class Camera
    {
        private Map map;
        private Hero hero;
        private Ennemi ennemi;

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera(Map map, Hero hero, Ennemi ennemi)
        {
            this.map = map;
            this.hero = hero;
            this.ennemi = ennemi;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).bg, new Rectangle(0, 0, 1280, 800), Color.White);

            foreach (Objet objet in map.Objets)
            {
                spriteBatch.Draw(objet.texture, new Rectangle((int)(objet.position.X - position.X), (int)(objet.position.Y), 45, 45), Color.White);
                if (objet.type == TypeCase.Piece)
                {
                    Animate piece = new Animate(Ressources.GetPerso(Perso.Choisi).piece, 8, 8);
                    piece.Update(0.3f);
                    piece.Draw(spriteBatch, new Vector2(objet.position.X - position.X, objet.position.Y), 3);
                }
            }

            hero.Draw(spriteBatch, position);
            ennemi.Draw(spriteBatch, position);
        }
    }
}