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

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera(Map map, Hero hero)
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
//                spriteBatch.DrawString(hero.font, Convert.ToString(objet.container.X + ";" + objet.container.Y), new Vector2(objet.position.X - position.X, objet.position.Y), Color.Green);
            }

 //           spriteBatch.DrawString(hero.font, "Rectangle: (" + Convert.ToString(hero.currentObjet.container.X) + " , " + Convert.ToString(hero.currentObjet.container.Y) + ")", new Vector2(20, 135), Color.Red);

            hero.Draw(spriteBatch, position);

   //         spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)(hero.currentObjet.container.X - position.X), hero.currentObjet.container.Y, hero.currentObjet.container.Width, hero.currentObjet.container.Height), Color.Green);

            //spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)hero.positionLocale.X, (int)hero.positionLocale.Y, hero.texture.Width, hero.texture.Height), Color.White);

            
            
        }
    }
}
