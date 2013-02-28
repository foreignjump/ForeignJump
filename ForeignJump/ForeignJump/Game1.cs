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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState mouseStateCurrent;

        private Menu menu; //déclaration de menu initial
        private MenuPause menupause; //déclaration de menu pause
        private MenuPauseAide menupauseaide; //déclaration de menu pause
        private MenuAide menuaide; //déclaration du menu aide
        private MenuOptions menuoptions; //déclaration du menu options
        private MenuChoose menuchoose; //déclaration du menu de choix de personnage
        private Gameplay game; //déclaration du gameplay
        private GameOver gameover; //déclaration du popup game over

        private AudioPlay audioPlay;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            Ressources.Content = Content;
            AudioRessources.Content = Content;
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            KB.Old = Keyboard.GetState();

            menu = new Menu();
            menu.Initialize(-37, 0); //initialisation menu

            game = new Gameplay();
            //game.Initialize(); //initialisation game

            menupause = new MenuPause(game);
            menupause.Initialize(450, 0); //initialisation menu pause

            menupauseaide = new MenuPauseAide();

            menuaide = new MenuAide();
            menuaide.Initialize(); //initialisation menu aide

            menuoptions = new MenuOptions();
            menuoptions.Initialize(); //initialisation menu options

            menuchoose = new MenuChoose(game, Content);
            menuchoose.Initialize(); //initialisation menu options

            gameover = new GameOver(game);
            gameover.Initialize(); //initialisation menu options

            audioPlay = new AudioPlay(1f);

            GameState.State = "initial"; //mise à l'état initial

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Ressources.Load();
            AudioRessources.Load();

            menu.LoadContent(Content); //charger menu
            //game.LoadContent(Content); //charger game
            menupause.LoadContent(Content); //charger menu pause
            menupauseaide.LoadContent(Content); //charger menu pause aide
            menuaide.LoadContent(Content); //charger menu aide
            menuoptions.LoadContent(Content); //charger menu options
            menuchoose.LoadContent(); //charger menu choix de personnage
            gameover.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState(); //gestion souris
            KB.New = Keyboard.GetState(); //verification clavier

            audioPlay.Update();

            if (KB.New.IsKeyDown(Keys.Tab) && !KB.Old.IsKeyDown(Keys.Tab))
                System.Environment.Exit(0);

            if (GameState.State == "initial") //mise à jour menu
                menu.Update(gameTime, 8);

            if (GameState.State == "inGame") //mise à jour game
                game.Update(gameTime);

            //menuPause
            if (GameState.State == "menuPause") //mise à jour menu pause
                menupause.Update(gameTime, 3);

            if (GameState.State == "menuPauseAide") //mise à jour menu pause aide
                menupauseaide.Update();
            //menuPause

            if (GameState.State == "menuAide") //mise à jour menu aide
                menuaide.Update(gameTime, 5);

            if (GameState.State == "menuOptions") //mise à jour menu aide
                menuoptions.Update(gameTime, 5, graphics);

            if (GameState.State == "menuChoose") //mise à jour menu aide
                menuchoose.Update(gameTime, 5);

            if (GameState.State == "GameOver") //mise à jour du game over
                gameover.Update(gameTime);

            KB.Old = KB.New;

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //DEBUT

            menu.Draw(spriteBatch, gameTime, true); //afficher menu

            if (GameState.State == "inGame" || GameState.State == "menuPause" || GameState.State == "GameOver") //afficher jeu
                game.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuPause") //afficher menu pause
                menupause.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuPauseAide") //afficher menu pause
            {
                game.Draw(spriteBatch, gameTime);
                menupauseaide.Draw(spriteBatch);
            }

            if (GameState.State == "menuAide") //afficher menu pause
                menuaide.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuOptions") //afficher menu pause
                menuoptions.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuChoose") //afficher menu pause
                menuchoose.Draw(spriteBatch, gameTime);

            if (GameState.State == "GameOver") //afficher le popup game over
                gameover.Draw(spriteBatch, gameTime);

            spriteBatch.End(); //FIN

            base.Draw(gameTime);
        }



    }
}