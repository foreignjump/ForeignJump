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
    class Ennemi : Personnage
    {
        private Map map;
        private Hero hero;

        public SpriteFont font;

        private float distance;
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        private Objet currentObjet;

        private Particule particulefeu;

        public Ennemi(Animate textureAnime, Vector2 position, Hero hero, Map map)
        {
            this.personnageAnime = Ressources.GetPerso(Perso.Choisi).ennemiAnime;
            this.positionGlobale = position;
            this.positionInitiale = position;
            this.vitesse = hero.vitesse;
            this.vitesseInitiale = hero.vitesse;
            this.container = new Rectangle((int)position.X, (int)position.Y, (int)(textureAnime.Texture.Width / textureAnime.Columns), textureAnime.Texture.Height);
            this.type = TypePerso.Ennemi;
            this.poids = new Vector2(0, hero.poids.Y);
            this.force = Vector2.Zero;
            this.reaction = Vector2.Zero;
            this.map = map;
            this.distance = hero.positionGlobale.X - positionGlobale.X;
            this.currentObjet = new Objet();
            this.hero = hero;

            particulefeu = new Particule();
            particulefeu.LoadContent();

            font = Ressources.GetPerso(Perso.Choisi).font;
        }

        public void Update(GameTime gameTime, float speed)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            personnageAnime.Update(speed); //Animation

            container = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y, 45, 45);

            #region Test cases adjacentes
            currentObjet = new Objet();
            currentObjet.container.Width = 45;
            currentObjet.container.Height = 45;

            int posY = (int)(container.Y / 45);
            int posX = (int)(container.X / 45);

            int currentX = posX + 1, currentY = posY;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX + 1;
            currentY = posY + 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX;
            currentY = posY;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX + 1;
            currentY = posY - 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX - 1;
            currentY = posY;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX - 1;
            currentY = posY - 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX - 1;
            currentY = posY + 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX;
            currentY = posY + 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }

            currentX = posX;
            currentY = posY - 1;
            if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type != TypeCase.Null)
            {
                currentObjet.container.X = currentX * 45;
                currentObjet.container.Y = currentY * 45;
                testCollision(currentObjet);
            }
            
            #endregion

            Vector2 acceleration = poids + force; //somme des forces = masse * acceleration

            vitesse += acceleration * dt;
            positionGlobale += vitesse * dt;

            lastPos.X = container.X;
            lastPos.Y = container.Y;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 positionCam)
        {
            particulefeu.Draw();
            
            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)(positionGlobale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.AliceBlue);

            //spriteBatch.DrawString(font, Convert.ToString(bottom), new Vector2(30, 40), Color.White);
        }

        private void testCollision(Objet objet)
        {
            if (container.Intersects(objet.container))
            {
                //collision bas hero
                if (container.X + container.Width >= objet.container.X &&
                    lastPos.Y + container.Height <= objet.container.Y &&
                    container.Y + container.Height >= objet.container.Y)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y - container.Height;
                }

                //collision côté droit hero
                if (objet.type == TypeCase.Terre)
                {
                    if (container.X + container.Width >= objet.container.X &&
                        lastPos.Y + container.Height > objet.container.Y)
                    {
                        //MOTEUR A PARTICULES A METTRE ICI

                        map.Objets[(int)(objet.container.X / 45), (int)(objet.container.Y / 45)].texture = Ressources.GetPerso(Perso.Choisi).nulle;
                    }
                }
            }

            if (container.Intersects(hero.container))
            {
                GameState.State = "GameOver";
            }
        }
    }
}
