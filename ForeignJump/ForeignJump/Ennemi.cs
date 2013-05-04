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
    class Ennemi : Personnage
    {
        private Map map;
        private Hero hero;
        public Camera camera;

        public SpriteFont font;

        private Objet currentObjet;

        //particules
        ParticleComponent particleComponent;

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
            this.currentObjet = new Objet();
            this.hero = hero;

            font = Ressources.GetPerso(Perso.Choisi).font;

            #region moteur à particules
            particleComponent = new ParticleComponent(Ressources.Game);
            Ressources.Game.Components.Add(particleComponent);
            Emitter fireEmitter = new Emitter();
            fireEmitter.Active = false;
            fireEmitter.TextureList.Add(Ressources.GetPerso(Perso.Choisi).obstacle);
            fireEmitter.RandomEmissionInterval = new RandomMinMax(50);
            fireEmitter.ParticleLifeTime = 9000;
            fireEmitter.ParticleDirection = new RandomMinMax(270, 300);
            fireEmitter.ParticleSpeed = new RandomMinMax(10, 20); // g modifié le moteur a particule;
            fireEmitter.ParticleRotation = new RandomMinMax(0, 180);
            fireEmitter.RotationSpeed = new RandomMinMax(0.04f);
            fireEmitter.ParticleFader = new ParticleFader(false, true, 30);
            fireEmitter.ParticleScaler = new ParticleScaler(0.2f, 0.18f, 0, 100);
            fireEmitter.Position = new Vector2(400, 650);
            //******piece*********
            Emitter desinté_piece = new Emitter();
            desinté_piece.Active = false;
            desinté_piece.TextureList.Add(Ressources.GetPerso(Perso.Choisi).piece);
            desinté_piece.RandomEmissionInterval = new RandomMinMax(50);
            desinté_piece.ParticleLifeTime = 9000;
            desinté_piece.ParticleDirection = new RandomMinMax(270, 300);
            desinté_piece.ParticleSpeed = new RandomMinMax(10, 20); // g modifié le moteur a particule;
            desinté_piece.ParticleRotation = new RandomMinMax(0, 180);
            desinté_piece.RotationSpeed = new RandomMinMax(0.04f);
            desinté_piece.ParticleFader = new ParticleFader(false, true, 30);
            desinté_piece.ParticleScaler = new ParticleScaler(1.2f, 0.18f, 0, 100);
            desinté_piece.Position = new Vector2(400, 650);
            particleComponent.particleEmitterList.Add(fireEmitter);
            particleComponent.particleEmitterList.Add(desinté_piece);
            #endregion
        }

        public void Update(GameTime gameTime, float speed)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            personnageAnime.Update(speed); //Animation
            force.Y = 600;
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
            int activeParticles = 0;
            foreach (Emitter activeEmitters in particleComponent.particleEmitterList)
            {
                activeParticles += activeEmitters.ParticleList.Count();
            }

            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).barre, new Rectangle((int)(positionGlobale.X - positionCam.X), (int)positionGlobale.Y, container.Width, container.Height), Color.AliceBlue);
        }

        private void testCollision(Objet objet)
        {
            if (container.Intersects(hero.container))
            {
                GameState.State = "GameOver";
            }
            //création nouvelle particule
            Emitter t3 = particleComponent.particleEmitterList[0];
            Emitter t4 = particleComponent.particleEmitterList[1];

            if (container.Intersects(objet.container))
            {
                //collision bas ennemi
                if (container.X + container.Width >= objet.container.X &&
                    lastPos.Y + container.Height <= objet.container.Y &&
                    container.Y + container.Height >= objet.container.Y)
                {
                    vitesse.Y = 0;
                    positionGlobale.Y = objet.container.Y - container.Height;
                }

                //collision côté droit ennemi
                if (map.Objets[(int)(objet.container.X / 45), (int)(objet.container.Y / 45)].type == TypeCase.Terre)
                {
                    if (container.X + container.Width >= objet.container.X &&
                        lastPos.Y + container.Height > objet.container.Y && GameState.State == "inGame")
                    {
                        t3.Active = true;
                        t3.ParticleSpeed = new RandomMinMax(0.6f);
                        float Y = t3.Position.X;
                        Y = Y + 1.5f;
                        t3.Position = new Vector2(positionGlobale.X - camera.Position.X, positionGlobale.Y);

                        map.Objets[(int)(objet.container.X / 45), (int)(objet.container.Y / 45)].texture = Ressources.GetPerso(Perso.Choisi).nulle;

                        //t3.Active = false;
                    }
                    else
                    {
                        t3.Active = false;
                        // activpart = false;
                    }
                }
                else
                {
                    t3.Active = false;
                    // activpart = false;
                }
                if (map.Objets[(int)(objet.container.X / 45), (int)(objet.container.Y / 45)].type == TypeCase.Piece)
                {
                    if (container.X + container.Width >= objet.container.X &&
                        lastPos.Y + container.Height > objet.container.Y && GameState.State == "inGame")
                    {

                        t4.Active = true;
                        t4.ParticleSpeed = new RandomMinMax(0.6f);
                        float Y = t3.Position.X;
                        Y = Y + 1.5f;
                        t4.Position = new Vector2(positionGlobale.X - camera.Position.X, positionGlobale.Y);

                        map.Objets[(int)(objet.container.X / 45), (int)(objet.container.Y / 45)].texture = Ressources.GetPerso(Perso.Choisi).nulle;

                        //t3.Active = false;
                    }
                    else
                    {
                        t4.Active = false;
                        // activpart = false;
                    }
                }
                else
                {
                    t4.Active = false;
                    // activpart = false;
                }
            }
            if ((map.Objets[(int)(objet.container.X / 45 + 1), (int)(objet.container.Y / 45)].type == TypeCase.Eau) && (vitesse.Y == 0) && (map.Objets[(int)(objet.container.X / 45 + 4), (int)(objet.container.Y / 45)].type == TypeCase.Eau))
            {
                force.Y -= 49000;
            }
            else if ((map.Objets[(int)(objet.container.X / 45 + 1), (int)(objet.container.Y / 45)].type == TypeCase.Eau) && (vitesse.Y == 0))
            {
                force.Y -= 40000;
            }
            
        }
    }
}
