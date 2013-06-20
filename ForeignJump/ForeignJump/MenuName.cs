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
    class MenuName
    {
        private Texture2D menubg; //bg
        private SpriteFont font; //font description

        private string name;
        private char tempChar;

        public MenuName()
        {
            name = "";
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            menubg = Ressources.Content.Load<Texture2D>("Menu/menuNameBG");
            font = Ressources.Pericles30;
        }

        public void Update()
        {
            if (name.Length <= 15)
            {
                tempChar = KeyToChar();
                if (tempChar != '}')
                    name += tempChar;
            }

            if (KB.New.IsKeyDown(Keys.Back) && !KB.Old.IsKeyDown(Keys.Back))
            {
                name = Backspace(name);
            }

            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter) && name != "")
            {
                Statistiques.Name = name;
                name = "";
                GameState.State = "initial";
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
            spriteBatch.DrawString(font, "Please enter your name: ", new Vector2(50, 250), Color.White);
            spriteBatch.DrawString(Ressources.Scratch26, name, new Vector2(100, 350), Color.White);
        }

        private char KeyToChar()
        {
            if (KB.New.IsKeyDown(Keys.A) && !KB.Old.IsKeyDown(Keys.A))
                return 'a';
            else if (KB.New.IsKeyDown(Keys.B) && !KB.Old.IsKeyDown(Keys.B))
                return 'b';
            else if (KB.New.IsKeyDown(Keys.C) && !KB.Old.IsKeyDown(Keys.C))
                return 'c';
            else if (KB.New.IsKeyDown(Keys.D) && !KB.Old.IsKeyDown(Keys.D))
                return 'd';
            else if (KB.New.IsKeyDown(Keys.E) && !KB.Old.IsKeyDown(Keys.E))
                return 'e';
            else if (KB.New.IsKeyDown(Keys.F) && !KB.Old.IsKeyDown(Keys.F))
                return 'f';
            else if (KB.New.IsKeyDown(Keys.G) && !KB.Old.IsKeyDown(Keys.G))
                return 'g';
            else if (KB.New.IsKeyDown(Keys.H) && !KB.Old.IsKeyDown(Keys.H))
                return 'h';
            else if (KB.New.IsKeyDown(Keys.I) && !KB.Old.IsKeyDown(Keys.I))
                return 'i';
            else if (KB.New.IsKeyDown(Keys.J) && !KB.Old.IsKeyDown(Keys.J))
                return 'j';
            else if (KB.New.IsKeyDown(Keys.K) && !KB.Old.IsKeyDown(Keys.K))
                return 'k';
            else if (KB.New.IsKeyDown(Keys.L) && !KB.Old.IsKeyDown(Keys.L))
                return 'l';
            else if (KB.New.IsKeyDown(Keys.M) && !KB.Old.IsKeyDown(Keys.M))
                return 'm';
            else if (KB.New.IsKeyDown(Keys.N) && !KB.Old.IsKeyDown(Keys.N))
                return 'n';
            else if (KB.New.IsKeyDown(Keys.O) && !KB.Old.IsKeyDown(Keys.O))
                return 'o';
            else if (KB.New.IsKeyDown(Keys.P) && !KB.Old.IsKeyDown(Keys.P))
                return 'p';
            else if (KB.New.IsKeyDown(Keys.Q) && !KB.Old.IsKeyDown(Keys.Q))
                return 'q';
            else if (KB.New.IsKeyDown(Keys.R) && !KB.Old.IsKeyDown(Keys.R))
                return 'r';
            else if (KB.New.IsKeyDown(Keys.S) && !KB.Old.IsKeyDown(Keys.S))
                return 's';
            else if (KB.New.IsKeyDown(Keys.T) && !KB.Old.IsKeyDown(Keys.T))
                return 't';
            else if (KB.New.IsKeyDown(Keys.U) && !KB.Old.IsKeyDown(Keys.U))
                return 'u';
            else if (KB.New.IsKeyDown(Keys.V) && !KB.Old.IsKeyDown(Keys.V))
                return 'v';
            else if (KB.New.IsKeyDown(Keys.W) && !KB.Old.IsKeyDown(Keys.W))
                return 'w';
            else if (KB.New.IsKeyDown(Keys.X) && !KB.Old.IsKeyDown(Keys.X))
                return 'x';
            else if (KB.New.IsKeyDown(Keys.Y) && !KB.Old.IsKeyDown(Keys.Y))
                return 'y';
            else if (KB.New.IsKeyDown(Keys.Z) && !KB.Old.IsKeyDown(Keys.Z))
                return 'z';
            else if (KB.New.IsKeyDown(Keys.Space) && !KB.Old.IsKeyDown(Keys.Space))
                return ' ';
            else if (KB.New.IsKeyDown(Keys.D0) && !KB.Old.IsKeyDown(Keys.D0))
                return '0';
            else if (KB.New.IsKeyDown(Keys.D1) && !KB.Old.IsKeyDown(Keys.D1))
                return '1';
            else if (KB.New.IsKeyDown(Keys.D2) && !KB.Old.IsKeyDown(Keys.D2))
                return '2';
            else if (KB.New.IsKeyDown(Keys.D3) && !KB.Old.IsKeyDown(Keys.D3))
                return '3';
            else if (KB.New.IsKeyDown(Keys.D4) && !KB.Old.IsKeyDown(Keys.D4))
                return '4';
            else if (KB.New.IsKeyDown(Keys.D5) && !KB.Old.IsKeyDown(Keys.D5))
                return '5';
            else if (KB.New.IsKeyDown(Keys.D6) && !KB.Old.IsKeyDown(Keys.D6))
                return '6';
            else if (KB.New.IsKeyDown(Keys.D7) && !KB.Old.IsKeyDown(Keys.D7))
                return '7';
            else if (KB.New.IsKeyDown(Keys.D8) && !KB.Old.IsKeyDown(Keys.D8))
                return '8';
            else if (KB.New.IsKeyDown(Keys.D9) && !KB.Old.IsKeyDown(Keys.D9))
                return '9';
            else if (KB.New.IsKeyDown(Keys.NumPad0) && !KB.Old.IsKeyDown(Keys.NumPad0))
                return '0';
            else if (KB.New.IsKeyDown(Keys.NumPad1) && !KB.Old.IsKeyDown(Keys.NumPad1))
                return '1';
            else if (KB.New.IsKeyDown(Keys.NumPad2) && !KB.Old.IsKeyDown(Keys.NumPad2))
                return '2';
            else if (KB.New.IsKeyDown(Keys.NumPad3) && !KB.Old.IsKeyDown(Keys.NumPad3))
                return '3';
            else if (KB.New.IsKeyDown(Keys.NumPad4) && !KB.Old.IsKeyDown(Keys.NumPad4))
                return '4';
            else if (KB.New.IsKeyDown(Keys.NumPad5) && !KB.Old.IsKeyDown(Keys.NumPad5))
                return '5';
            else if (KB.New.IsKeyDown(Keys.NumPad6) && !KB.Old.IsKeyDown(Keys.NumPad6))
                return '6';
            else if (KB.New.IsKeyDown(Keys.NumPad7) && !KB.Old.IsKeyDown(Keys.NumPad7))
                return '7';
            else if (KB.New.IsKeyDown(Keys.NumPad8) && !KB.Old.IsKeyDown(Keys.NumPad8))
                return '8';
            else if (KB.New.IsKeyDown(Keys.NumPad9) && !KB.Old.IsKeyDown(Keys.NumPad9))
                return '9';
            else return '}';
        }

        private string Backspace(string name)
        {
            if (name.Length >= 1)
                return name.Remove(name.Length - 1);
            else
                return name;
        }
    }
}