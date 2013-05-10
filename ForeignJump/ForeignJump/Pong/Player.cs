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
    class Player : PongObject
    {
        private string name;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Player(string name, Texture2D texture, Vector2 position)
        {
            this.name = name;
            this.texture = texture;
            this.position = position;

            this.container = new Rectangle((int)position.X - 15, (int)position.Y, texture.Width - 30, texture.Height);
        }

        public void Update()
        {
            this.container = new Rectangle((int)position.X - 15, (int)position.Y, texture.Width - 30, texture.Height);
        }
    }
}
