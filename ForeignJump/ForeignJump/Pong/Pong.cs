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
        Texture2D barreTexture; //texture de la barre
        Texture2D balleTexture; //texture de la balle

        PongObject balle; //balle

        Vector2 force; //force
        Vector2 vitesse;

        bool lost;

        SpriteFont font;

        public int score;

        public void Initialize()
        {
            vitesse = new Vector2(0, 0);
            force = new Vector2(0, 300);
            //poids = new Vector2(0, 300);

            random = new Random();
            score = 0;
            lost = false;
        }

        public void LoadContent(ContentManager Content)
        {
            barreTexture = Content.Load<Texture2D>("newGame/barre");
            balleTexture = Content.Load<Texture2D>("newGame/balle");

            player = new Player(Ressources.GetPerso(Perso.Choisi).name, barreTexture, new Vector2(1280 / 2 - barreTexture.Width / 2 - 40, 100 + 600 - barreTexture.Height));

            balle = new PongObject();
            balle.positionInitiale = new Vector2(1280 / 2 - balleTexture.Width / 2, 100 + 600 / 2 - balleTexture.Height);
            balle.position = balle.positionInitiale;
            balle.texture = balleTexture;
            balle.container = new Rectangle((int)balle.position.X, (int)balle.position.Y, balle.texture.Width, balle.texture.Height);

            font = Ressources.GetPerso(Perso.Choisi).font;
        }

        public void Update(GameTime gameTime, Gameplay game)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 acceleration = force;

            if (!lost)
            {
                #region Input
                if (KB.New.IsKeyDown(Keys.Right) && player.position.X < 1038 - player.texture.Width)
                    player.position.X += 8;

                if (KB.New.IsKeyDown(Keys.Left) && player.position.X > 244)
                    player.position.X -= 8;
                #endregion
            }

            balle.container = new Rectangle((int)balle.position.X, (int)balle.position.Y, balle.texture.Width, balle.texture.Height);

            #region Collisions

            if (balle.container.Intersects(player.container))
            {
                score++;
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

            if (balle.position.Y > 800)
            {
                GameOver(game);
            }

            if (KB.New.IsKeyDown(Keys.Enter) && lost)
            {
                NewGame();
                GameState.State = "inGame";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).bg, new Rectangle(240, 100, 800, 600), Color.Red);
            spriteBatch.Draw(player.texture, player.container, Color.White);
            spriteBatch.Draw(balle.texture, balle.container, Color.White);

            if (lost)
            {
                spriteBatch.Draw(Ressources.GetPerso(Perso.Choisi).persoMenu, new Rectangle(240, 100, 800, 600), Color.Blue);
                spriteBatch.DrawString(font, "Score Final: " + score, new Vector2(1280 / 2 - balleTexture.Width / 2, 100 + 600 / 2 - balleTexture.Height), Color.Red);
            }
        }

        public void NewGame()
        {

            vitesse = new Vector2(0, 0);
            force = new Vector2(0, 300);
            //poids = new Vector2(0, 300);

            random = new Random();
            score = 0;
            lost = false;

            player = new Player(Ressources.GetPerso(Perso.Choisi).name, barreTexture, new Vector2(1280 / 2 - barreTexture.Width / 2, 100 + 600 - barreTexture.Height));

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
