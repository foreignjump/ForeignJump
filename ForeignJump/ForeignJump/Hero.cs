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
    class Hero : Personnage
    {
        public SpriteFont font;

        private bool jumping;

        public Vector2 Position
        {
            get { return positionGlobale; }
            set { positionGlobale = value; }
        }
        private Vector2 lastPos;

        private Map map;

        private Objet currentObjet;

        //***moteur particule****
        private Particule particulefeu;

        public Hero(Animate textureAnime, Vector2 position, Vector2 vitesse, float poids, Map map)
        {
            this.texture = Ressources.GetPerso(Perso.Choisi).heroTexture;
            this.textureAnime = Ressources.GetPerso(Perso.Choisi).heroTextureAnime;
            this.heroAnime = Ressources.GetPerso(Perso.Choisi).heroAnime;
            this.positionGlobale = position;
            this.positionInitiale = position;
            this.vitesse = vitesse;
            this.vitesseInitiale = vitesse;
            this.container = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.type = TypePerso.Hero;
            this.poids = new Vector2(0, poids);
            this.force = Vector2.Zero;
            this.reaction = Vector2.Zero;
            this.map = map;
            this.currentObjet = new Objet();

            particulefeu = new Particule();
            particulefeu.LoadContent();
            jumping = false;
            font = Ressources.GetPerso(Perso.Choisi).font;

        }

        public void Jump()
        {
            force = new Vector2(0, -20000);
            jumping = true;
        }

        public void Update(GameTime gameTime, float speed)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            heroAnime.Update(speed); //Animation

            container = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y, 45, 45);

            #region Test cases adjacentes
            currentObjet = new Objet();
            currentObjet.container.Width = 45;
            currentObjet.container.Height = 45;

            int posY = (int)(container.Y / 45);
            int posX = (int)(container.X / 45);

            int currentX = posX + 1, currentY = posY;
            #region piece
            for (int i = 0; i < Map.piece.Count; i++)
            {
                if (container.Intersects(Map.piece[i]))
                {
                    map.Objets[Map.piece[i].X / 45 , Map.piece[i].Y /45 ].position = new Vector2(1, 1);
                }
            }
            #endregion

            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX + 1;
            currentY = posY + 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX;
            currentY = posY;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX + 1;
            currentY = posY - 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX - 1;
            currentY = posY;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX - 1;
            currentY = posY - 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX - 1;
            currentY = posY + 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX;
            currentY = posY + 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }

            currentX = posX;
            currentY = posY - 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);

                //                map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barre;
            }
            
            #endregion

            Vector2 acceleration = poids + force; //somme des forces = masse * acceleration

            vitesse += acceleration * dt;
            positionGlobale += vitesse * dt;

            #region kb
            if (KB.New.IsKeyDown(Keys.Right))
                positionGlobale.X += 5;

            if (KB.New.IsKeyDown(Keys.Left))
                positionGlobale.X -= 5;

            if (KB.New.IsKeyDown(Keys.Up))
                positionGlobale.Y -= 11;

            if (KB.New.IsKeyDown(Keys.Down))
               // positionGlobale.Y += 30;
            #endregion

            lastPos.X = container.X;
            lastPos.Y = container.Y;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 positionCam)
        {
            particulefeu.Draw();
            //if (!jumping)
            //  heroAnime.Draw(spriteBatch, new Vector2((positionGlobale.X + positionInitiale.X - positionCam.X), positionGlobale.Y), 3);
            //else
            //spriteBatch.Draw(texture, new Rectangle((int)(positionGlobale.X + positionInitiale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.White);

            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)(positionGlobale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.Red);

//            spriteBatch.DrawString(font, Convert.ToString(bottom), new Vector2(30, 40), Color.White);
        }

        private void testCollision(Objet objet)
        {
            if (container.Intersects(objet.container))
            {
                //collision côté droit hero
                if (container.X + container.Width >= objet.container.X &&
                    lastPos.Y + container.Height > objet.container.Y)
                {
//                    vitesse.X = 0;
                    positionGlobale.X = objet.container.X - container.Width - 1;
                }

                //collision bas hero
                if (container.X + container.Width >= objet.container.X &&
                    lastPos.Y + container.Height <= objet.container.Y &&
                    container.Y + container.Height >= objet.container.Y)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y - container.Height;
                }
                


                /*
               //ça rentre jamais?!
               if (container.X + container.Width >= objet.container.X &&
                  lastPos.Y >= objet.container.Y + objet.container.Height &&
                  container.Y <= objet.container.Y + objet.container.Width)
               {
                   vitesse.X = 0;
                   positionGlobale.X = objet.container.X - objet.container.Width;
               }

                */

                /*   //rentre jamais
                   if (container.X + container.Width <= objet.container.X &&
                      lastPos.Y >= objet.container.Y)
                   {
                       vitesse.X = 0;
                       positionGlobale.X = objet.container.X - container.Width;
                   }
                  */
            }

        }
    }
}
