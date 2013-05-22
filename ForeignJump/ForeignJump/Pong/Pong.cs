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
    class Pong
    {
        Random random; //random pour la direction de la balle

        Player player; //joueur
        PongObject balle;

        Texture2D balleTexture; //texture de la balle
        Texture2D pongBG;
        Texture2D pongStart;
        Texture2D pongBarre;
        Texture2D pongOver;

        Vector2 force;
        Vector2 vitesse;

        bool lost; //perdu
        public bool startgame; //jeu commencé?

        SpriteFont font;

        public int score; //score local

        public void Initialize()
        {
            vitesse = new Vector2(0, 0);
            force = new Vector2(0, 300);

            random = new Random();
            score = 0;
            lost = false;
            startgame = false;
        }

        public void LoadContent()
        {
            pongStart = Ressources.GetLangue(Langue.Choisie).pongStart;
            pongOver = Ressources.GetLangue(Langue.Choisie).pongOver;
            balleTexture = Ressources.Content.Load<Texture2D>("newGame/balle");
            pongBG = Ressources.Content.Load<Texture2D>("newGame/pongBG");
            pongBarre = Ressources.Content.Load<Texture2D>("newGame/pongBarre");
            
            player = new Player(Ressources.GetPerso(Perso.Choisi).name, pongBarre, new Vector2(467, 100 + 600 - pongBarre.Height));

            balle = new PongObject();
            balle.positionInitiale = new Vector2(1280 / 2 - balleTexture.Width / 2, 100 + 600 / 2 - balleTexture.Height);
            balle.position = balle.positionInitiale;
            balle.texture = balleTexture;
            balle.container = new Rectangle((int)balle.position.X, (int)balle.position.Y, balle.texture.Width, balle.texture.Height);

            font = Ressources.GetPerso(Perso.Choisi).font;
        }

        public void Update(GameTime gameTime, Gameplay game)
        {
            pongStart = Ressources.GetLangue(Langue.Choisie).pongStart;
            pongOver = Ressources.GetLangue(Langue.Choisie).pongOver;


            if (GameState.State == "newGame" && KB.New.IsKeyDown(Keys.Escape))
                GameState.State = "menuPause";

            if (!startgame)
            {
                if (KB.New.IsKeyDown(Keys.Enter))
                startgame = true;
            }
            else 
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Vector2 acceleration = force;

                if (!lost)
                {
                    #region Input
                    if (KB.New.IsKeyDown(Keys.Right) && player.position.X + player.texture.Width < 1082)
                        player.position.X += 8;

                    if (KB.New.IsKeyDown(Keys.Left) && player.position.X > 260)
                        player.position.X -= 8;
                    #endregion

                    balle.container = new Rectangle((int)balle.position.X, (int)balle.position.Y, balle.texture.Width, balle.texture.Height);

                    #region Collisions

                    if (balle.container.Intersects(player.container))
                    {
                        score++;
                        AudioRessources.wingold.Play(AudioRessources.volume, 0f, 0f);
                        vitesse.Y = random.Next(-1500, -1000);
                        vitesse.X = random.Next(-1100, 1100);
                    }

                    if (balle.position.Y <= 100)
                    {
                        balle.position.Y = 100;
                        vitesse.Y = random.Next(1000, 1500);
                        vitesse.X = random.Next(-1100, 1100);
                    }

                    if (balle.position.X >= 1040 - balle.texture.Width)
                    {
                        balle.position.X = 1040 - balle.texture.Width;
                        vitesse.X *= -1;
                        force.X *= -1;
                    }

                    if (balle.position.X <= 240)
                    {
                        balle.position.X = 240;
                        vitesse.X *= -1;
                        force.X *= -1;
                    }
                    #endregion

                    vitesse += acceleration * dt;
                    balle.position += vitesse * dt;

                    player.Update();
                }

                if (balle.position.Y > 800)
                    GameOver(game);
               
                if (KB.New.IsKeyDown(Keys.Enter) && lost)
                {
                    NewGame();
                    GameState.State = "inGame";
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!startgame)
            {
                spriteBatch.Draw(pongStart, new Rectangle(240, 100, 800, 600), Color.White);
            }
            else
            {
                spriteBatch.Draw(pongBG, new Rectangle(240, 100, 800, 600), Color.White);
                spriteBatch.Draw(pongBarre, player.container, Color.White);
                spriteBatch.Draw(balle.texture, balle.container, Color.White);
            }

            if (lost)
            {
                spriteBatch.Draw(pongOver, new Rectangle(440, 185, 400, 431), Color.White);
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(550, 300), Color.Black);
            }
        }

        public void NewGame()
        {
            vitesse = new Vector2(0, 0);
            force = new Vector2(0, 300);

            random = new Random();
            score = 0;
            lost = false;
            startgame = false;

            player = new Player(Ressources.GetPerso(Perso.Choisi).name, pongBarre, new Vector2(1280 / 2 - pongBarre.Width / 2, 100 + 600 - pongBarre.Height));

            balle = new PongObject();
            balle.positionInitiale = new Vector2(1280 / 2 - balleTexture.Width / 2, 100 + 600 / 2 - balleTexture.Height);
            balle.position = balle.positionInitiale;
            balle.texture = balleTexture;
            balle.container = new Rectangle((int)balle.position.X, (int)balle.position.Y, balle.texture.Width, balle.texture.Height);

            font = Ressources.GetPerso(Perso.Choisi).font;
        }

        public void GameOver(Gameplay game)
        {
            Statistiques.Score += score;
            lost = true;
            vitesse = new Vector2(0, 0);
            force = new Vector2(0, 0);
            balle.position = balle.positionInitiale;
        }
    }
}
