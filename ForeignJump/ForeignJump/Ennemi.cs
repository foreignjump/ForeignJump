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
    class Ennemi : Object
    {
        private Animate ennemiAnime;

        public Ennemi(int x, int y)
        {
            positionInitiale = new Vector2(x, y);
        }

        public void Initialize()
        {
            position = positionInitiale;
            vitesse = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Ressources.GetPerso(Perso.Choisi).texture;
            ennemiAnime = Ressources.GetPerso(Perso.Choisi).ennemiAnime;
        }

        public void Update(float vitesse)
        {
            ennemiAnime.Update(vitesse);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            ennemiAnime.Draw(spriteBatch, position, 1);
        }
    }
}
