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

        //map actuelle
        private Map map;

        //objet en collision
        private Objet currentObjet;

        //ennemi
        public Ennemi ennemi;

        //random pour bonus
        private Random random;

        //l'abaissement du personnage
        private bool down;
        private Rectangle containerDown;
        private Rectangle containerUp;
        
        //ACDC
        public Rectangle containerACDC;
        public bool acdc;
        private Texture2D acdcTexture;

        //superman
        public bool bonusVitesse;
        private Texture2D superman;
        int t0, t1;

        //particules bombe
        double tfloat1, tfloat2;
        ParticleComponent particleComponent;
        bool explosion;
        Emitter bombe = new Emitter();
        public static Emitter smokeEmitter = new Emitter();

        public Hero(Animate textureAnime, Vector2 position, Vector2 vitesse, float poids, Map map)
        {
            this.texture = Ressources.GetPerso(Perso.Choisi).heroTexture;
            this.textureAnime = Ressources.GetPerso(Perso.Choisi).heroTextureAnime;
            this.textureDown = Ressources.GetPerso(Perso.Choisi).heroTextureDown;
            this.personnageAnime = Ressources.GetPerso(Perso.Choisi).heroAnime;
            this.positionGlobale = position;
            this.positionInitiale = position;
            this.vitesse = vitesse;
            this.vitesseInitiale = vitesse;
            this.type = TypePerso.Hero;
            this.poids = new Vector2(0, poids);
            this.force = Vector2.Zero;
            this.reaction = Vector2.Zero;
            this.map = map;

            containerDown = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y + texture.Height - textureDown.Height, textureDown.Width, textureDown.Height);
            containerUp = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y, texture.Width, texture.Height);
            this.container = containerUp;
            down = false;

            acdcTexture = Ressources.Content.Load<Texture2D>("ACDC");
            containerACDC = new Rectangle((int)positionGlobale.X - acdcTexture.Width, (int)positionGlobale.Y, acdcTexture.Width, acdcTexture.Height);
            acdc = false;

            currentObjet = new Objet();

            animate = true;

            random = new Random();
            bonusVitesse = false;
            superman = Ressources.Content.Load<Texture2D>("superman");

            font = Ressources.GetPerso(Perso.Choisi).font;

            //moteur à particule pour la bombe
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


        public void Update(GameTime gameTime, float speed)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            personnageAnime.Update(speed); //Animation

            //container pour le personnage a l'horisontale
            containerDown = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y + texture.Height - textureDown.Height, textureDown.Width, textureDown.Height);
            //container pour le personnage debout
            containerUp = new Rectangle((int)positionGlobale.X, (int)positionGlobale.Y, texture.Width, texture.Height);

            if (down)
                container = containerDown;
            else
                container = containerUp;

            //force pour equilibre
            force.Y = 500;

            containerACDC = new Rectangle((int)positionGlobale.X - acdcTexture.Width + 20, (int)positionGlobale.Y - 10, acdcTexture.Width, acdcTexture.Height);

            #region Test collision ACDC
            for (int i = 0; i < Map.ListACDC.Count; i++)
            {
                if (container.Intersects(Map.ListACDC[i]))
                {
                    map.Objets[Map.ListACDC[i].X / 45, Map.ListACDC[i].Y / 45] = map.Objets[1, 1];
                    Map.ListACDC[i] = new Rectangle(0, 0, 45, 45);
                    acdc = true;
                }
            }
            #endregion

            #region Test collision Bonus
            for (int i = 0; i < Map.ListBonus.Count; i++)
            {
                if (container.Intersects(Map.ListBonus[i]))
                {
                    map.Objets[Map.ListBonus[i].X / 45, Map.ListBonus[i].Y / 45] = map.Objets[1, 1];
                    Map.ListBonus[i] = new Rectangle(0, 0, 45, 45);
                    AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                    Bonus.Execute(random.Next(0, 4), ref bonusVitesse);
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

            currentObjet = new Objet();
            currentObjet.container.Width = 45;
            currentObjet.container.Height = 45;

            int posY = (int)(container.Y / 45);
            int posX = (int)(container.X / 45);

            if (down)
            {
                #region Test cases adjacentes Down
                for (int i = 0; i <= 4; i++)
                {
                    for (int j = 0; j <= 2; j++)
                    {
                        int currentX = posX + i;
                        int currentY = posY + j;

                        if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
                        {
                            currentObjet.container.X = currentX * 45;
                            currentObjet.container.Y = currentY * 45;
                            testCollision(currentObjet);

                            //map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barregreencenter;
                        }

                    }
                }
                #endregion
            }
            else
            {
                #region Test cases adjacentes Up
                for (int i = 0; i <= 2; i++)
                {
                    for (int j = 0; j <= 4; j++)
                    {
                        int currentX = posX + i;
                        int currentY = posY + j;

                        if (map.Valid(currentX, currentY) && map.Objets[currentX, currentY].type == TypeCase.Terre)
                        {
                            currentObjet.container.X = currentX * 45;
                            currentObjet.container.Y = currentY * 45;
                            testCollision(currentObjet);

                            //map.Objets[currentX, currentY].texture = Ressources.GetPerso(Perso.Choisi).barregreencenter;
                        }
                    }
                }
                #endregion
            }

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
                    GameOver.Die();
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
            #endregion

            #region Keyboard Input

            if (KB.New.IsKeyDown(Keys.Left))
                positionGlobale.X -= 5;

            if (KB.New.IsKeyDown(Keys.Right))
                positionGlobale.X += 5;
            /*
            if (KB.New.IsKeyDown(Keys.Up))
                positionGlobale.Y -= 1;

            if (KB.New.IsKeyDown(Keys.Down))
                positionGlobale.Y += 1;
            */

            if (KB.New.IsKeyDown(Keys.Up) && vitesse.Y == 0)
            {
                force.Y -= 26000;
                animate = false;
            }

            if (KB.New.IsKeyDown(Keys.Down) && vitesse.Y == 0)
            {
                down = true;
            }

            if (KB.New.IsKeyUp(Keys.Down))
            {
                down = false;
            }

            #endregion

            Vector2 acceleration = poids + force; //somme des forces = masse * acceleration

            vitesse += acceleration * dt;
            positionGlobale += vitesse * dt;

            //mise à jour de la position d'avant
            lastPos.X = container.X;
            lastPos.Y = container.Y;

            //si il tombe dans le vide
            if (positionGlobale.Y >= 800)
            {
                GameState.State = "GameOver";
                GameOver.Die();
            } 
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 positionCam)
        {
            if (!animate)
            {
                spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).heroTexture, new Rectangle((int)(container.X - positionCam.X), (int)container.Y, texture.Width, texture.Height), Color.White);
            }
            else
            {
                if (down)//affichage du personnage a l'horisontale
                    spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).heroTextureDown, new Rectangle((int)(container.X - positionCam.X), (int)container.Y, textureDown.Width, textureDown.Height), Color.White);
                else
                    personnageAnime.Draw(spriteBatch, new Vector2(containerUp.X - positionCam.X - 14f, containerUp.Y - 14f), 3);
            }

            if (acdc)
                spriteBatch.Draw(acdcTexture, new Rectangle((int)(containerACDC.X - positionCam.X), containerACDC.Y, acdcTexture.Width, acdcTexture.Height), Color.White);

            if (bonusVitesse)
            {
                spriteBatch.DrawString(font, "SUPERMAN MODE!", new Vector2(60, 200), Color.Red);
                spriteBatch.Draw(superman, new Rectangle(60, 250, superman.Width, superman.Height), Color.White);
            }
        }

        private void testCollision(Objet objet)
        {
            if (container.Intersects(objet.container))
            {
                //collision bas hero
                if (lastPos.Y + container.Height <= objet.container.Y &&
                    (container.X >= objet.container.X ||
                    container.X <= objet.container.X + objet.container.Width) &&
                    container.Y + container.Height >= objet.container.Y)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y - containerUp.Height;
                    animate = true;
                }

                //collision haut hero
                if (lastPos.Y > objet.container.Y + objet.container.Height &&
                    (container.X >= objet.container.X ||
                    container.X <= objet.container.X + objet.container.Width) &&
                    container.Y <= objet.container.Y + objet.container.Height)
                {
                    vitesse.Y = 150; //à revoir.
                }

                //collision côté droit hero
                if (lastPos.Y + container.Height > objet.container.Y &&
                    container.X + container.Width >= objet.container.X &&
                    lastPos.X + container.Width > objet.container.X)
                {
                    bonusVitesse = false;
                    positionGlobale.X = objet.container.X - container.Width;
                }
            }
        }
    }
}
