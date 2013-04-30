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
    class Particule
    {
        ParticleComponent particleComponent;
        public Particule()
        {
            particleComponent = new ParticleComponent(ContentLoad.Game);
            ContentLoad.Game.Components.Add(particleComponent);

        }
        public void LoadContent()
        {
            
            Emitter fireEmitter = new Emitter();
            fireEmitter.Active = false;
            fireEmitter.TextureList.Add(ContentLoad.load.Load<Texture2D>("fire"));
            fireEmitter.RandomEmissionInterval = new RandomMinMax(300);
            fireEmitter.ParticleLifeTime = 1500;
            fireEmitter.ParticleDirection = new RandomMinMax(0);
            fireEmitter.ParticleSpeed = new RandomMinMax(0.3f);
            fireEmitter.ParticleRotation = new RandomMinMax(0);
            fireEmitter.RotationSpeed = new RandomMinMax(0);
            fireEmitter.ParticleFader = new ParticleFader(true, true, 0);
            fireEmitter.ParticleScaler = new ParticleScaler(0.1f, 0.2f, 0, 1000);
            fireEmitter.Position = new Vector2(400, 650);

            particleComponent.particleEmitterList.Add(fireEmitter);
        }
        public void update(Vector2 position)
        {
            Emitter t2 = particleComponent.particleEmitterList[0];
            t2.Position = position;
            if (t2.EmittedNewParticle)
            {
                t2.Position = position;
                t2.Active = true;
                float f = MathHelper.ToRadians(t2.LastEmittedParticle.Direction + 180);
                t2.LastEmittedParticle.Rotation = f;
            }

        }
        public void Draw()
        {
            int activeParticles = 0;
            foreach (Emitter activeEmitters in particleComponent.particleEmitterList)
            {
                activeParticles += activeEmitters.ParticleList.Count();
            }
        }
    }
}
