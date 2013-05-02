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
using X2DPE;
using X2DPE.Helpers;

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

        //random pour bonus
        private Random random;

        public bool bonusVitesse;
        private Texture2D superman;

        //temps de vitesse augumenté
        int t0, t1;

        double tfloat1, tfloat2;
        //particules bombe******
        ParticleComponent particleComponent;
        bool explosion;
        Emitter bombe = new Emitter();
        public static Emitter smokeEmitter = new Emitter();

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
            
            currentObjet = new Objet();
            
            //jumping = false;

            random = new Random();
            bonusVitesse = false;
            superman = Ressources.Content.Load<Texture2D>("superman");

            font = Ressources.GetPerso(Perso.Choisi).font;

            //moteur à particule pour la bombe*************
            #region Moteur a particules

            Emitter.statut = true;
            particleComponent = new ParticleComponent(Ressources.Game);
            Ressources.Game.Components.Add(particleComponent);
            bombe.Active = false;
            bombe.TextureList.Add(Ressources.Content.Load<Texture2D>("fire"));
            bombe.RandomEmissionInterval = new RandomMinMax(2.0d);
            bombe.ParticleLifeTime = 2000;
            bombe.ParticleDirection = new RandomMinMax(0, 359);
            bombe.ParticleSpeed = new RandomMinMax(0.1f, 1.0f);
            bombe.ParticleRotation = new RandomMinMax(0, 100);
            bombe.RotationSpeed = new RandomMinMax(0.015f);
            bombe.ParticleFader = new ParticleFader(false, true, 1350);
            bombe.ParticleScaler = new ParticleScaler(false, 0.3f);
            bombe.Position = new Vector2(400, 400);
            particleComponent.particleEmitterList.Add(bombe);
            //***************fumée**************************

            smokeEmitter.Active = false;
            smokeEmitter.TextureList.Add(Ressources.Content.Load<Texture2D>("smoke"));
            smokeEmitter.RandomEmissionInterval = new RandomMinMax(100);
            smokeEmitter.ParticleLifeTime = 9000;
            smokeEmitter.ParticleDirection = new RandomMinMax(-5, 5);
            smokeEmitter.ParticleSpeed = new RandomMinMax(0.6f);
            smokeEmitter.ParticleRotation = new RandomMinMax(0);
            smokeEmitter.RotationSpeed = new RandomMinMax(-0.008f, 0.008f);
            smokeEmitter.ParticleFader = new ParticleFader(true, true);
            smokeEmitter.ParticleScaler = new ParticleScaler(0.15f, 0.7f, 400, smokeEmitter.ParticleLifeTime);
            smokeEmitter.Position = new Vector2(0, 0);
            particleComponent.particleEmitterList.Add(smokeEmitter);

            //*********************************************
            //*******************************************

            #endregion
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

            #region Test collision Bonus
            for (int i = 0; i < Map.ListBonus.Count; i++)
            {
                if (container.Intersects(Map.ListBonus[i]))
                {
                    map.Objets[Map.ListBonus[i].X / 45, Map.ListBonus[i].Y / 45] = map.Objets[1, 1];
                    Map.ListBonus[i] = new Rectangle(0, 0, 45, 45);
                    AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                    Bonus.Execute(random.Next(0, 3), ref bonusVitesse);
                    random = new Random();
                    t0 = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds);
                }
            }
            #endregion

            #region Bonus Vitesse
            if (bonusVitesse)
            {
                t1 = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds);
                AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);

                positionGlobale.X += 10;
                ennemi.positionGlobale.X += 9;

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

            #region Test Bombe + Moteur à particules
            Emitter bomb = particleComponent.particleEmitterList[0];
            Emitter fum = particleComponent.particleEmitterList[1];

            if (!explosion)
            {
                for (int i = 0; i < Map.ListBombe.Count; i++)
                {
                    if (container.Intersects(Map.ListBombe[i]))
                    {
                        AudioRessources.getbomb.Play(AudioRessources.volume, 0f, 0f);
                        tfloat1 = gameTime.TotalGameTime.TotalMilliseconds;
                        Emitter.bombe = true;
                        bomb.Active = true;
                        bomb.ParticleSpeed = new RandomMinMax(0.6f);
                        float Y = bomb.Position.X;
                        Y = Y + 1.5f;
                        bomb.Position = new Vector2(positionGlobale.X - ennemi.camera.Position.X + 50, positionGlobale.Y + 20);
                        vitesse.X = 0;
                        ennemi.vitesse.X = 0;
                        explosion = true;

                        //t3.Active = false;
                    }
                    else
                    {
                        Emitter.bombe = false;
                        bomb.Active = false;
                        // activpart = false;
                    }

                }
            }
            else
            {
                tfloat2 = gameTime.TotalGameTime.TotalMilliseconds;
                if (tfloat2 - tfloat1 > 0.8f)
                {
                    bomb.Active = false;
                    GameState.State = "GameOver";
                    fum.Active = true;
                    fum.Position = new Vector2(positionGlobale.X - ennemi.camera.Position.X + 50, positionGlobale.Y + 20);
                    if (fum.EmittedNewParticle)
                    {
                        float f = MathHelper.ToRadians(fum.LastEmittedParticle.Direction + 180);
                        fum.LastEmittedParticle.Rotation = f;
                    }
                }
                else if ((tfloat2 - tfloat1 > 0.5f))
                {
                    fum.Active = true;
                    fum.Position = new Vector2(positionGlobale.X - ennemi.camera.Position.X + 50, positionGlobale.Y + 20);
                    if (fum.EmittedNewParticle)
                    {
                        float f = MathHelper.ToRadians(fum.LastEmittedParticle.Direction + 180);
                        fum.LastEmittedParticle.Rotation = f;
                    }
                }
            }
            #endregion bombe

            #region kb

            /* (KB.New.IsKeyDown(Keys.Left))
                positionGlobale.X -= 10;

            if (KB.New.IsKeyDown(Keys.Right))
                positionGlobale.X += 10;*/

            if (KB.New.IsKeyDown(Keys.Up) && vitesse.Y == 0)
                force.Y -= 42000;

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
            /*            if (!jumping)
                          heroAnime.Draw(spriteBatch, new Vector2((positionGlobale.X + positionInitiale.X - positionCam.X), positionGlobale.Y), 3);
                        else
                        spriteBatch.Draw(texture, new Rectangle((int)(positionGlobale.X + positionInitiale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.White);
                        */
            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)(positionGlobale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.Red);

            if (bonusVitesse)
                spriteBatch.Draw(superman, new Rectangle(200, 200, 200,200), Color.White);
        }

        private void testCollision(Objet objet)
        {
            if (container.Intersects(objet.container) && container.X + container.Width <= objet.container.X + objet.container.Width)
            {
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

                /*
                //collision top hero
                if (lastPos.Y > objet.container.Y + objet.container.Height &&
                    container.X + container.Width >= objet.container.X &&
                    container.Y <= objet.container.Y + objet.container.Height)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y + objet.container.Height;
                }*/
            }

        }
    }
}
