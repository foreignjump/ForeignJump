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
    class Audio
    {
        KeyboardState oldState; //gestion clavier

        private AudioEngine engine;
        private WaveBank music;
        private SoundBank sound;
        private Cue track;

        public Audio()
        { }

        public void Initialize()
        {
            oldState = Keyboard.GetState();

        }

        public void LoadContent(ContentManager Content)
        {
           
        }

        public void Update(GameTime gameTime, int vitesse, GraphicsDeviceManager graphics)
        {
            var newState = Keyboard.GetState(); //mettre à jour le clavier

            oldState = newState;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }
    }
}
