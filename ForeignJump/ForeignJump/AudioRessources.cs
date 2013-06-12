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
    public static class AudioRessources
    {
        public static SoundEffect confirmation;
        public static SoundEffect jump;
        public static SoundEffect selectiondown;
        public static SoundEffect selectionup;
        public static SoundEffect pas;
        public static SoundEffect turn;
        public static SoundEffect escape;
        public static SoundEffect wingold;
        public static SoundEffect winbonus;
        public static SoundEffect getbomb;
        public static SoundEffect MenuMusic;

        public static float volume;

        public static ContentManager Content;

        public static void Load()
        {
            Content = Ressources.Content;

            confirmation = Content.Load<SoundEffect>("Sound/confirmation");
            jump = Content.Load<SoundEffect>("Sound/Jump");
            selectiondown = Content.Load<SoundEffect>("Sound/selectionDown");
            selectionup = Content.Load<SoundEffect>("Sound/selectionUp");
            turn = Content.Load<SoundEffect>("Sound/turn");
            escape = Content.Load<SoundEffect>("Sound/escape");
            wingold = Content.Load<SoundEffect>("Sound/wingold");
//            winbonus = Content.Load<SoundEffect>("Sound/winbonus");
            getbomb = Content.Load<SoundEffect>("Sound/getbomb");
            volume = 0f;
        }


    }
}