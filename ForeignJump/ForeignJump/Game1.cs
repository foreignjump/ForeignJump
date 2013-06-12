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
        private GameOver gameover; //déclaration du gameover
        private Pong pong; //déclaration du popup du nouveau jeu
        private KeyBonusGame keybonusgame; //déclaration du popup du jeu de touches

        private AudioPlay audioPlay;

        //**********ENTREE**********
        //générique
        private Video gen;
        private VideoPlayer generique = new VideoPlayer();
        //musique
        private Song MenuMusic;
        private bool play;
        private bool disagenerique;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            Components.Add(new GamerServicesComponent(this)); //Live

            Ressources.Content = Content;
            Ressources.Game = this;
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            KB.Old = Keyboard.GetState();

            Langue.Choisie = "en";
            Ressources.LoadLangue();

            menu = new Menu();
            menu.Initialize(-37, 0); //initialisation menu

            game = new Gameplay();

            menupauseaide = new MenuPauseAide();

            menuaide = new MenuAide();
            menuaide.Initialize(); //initialisation menu aide

            menuchoose = new MenuChoose(game, Content);
            menuchoose.Initialize(); //initialisation menu options

            menuoptions = new MenuOptions(menu, menuaide, menuchoose);
            menuoptions.Initialize(); //initialisation menu options

            gameover = new GameOver();

            pong = new Pong();
            pong.Initialize();

            keybonusgame = new KeyBonusGame();
            keybonusgame.Initialize();

            menupause = new MenuPause(pong, keybonusgame);
            menupause.Initialize(450, 0); //initialisation menu pause

            audioPlay = new AudioPlay(1f);
            GameState.State = "Generique"; //mise à l'état initial

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Ressources.LoadPerso();
            AudioRessources.Load();

            menu.LoadContent(); //charger menu
            menupause.LoadContent(Content); //charger menu pause
            menupauseaide.LoadContent(Content); //charger menu pause aide
            menuaide.LoadContent(); //charger menu aide
            menuoptions.LoadContent(); //charger menu options
            menuchoose.LoadContent(); //charger menu choix de personnage
            pong.LoadContent();
            keybonusgame.LoadContent();
            gen = Content.Load<Video>("Generique");
            MenuMusic = Content.Load<Song>("Sound/MenuMusic");
        }


        protected override void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState(); //gestion souris
            KB.New = Keyboard.GetState(); //verification clavier

            audioPlay.Update();

            //pour que la fumée et feu ne s'affiche que dans le jeu
            if (GameState.State != "inGame" && GameState.State != "GameOver" && GameState.State != "newGame" && GameState.State != "KeyBonusGame")
            {
                Hero.smokeEmitter.Active = false;
                Emitter.statut = false;
            }

            if (KB.New.IsKeyDown(Keys.Tab) && !KB.Old.IsKeyDown(Keys.Tab))
                System.Environment.Exit(0);

            if (GameState.State == "Generique")
            {
                if (gameTime.TotalGameTime.Seconds < gen.Duration.Seconds && !(KB.New.IsKeyDown(Keys.Space)))
                {
                    if (disagenerique == false)
                    {
                        generique.Play(gen);
                        disagenerique = true;
                    }
                    play = false;
                }
                else
                {
                    GameState.State = "initial";
                    disagenerique = true;
                    generique.Stop();
                }
            }

            if (GameState.State == "initial") //mise à jour menu
            {
                menu.Update(gameTime, 8);
                
                if (AudioRessources.volume == 0)
                {
                    MediaPlayer.Stop();
                    play = false;
                }
                else if (!play)
                {
                    MediaPlayer.Play(MenuMusic);
                    play = true;
                }
            }

            if (GameState.State == "inGame") //mise à jour game
            {
                game.Update(gameTime);
                MediaPlayer.Stop();
                play = false;
            }

            //menus
            if (GameState.State == "menuPause") //mise à jour menu pause
                menupause.Update(gameTime, 3);

            if (GameState.State == "menuPauseAide") //mise à jour menu pause aide
                menupauseaide.Update();

            if (GameState.State == "menuAide") //mise à jour menu aide
                menuaide.Update(gameTime, 5);

            if (GameState.State == "menuOptions") //mise à jour menu aide
                menuoptions.Update(gameTime, 5, graphics);

            if (GameState.State == "menuChoose") //mise à jour menu aide
                menuchoose.Update(gameTime, 5);
            //menus fin

            if (GameState.State == "GameOver") //game over
                gameover.Update();

            if (GameState.State == "newGame")
                pong.Update(gameTime, game);

            if (GameState.State == "KeyBonusGame")
                keybonusgame.Update(gameTime);

            KB.Old = KB.New;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //Debut

            menu.Draw(spriteBatch, gameTime, true); //afficher menu

            if (GameState.State == "Generique")
                spriteBatch.Draw(generique.GetTexture(), new Rectangle(0, 0, 1280, 800), Color.Gray);

            if (GameState.State == "inGame" || GameState.State == "menuPause" || GameState.State == "GameOver"
                || GameState.State == "newGame" || GameState.State == "KeyBonusGame") //afficher jeu
                game.Draw(spriteBatch);

            if (GameState.State == "menuAide") //afficher menu pause
                menuaide.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuOptions") //afficher menu pause
                menuoptions.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuChoose") //afficher menu pause
                menuchoose.Draw(spriteBatch, gameTime);

            if (GameState.State == "GameOver") //afficher le pen
                gameover.Draw(spriteBatch);

            if (GameState.State == "newGame")
                pong.Draw(spriteBatch);

            if (GameState.State == "KeyBonusGame")
                keybonusgame.Draw(spriteBatch);

            if (GameState.State == "menuPause") //afficher menu pause
            {
                if (pong.startgame)
                    pong.Draw(spriteBatch);

                if (keybonusgame.startgame)
                    keybonusgame.Draw(spriteBatch);

                menupause.Draw(spriteBatch);
            }

            if (GameState.State == "menuPauseAide") //afficher menu pause
            {
                game.Draw(spriteBatch);
                menupauseaide.Draw(spriteBatch);
            }

            spriteBatch.End(); //FIN

            base.Draw(gameTime);
        }
    }
}
