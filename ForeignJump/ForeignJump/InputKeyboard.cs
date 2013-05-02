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
    public static class KB
    {
        static KeyboardState newState;

        public static KeyboardState New
        {
            get { return newState; }
            set { newState = value; }
        }

        static KeyboardState oldState;

        public static KeyboardState Old
        {
            get { return oldState; }
            set { oldState = value; }
        }

        public static bool IsAnyKeyPressed()
        {
            Keys[] keys = New.GetPressedKeys();
            return keys.Length == 0 || (keys.Length == 1 && keys[0] == Keys.None);
        }

    }
}
