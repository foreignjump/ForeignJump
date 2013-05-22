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
    class MenuChoose
    {
        private Texture2D menubg; //bg
        private Texture2D drapeau; //drapeau
        private Texture2D perso; //personnage
        private Texture2D name; //nom
        private string description; //description du perso
        private SpriteFont fontmenuchoose; //font description

        private ContentManager Content;
        private Gameplay game;

        private int selection; //selection verticale

        public MenuChoose(Gameplay game, ContentManager Content)
        {
            this.game = game;
            this.Content = Content;
        }

        public void Initialize()
        {
            //initialiser la selection à 0 sur fullscreen
            selection = 1;
        }

        public void LoadContent()
        {
            menubg = Ressources.GetLangue(Langue.Choisie).menuChoose;
            fontmenuchoose = Content.Load<SpriteFont>("Menu/Choose/FontMenuChoose");
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
                GameState.State = "initial"; //retour au menu

            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
            {
                game.Initialize(); //charger game
                game.LoadContent();
                GameState.State = "inGame";
            }

            #region selection

            if (selection == -1) //pour que la selection ne dépasse pas les negatifs
                selection = 1;
            else
                selection = selection % 2; //pour que la selection ne dépasse pas 2

            if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right))
                selection++;

            if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left))
                selection--;

            #endregion

            if (selection == 1)
            {
                drapeau = Ressources.GetPerso("renoi").drapeauMenu;
                perso = Ressources.GetPerso("renoi").persoMenu;
                name = Ressources.GetPerso("renoi").nameMenu;
                description = Ressources.GetPerso("renoi").description;

                Perso.Choisi = "renoi";
            }
            else
            {
                drapeau = Ressources.GetPerso("roumain").drapeauMenu;
                perso = Ressources.GetPerso("roumain").persoMenu;
                name = Ressources.GetPerso("roumain").nameMenu;
                description = Ressources.GetPerso("roumain").description;

                Perso.Choisi = "roumain";
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
            spriteBatch.Draw(drapeau, new Rectangle(900, 580, drapeau.Width, drapeau.Height), Color.White);
            spriteBatch.Draw(perso, new Rectangle(920, 145, perso.Width, perso.Height), Color.White);
            spriteBatch.Draw(name, new Rectangle(800, 57, name.Width, name.Height), Color.White);
            spriteBatch.DrawString(fontmenuchoose, description, new Vector2(150, 150), Color.White);
        }
    }
}