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
    public class Menu
    {
        public GameState gameState;
        public GameState realGameState;

        //BG
        private Texture2D menubg;
        private Texture2D idle;
        private Texture2D hover;

        #region Button positions

        public Vector2 PositionJouer
        {
            get { return positionJouer; }
            set { positionJouer = value; }
        }
        private Vector2 positionJouer;

        public Vector2 PositionOptions
        {
            get { return positionOptions; }
            set { positionOptions = value; }
        }
        private Vector2 positionOptions;

        public Vector2 PositionAide
        {
            get { return positionAide; }
            set { positionAide = value; }
        }
        private Vector2 positionAide;

        public Vector2 PositionExit
        {
            get { return positionExit; }
            set { positionExit = value; }
        }
        private Vector2 positionExit;

        #endregion

        #region Buttons
        
        private Button buttonJouer;
        private Button buttonOptions;
        private Button buttonAide;
        private Button buttonExit;
        private Button buttonJouerH;
        private Button buttonOptionsH;
        private Button buttonAideH;
        private Button buttonExitH;
        
        #endregion

        #region Parcours menu

        public int Selection
        {
            get { return selection; }
            set { selection = value; }
        }
        private int selection;

        public int Appuye
        {
            get { return appuye; }
            set { appuye = value; }
        }
        private int appuye;

        public int Relache
        {
            get { return relache; }
            set { relache = value; }
        }
        private int relache;

        #endregion

        public Menu()
        {
        }

        public void Initialize(int milieu)
        {
            positionJouer = new Vector2(milieu, -900);
            positionOptions = new Vector2(milieu, -700);
            positionAide = new Vector2(milieu, -500);
            positionExit = new Vector2(milieu, -300);

            gameState = new GameState();
            gameState.Initialize();

            realGameState = gameState;

            selection = 1;
            appuye = 1;
            relache = 1;
        }

        public void LoadContent(ContentManager Content)
        {
            menubg = Content.Load<Texture2D>("menubg");
            idle = Content.Load<Texture2D>("idle");
            hover = Content.Load<Texture2D>("hover");

        }

        public void Update(GameTime gameTime, int vitesse)
        {
            KeyboardState newState = Keyboard.GetState(); //Gestion clavier

        buttonJouer = new Button(idle, new Rectangle((int)positionJouer.X, (int)positionJouer.Y, 200, 140));
        buttonOptions = new Button(idle, new Rectangle((int)positionOptions.X, (int)positionOptions.Y, 200, 140));
        buttonAide = new Button(idle, new Rectangle((int)positionAide.X, (int)positionAide.Y, 200, 140));
        buttonExit = new Button(idle, new Rectangle((int)positionExit.X, (int)positionExit.Y, 200, 140));
        buttonJouerH = new Button(hover, new Rectangle((int)positionJouer.X, (int)positionJouer.Y, 200, 140));
        buttonOptionsH = new Button(hover, new Rectangle((int)positionOptions.X, (int)positionOptions.Y, 200, 140));
        buttonAideH = new Button(hover, new Rectangle((int)positionAide.X, (int)positionAide.Y, 200, 140));
        buttonExitH = new Button(hover, new Rectangle((int)positionExit.X, (int)positionExit.Y, 200, 140));


        if (newState.IsKeyDown(Keys.Up) && appuye == 0)
        {

            if (selection == 1)
            {
                selection = 4;
            }
            else
            {
                selection = selection - 1;
            }
            appuye = 1;

        }
        else if (newState.IsKeyDown(Keys.Down) && relache == 0)
        {

            if (selection == 4)
            {
                selection = 1;
            }
            else
            {
                selection = selection + 1;
            }
            relache = 1;

        }

        if (newState.IsKeyUp(Keys.Up))
        {
            appuye = 0;
        }
        if (newState.IsKeyUp(Keys.Down))
        {
            relache = 0;
        }

        if (newState.IsKeyDown(Keys.Enter) && selection == 1)
        {
            gameState.Update("load");
        }
        if (newState.IsKeyDown(Keys.Enter) && selection == 4)
        {
            System.Environment.Exit(0);
        }

        if (gameState.Get() == "load")
        {
             if (positionJouer.Y > -900)
             {  
                positionJouer.Y -= vitesse + 2;
             }

             if (positionOptions.Y > -700)
             {
                 positionOptions.Y -= vitesse + 1;
             }
            
             if (positionAide.Y > -500)
             {  
                 positionAide.Y -= vitesse;
             }
             
             if (positionExit.Y > -300)
             {
                positionExit.Y -= vitesse - 1;
             }
        }
        else
        {

            if (positionJouer.Y < 240)
            {
                positionJouer.Y += vitesse;
            }
            if (positionOptions.Y < 325)
            {
                positionOptions.Y += vitesse;
            }
            if (positionAide.Y < 420)
            {
                positionAide.Y += vitesse;
            }
            if (positionExit.Y < 520)
            {
                positionExit.Y += vitesse;
            }

        }

        if (positionExit.Y <= -300 && gameState.Get() == "load")
        {
            gameState.Update("inGame");
        }

        realGameState = gameState;


        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg,new Rectangle(0,0, 1280,800),Color.White);

            if (selection == 1)
            {
                buttonJouerH.DrawButton(spriteBatch);
            }
            else
            {
                buttonJouer.DrawButton(spriteBatch);
            }
            if (selection == 2)
            {
                buttonOptionsH.DrawButton(spriteBatch);
            }
            else
            {
                buttonOptions.DrawButton(spriteBatch);
            }
            if (selection == 3)
            {
                buttonAideH.DrawButton(spriteBatch);
            }
            else
            {
                buttonAide.DrawButton(spriteBatch);
            }
            if (selection == 4)
            {
                buttonExitH.DrawButton(spriteBatch);
            }
            else
            {
                buttonExit.DrawButton(spriteBatch);
            }
        }
    }
}
