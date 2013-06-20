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
using Microsoft.Xna.Framework.Net;

namespace ForeignJump
{
    class MenuConnection
    {
        private Texture2D menubg; //bg

        #region what to do
        private Texture2D createN;
        private Texture2D createH;
        private Texture2D create;

        private Texture2D joinN;
        private Texture2D joinH;
        private Texture2D join;
        #endregion

        private int selection; //selection verticale
        MultiGameplay game;
        SpriteFont font;

        string joindre;
        string creer;

        public MenuConnection(MultiGameplay game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            selection = 0;

            if (Langue.Choisie == "en")
            {
                creer = "Press SPACE \n to create a game.";
                joindre = "Press SPACE \n to join a game.";
            }
            else
            {
                creer = "Appuyez sur ESPACE \n pour creer une partie.";
                joindre = "Appuyez sur ESPACE \n pour joindre une partie.";
            }
        }

        public void LoadContent()
        {
            menubg = Ressources.Content.Load<Texture2D>("Menu/Connection/menuConnection");

            font = Ressources.Pericles25;

            createH = Ressources.GetLangue(Perso.Choisi).createH;
            createN = Ressources.GetLangue(Perso.Choisi).createN;
            create = createH;

            joinH = Ressources.GetLangue(Perso.Choisi).joinH;
            joinN = Ressources.GetLangue(Perso.Choisi).joinN;
            join = joinN;
        }

        public void Update()
        {
            if (KB.New.IsKeyDown(Keys.Escape) && !KB.Old.IsKeyDown(Keys.Escape))
                GameState.State = "menuMode"; //retour au menu

            #region selection

            if (selection == -1)
                selection = 1;
            if (selection == 2)
                selection = 0;

            if (KB.New.IsKeyDown(Keys.Right) && !KB.Old.IsKeyDown(Keys.Right))
                selection++;

            if (KB.New.IsKeyDown(Keys.Left) && !KB.Old.IsKeyDown(Keys.Left))
                selection--;

            if (selection == 0)
                create = createH;
            else
                create = createN;

            if (selection == 1)
                join = joinH;
            else
                join = joinN;

            #endregion

            try
            {
                if (SignedInGamer.SignedInGamers.Count != 0) // Si il y a un compte connecté ou créer soit un serveur soit un client
                {
                    if (selection == 0 && KB.New.IsKeyDown(Keys.Space) && Reseau.session == null)
                    {
                        Reseau.session = NetworkSession.Create(NetworkSessionType.SystemLink, 3, 3);  // Serveur
                    }
                    if (selection == 1 && KB.New.IsKeyDown(Keys.Space) && Reseau.session == null)
                    {
                        Reseau.asessions = NetworkSession.Find(NetworkSessionType.SystemLink, 3, null); //Cherche les serveurs disponibles
                    }
                    if (selection == 1 && Reseau.session == null && Reseau.asessions != null && Reseau.asessions.Count != 0)
                    {
                        Reseau.session = NetworkSession.Join(Reseau.asessions[0]); // Rejoint le premier serveur******
                    }
                }

                if (Reseau.session != null)
                {
                    Reseau.session.Update();

                    if (Reseau.session.IsHost)
                    {
                        if (Langue.Choisie == "en")
                        creer = "Game created, waiting \n    for other players...";
                        else
                            creer = "Partie cree, attente du joueur...";
                    }

                    if (Reseau.session.AllGamers.Count == 2) //&& Reseau.start == true) ;
                        GameState.State = "multiInGame";
                }
            }
            catch
            {
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menubg, new Rectangle(0, 0, 1280, 800), Color.White);
            spriteBatch.Draw(create, new Rectangle(210, 380, create.Width, create.Height), Color.White);
            spriteBatch.Draw(join, new Rectangle(850, 380, join.Width, join.Height), Color.White);

            spriteBatch.DrawString(font, creer , new Vector2(100, 500), Color.Black);
            spriteBatch.DrawString(font, joindre, new Vector2(740, 500), Color.Black);
        }
    }
}
