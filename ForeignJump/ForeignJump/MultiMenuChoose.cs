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
using System.Threading;

namespace ForeignJump
{
    class MultiMenuChoose
    {
        private Texture2D menubg; //bg
        private Texture2D drapeau; //drapeau
        private Texture2D perso; //personnage
        private Texture2D name; //nom
        private Texture2D liveLoading;
        private string description; //description du perso
        private SpriteFont fontmenuchoose; //font description

        private MultiGameplay game;

        private bool live;

        private int selection; //selection verticale

        GameComponentCollection Components;

        public MultiMenuChoose(MultiGameplay game, GameComponentCollection Components)
        {
            this.game = game;
            live = false;
            this.Components = Components;
        }

        public void Initialize()
        {
            //initialiser la selection à 1 sur renoi
            selection = 1;
            Perso.Choisi = "renoi";
        }

        public void LoadContent()
        {
            menubg = Ressources.GetLangue(Langue.Choisie).menuChoose;
            fontmenuchoose = Ressources.Pericles25;
            liveLoading = Ressources.GetLangue(Langue.Choisie).loading;
            drapeau = Ressources.GetPerso("renoi").drapeauMenu;
            perso = Ressources.GetPerso("renoi").persoMenu;
            name = Ressources.GetPerso("renoi").nameMenu;
            description = Ressources.GetPerso("renoi").description;
        }

        public void Update(GameTime gameTime, int vitesse)
        {
            if (!live)
            {
                Components.Add(new GamerServicesComponent(Ressources.Game));
                live = true;
            }
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
                GameState.State = "menuMode"; //retour au menu

            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
            {
                game.Initialize(); //charger game
                game.LoadContent();
                GameState.State = "menuConnection";
            }

            #region selection

            if (selection == 5)
                selection = 1;
            if (selection == 0)
                selection = 4;

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
            else if (selection == 2)
            {
                drapeau = Ressources.GetPerso("roumain").drapeauMenu;
                perso = Ressources.GetPerso("roumain").persoMenu;
                name = Ressources.GetPerso("roumain").nameMenu;
                description = Ressources.GetPerso("roumain").description;

                Perso.Choisi = "roumain";
            }

            else if (selection == 3)
            {
                drapeau = Ressources.GetPerso("reunionnais").drapeauMenu;
                perso = Ressources.GetPerso("reunionnais").persoMenu;
                name = Ressources.GetPerso("reunionnais").nameMenu;
                description = Ressources.GetPerso("reunionnais").description;

                Perso.Choisi = "reunionnais";
            }

            else if (selection == 4)
            {
                drapeau = Ressources.GetPerso("indien").drapeauMenu;
                perso = Ressources.GetPerso("indien").persoMenu;
                name = Ressources.GetPerso("indien").nameMenu;
                description = Ressources.GetPerso("indien").description;

                Perso.Choisi = "indien";
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Ressources.Game.Components.Count >= 1)
            {
                spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
                spriteBatch.Draw(drapeau, new Rectangle(900, 580, drapeau.Width, drapeau.Height), Color.White);
                spriteBatch.Draw(perso, new Rectangle(920, 145, perso.Width, perso.Height), Color.White);
                spriteBatch.Draw(name, new Rectangle(800, 57, name.Width, name.Height), Color.White);
                spriteBatch.DrawString(fontmenuchoose, description, new Vector2(150, 150), Color.White);
            }
            else
            {
                spriteBatch.Draw(liveLoading, new Rectangle(640 - liveLoading.Width, 400 - liveLoading.Height, liveLoading.Width, liveLoading.Height), Color.White);
            }
        }
    }
}