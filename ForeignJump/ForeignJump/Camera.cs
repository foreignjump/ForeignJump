using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForeignJump
{
    class Camera
    {
        public Map map;
        public Hero hero;
        public Ennemi ennemi;
        public Vector2 position;
        private Animate heroAnime;

        public Camera(Map map, Hero hero, Ennemi ennemi, Vector2 position, Animate heroAnime)
        {
            this.map = map;
            this.hero = hero;
            this.ennemi = ennemi;
            this.position = position;
            this.heroAnime = heroAnime;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //affichage des obstacles
            foreach (Object objet in Ressources.GetPerso(Perso.Choisi).objets)
            {
                spriteBatch.Draw(objet.texture, new Rectangle((int)(objet.position.X - position.X), (int)(objet.position.Y - position.Y), 45, 45/*objet.tex.Width, objet.tex.Height*/), Color.White);
            }

            //affichage de l'hero
            hero.Draw(spriteBatch, position);

            //affichage de l'enemi
            ennemi.Draw(spriteBatch);

        }
    }
}
