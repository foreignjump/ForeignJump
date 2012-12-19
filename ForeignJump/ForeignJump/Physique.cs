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
using ForeignJump.Game1;

namespace ForeignJump
{
    class Physique
    {
        //positions
        Vector2 obstaclehPosition = new Vector2(750, 0);
        Vector2 obstaclebPosition = new Vector2(600, 287);
        Vector2 voiturePosition = new Vector2(0, 156);
        Vector2 bgPosition = new Vector2(30, 0);
        Vector2 heroPosition = new Vector2(488, 164);

        //forces
        Vector2 force = new Vector2(0, -20000);
        Vector2 poids = new Vector2(0, 600);
        Vector2 reaction;
        Vector2 vitesse = new Vector2(0, 0);
        Vector2 position = new Vector2(0, 0);

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //faire defiler la map
            bgPosition.X -= 3;
            obstaclehPosition.X -= 3;
            obstaclebPosition.X -= 3;

            //faire repeter la map
            if (bgPosition.X == -300)
                bgPosition.X = 0;

            //Reaction du sol
            if (heroPosition.Y >= 168)
                //si il est au sol
                reaction = new Vector2(0, -(poids.Y));
            else
                //si il est en air (reaction = 0)
                reaction = new Vector2(0, 0);

            //acceleration = somme des forces / masse (=1kg dans l'exemple)
            Vector2 acceleration = poids + reaction;

            // TODO: Add your update logic here
            if (Game1.newState.IsKeyDown(Keys.Up) && heroPosition.Y > 167)
            {
                //si j'apuie sur up on applique la force
                acceleration += force + poids;
            }

            if (heroPosition.Y < 0)
            {
                //si je touche le bord haut le mec s'annule
                vitesse = new Vector2(0, 0);
                acceleration = poids + reaction;
            }

            //si il arrive a la position initiale il ne descend pas plus bas
            if (heroPosition.Y >= 168)
            {
                vitesse = new Vector2(0, 0);
                heroPosition.Y = 168;
            }


            vitesse += acceleration * dt;
            heroPosition += vitesse * dt;

        }
    }
}
