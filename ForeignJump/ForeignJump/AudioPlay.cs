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
    class AudioPlay
    {
        public float volume;
        
        public AudioPlay(float volume)
        {
            this.volume = volume;
        }

        public void Update()
        {
            volume = AudioRessources.volume;

            if (GameState.State == "inGame")
            {
                if (KB.New.IsKeyDown(Keys.Up) && !KB.Old.IsKeyDown(Keys.Up))
                    AudioRessources.jump.Play(volume, 0f, 0f );
            }

            if ((GameState.State == "menuAide" || GameState.State == "menuChoose" || GameState.State == "menuOptions" || GameState.State == "menuPauseAide")
                && (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape)))
                AudioRessources.escape.Play(volume, 0f, 0f );

            if (GameState.State == "initial" || GameState.State == "menuPause")
            {
                if (KB.New.IsKeyDown(Keys.Up) && !KB.Old.IsKeyDown(Keys.Up))
                    AudioRessources.selectionup.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Down) && !KB.Old.IsKeyDown(Keys.Down))
                    AudioRessources.selectiondown.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
                    AudioRessources.confirmation.Play(volume, 0f, 0f );
            }

            if (GameState.State == "menuOptions")
            {
                if (KB.New.IsKeyDown(Keys.Up) && !KB.Old.IsKeyDown(Keys.Up))
                    AudioRessources.selectionup.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Down) && !KB.Old.IsKeyDown(Keys.Down))
                    AudioRessources.selectiondown.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
                    AudioRessources.confirmation.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right))
                    AudioRessources.turn.Play(volume, 0f, 0f );
                
                if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left))
                    AudioRessources.turn.Play(volume, 0f, 0f );
            }

            if (GameState.State == "menuChoose")
            {
                if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
                    AudioRessources.confirmation.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right))
                    AudioRessources.turn.Play(volume, 0f, 0f );

                if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left))
                    AudioRessources.turn.Play(volume, 0f, 0f );
            }

        }
    }
}
