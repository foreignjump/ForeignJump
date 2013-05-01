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

        //private bool jumping;

        //map actuelle
        private Map map;

        //objet en collision
        private Objet currentObjet;

        //ennemi
        public Ennemi ennemi;
        
        //moteur à particules
        private Particule particulefeu;

        //nombre de pièces
        int pieces;

        bool bonusVitesse;
        
        //temps de vitesse augumenté
        int t0, t1;

        public Hero(Animate textureAnime, Vector2 position, Vector2 vitesse, float poids, Map map)
        {
            this.texture = Ressources.GetPerso(Perso.Choisi).heroTexture;
            this.textureAnime = Ressources.GetPerso(Perso.Choisi).heroTextureAnime;
            this.personnageAnime = Ressources.GetPerso(Perso.Choisi).heroAnime;
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
            //jumping = false;
            pieces = 0;
            bonusVitesse = false;
            font = Ressources.GetPerso(Perso.Choisi).font;

        }

        public void Jump()
        {
            force = new Vector2(0, -20000);
            //jumping = true;
        }

        public void Update(GameTime gameTime, float speed)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            personnageAnime.Update(speed); //Animation

            container = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y, 45, 45);

            force.Y = 600;

            #region Test collision bonusGame
            for (int i = 0; i < Map.ListBonusGame.Count; i++)
            {
                if (container.Intersects(Map.ListBonusGame[i]))
                {
                    map.Objets[Map.ListBonusGame[i].X / 45, Map.ListBonusGame[i].Y / 45] = map.Objets[1, 1];
                    Map.ListBonusGame[i] = new Rectangle(0, 0, 45, 45);
                    AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                    GameState.State = "newGame";
                    //MOTEUR A PARTICULES A METTRE ICI
                }
            }
            #endregion

            #region Test collision bonusVitesse

            for (int i = 0; i < Map.ListBonusSpeed.Count; i++)
            {
                if (container.Intersects(Map.ListBonusSpeed[i]))
                {
                    map.Objets[Map.ListBonusSpeed[i].X / 45, Map.ListBonusSpeed[i].Y / 45] = map.Objets[1, 1];
                    Map.ListBonusSpeed[i] = new Rectangle(0, 0, 45, 45);
                    bonusVitesse = true;
                    t0 = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds);
                    //MOTEUR A PARTICULES A METTRE ICI
                }
            }
            #endregion

            #region Bonus Vitesse
            if (bonusVitesse)
            {
                t1 = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds);
                AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);

                positionGlobale.X += 14;
                ennemi.positionGlobale.X += 14;

                if (t1 - t0 > 10)
                    bonusVitesse = false;
            }
            #endregion

            #region Test collision pièce
            for (int i = 0; i < Map.ListPiece.Count; i++)
            {
                if (container.Intersects(Map.ListPiece[i]))
                {
                    map.Objets[Map.ListPiece[i].X / 45, Map.ListPiece[i].Y / 45] = map.Objets[1, 1];
                    Map.ListPiece[i] = new Rectangle(0, 0, 45, 45);
                    AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                    //score mis à jour
                    Statistiques.Score++;
                    //MOTEUR A PARTICULES A METTRE ICI
                }
            }
            #endregion

            #region Test cases adjacentes
            currentObjet = new Objet();
            currentObjet.container.Width = 45;
            currentObjet.container.Height = 45;

            int posY = (int)(container.Y / 45);
            int posX = (int)(container.X / 45);

            int currentX = posX + 1, currentY = posY;
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


            #region kb
            if (KB.New.IsKeyDown(Keys.Right))
                positionGlobale.X += 5;

            if (KB.New.IsKeyDown(Keys.Left))
                positionGlobale.X -= 5;

            if (KB.New.IsKeyDown(Keys.Up) && vitesse.Y == 0)
                force.Y -= 45000;

            if (KB.New.IsKeyDown(Keys.Down) && vitesse.Y != 0)
                force.Y += 8500;
            #endregion

            Vector2 acceleration = poids + force; //somme des forces = masse * acceleration

            vitesse += acceleration * dt;
            positionGlobale += vitesse * dt;

            //mise à jour de la position d'avant
            lastPos.X = container.X;
            lastPos.Y = container.Y;

            //si il tombe dans le vide
            if (positionGlobale.Y >= 800)
                GameState.State = "GameOver";
        
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 positionCam)
        {
            particulefeu.Draw();
            //if (!jumping)
            //  heroAnime.Draw(spriteBatch, new Vector2((positionGlobale.X + positionInitiale.X - positionCam.X), positionGlobale.Y), 3);
            //else
            //spriteBatch.Draw(texture, new Rectangle((int)(positionGlobale.X + positionInitiale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.White);

            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)(positionGlobale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.Red);
            
            if (bonusVitesse)
                spriteBatch.DrawString(font, "Super-Man", new Vector2(200, 200), Color.White);
        }

        private void testCollision(Objet objet)
        {
            if (container.Intersects(objet.container) && container.X + container.Width <= objet.container.X + objet.container.Width)
            {
                /*
                //collision top hero
                if (lastPos.Y > objet.container.Y + objet.container.Height &&
                    container.X + container.Width >= objet.container.X &&
                    container.Y <= objet.container.Y + objet.container.Height)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y + objet.container.Height;
                }*/

                //collision côté droit hero
                if (lastPos.Y + container.Height > objet.container.Y &&
                    container.X + container.Width >= objet.container.X)
                {
                    bonusVitesse = false;
                    positionGlobale.X = objet.container.X - container.Width - 1;
                }

                //collision bas hero
                if (lastPos.Y + container.Height <= objet.container.Y &&
                    (container.X >= objet.container.X ||
                    container.X <= objet.container.X + objet.container.Width) &&
                    container.Y + container.Height >= objet.container.Y)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y - container.Height;
                }
            }

        }
    }
}
