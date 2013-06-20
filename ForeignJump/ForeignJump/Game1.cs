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
        private MenuName menuname; //déclaration du menu de nom
        private Gameplay game; //déclaration du gameplay
        private GameOver gameover; //déclaration du gameover
        private Pong pong; //déclaration du popup du nouveau jeu
        private KeyBonusGame keybonusgame; //déclaration du popup du jeu de touches
        private MenuMode menumode;
        private MenuConnection menuconnection;
        private MultiGameplay multigame;
        private MultiMenuChoose multimenuchoose;
        private MultiGameOver multigameover;
        private MultiMenuPause multimenupause;
        private MultiGameWin multigamewin;

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

            multigame = new MultiGameplay();

            menupauseaide = new MenuPauseAide();

            menuaide = new MenuAide();
            menuaide.Initialize(); //initialisation menu aide

            menuchoose = new MenuChoose(game, Content);
            menuchoose.Initialize(); //initialisation menu options

            menuconnection = new MenuConnection(multigame);
            menuconnection.Initialize();

            multimenuchoose = new MultiMenuChoose(multigame, Components);
            multimenuchoose.Initialize();

            menuname = new MenuName();
            menuname.Initialize(); //initialisation menu options

            menuoptions = new MenuOptions(menu, menuaide, menuchoose);
            menuoptions.Initialize(); //initialisation menu options

            gameover = new GameOver();
            multigameover = new MultiGameOver();
            multigamewin = new MultiGameWin();

            pong = new Pong();
            pong.Initialize();

            keybonusgame = new KeyBonusGame();
            keybonusgame.Initialize();

            menupause = new MenuPause(pong, keybonusgame);
            menupause.Initialize(450, 0); //initialisation menu pause

            multimenupause = new MultiMenuPause();
            multimenupause.Initialize(450, 0); //initialisation menu pause

            menumode = new MenuMode();
            menumode.Initialize();

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
            multimenupause.LoadContent(Content);
            menuoptions.LoadContent(); //charger menu options
            menuchoose.LoadContent(); //charger menu choix de personnage
            multimenuchoose.LoadContent();
            menuconnection.LoadContent();
            menuname.LoadContent();
            menumode.LoadContent();
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
                if ((GameState.State == "GameOver" || GameState.State == "initial") && Reseau.session != null)
                {
                    menuconnection.Initialize();
                    try
                    {
                        Reseau.session = null;
                        Reseau.session.Dispose();
                        Reseau.session = null;
                        Reseau.asessions = null;
                    }
                    catch
                    {
                    }
                }
            }

            if (GameState.State == "Generique")
            {
                if (gameTime.TotalGameTime.Seconds < gen.Duration.Seconds && !(KB.New.IsKeyDown(Keys.Escape)))
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
                    GameState.State = "menuName";
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

            if (GameState.State == "multiInGame") //mise à jour game
            {
                multigame.Update(gameTime);
                MediaPlayer.Stop();
                play = false;
            }

            //menus
            if (GameState.State == "menuName") //mise à jour menu aide
                menuname.Update();

            if (GameState.State == "menuAide") //mise à jour menu aide
                menuaide.Update(gameTime, 5);

            if (GameState.State == "menuOptions") //mise à jour menu aide
                menuoptions.Update(gameTime, 5, graphics, multimenuchoose);

            if (GameState.State == "menuConnection") //mise à jour de la selection du mode
                menuconnection.Update();

            if (GameState.State == "menuChoose") //mise à jour menu aide
                menuchoose.Update(gameTime, 5);

            if (GameState.State == "multiMenuChoose") //mise à jour menu aide
                multimenuchoose.Update(gameTime, 5);

            if (GameState.State == "menuMode") //mise à jour de la selection du mode
                menumode.Update();

            if (GameState.State == "menuPause") //mise à jour menu pause
                menupause.Update(gameTime, 3);

            if (GameState.State == "multiMenuPause") //mise à jour menu pause
            {
                multigame.reseau.Update(gameTime);
                multimenupause.Update(gameTime, 3);
            }

            if (GameState.State == "menuPauseAide") //mise à jour menu pause aide
                menupauseaide.Update();

            //menus fin

            if (GameState.State == "GameOver") //game over
                gameover.Update();

            if (GameState.State == "multiGameOver") //game over
            {
                multigame.reseau.Update(gameTime);
                multigameover.Update();
            }

            if (GameState.State == "multiGameWin") //game over
            {
                multigame.reseau.Update(gameTime);
                multigamewin.Update();
            }

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

            if (GameState.State == "multiInGame")
                multigame.Draw(spriteBatch);

            if (GameState.State == "menuMode") //mise à jour de la selection du mode
                menumode.Draw(spriteBatch);

            if (GameState.State == "menuChoose") //afficher menu pause
                menuchoose.Draw(spriteBatch, gameTime);

            if (GameState.State == "multiMenuChoose") //mise à jour menu aide
                multimenuchoose.Draw(spriteBatch, gameTime);

            if (GameState.State == "menuConnection") //mise à jour de la selection du mode
                menuconnection.Draw(spriteBatch);

            if (GameState.State == "menuName") //afficher menu name
                menuname.Draw(spriteBatch);

            if (GameState.State == "GameOver") //afficher lepen
                gameover.Draw(spriteBatch);

            if (GameState.State == "multiGameOver") //game over
            {
                multigame.Draw(spriteBatch);
                multigameover.Draw(spriteBatch);
            }

            if (GameState.State == "multiGameWin") //game over
            {
                multigame.Draw(spriteBatch);
                multigamewin.Draw(spriteBatch);
            }
            
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

            if (GameState.State == "multiMenuPause") //afficher menu pause
            {
                multigame.Draw(spriteBatch);
                multimenupause.Draw(spriteBatch);
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
