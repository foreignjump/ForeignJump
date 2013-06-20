using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
namespace ForeignJump
{
    class Reseau
    {
        public static NetworkSession session;
        public static AvailableNetworkSessionCollection asessions; //*****************************Permet de chercher toute les sessions disponible*************************
        PacketReader reader;// classe permettant de recevoir des données
        PacketWriter writer;// classe permettant d'envoyer des données
        // ******************Syteme permetant d'envoyer une seul donnée a la fois****************************
        bool send = true;
        bool send1 = true;
        // *************************************************************************************************
        string position = "";
        float x1;
        float y1;
        public static int pieceserveur = 0;
        public static string NameServeur;
        public static int piececlient = 0;
        public static string NameClient;
        
        //joueur adverse
        Player2 player2;
        string perso;
        string jump;
        bool jumpBool;
        string down;
        bool downBool;
        bool setPerso;
        string finalPerso;

        public Reseau()
        {
            downBool = false;
            jumpBool = false;
            setPerso = false;
            perso = "renoi";
            // Receive/Send
            reader = new PacketReader();
            writer = new PacketWriter();
            player2 = new Player2(Ressources.GetPerso(perso).heroTexture, Ressources.GetPerso(perso).heroTextureAnime, Vector2.Zero);
        }
        public void Update(GameTime gameTime)
        {
            try
            {
                #region Envoie coord
                if (session != null)
                {
                    if (session.IsHost)
                    {
                        if (session != null && send == true) //Si serveur**
                        {
                            if (GameState.State == "GameWin")
                            {
                                writer.Write("server" + "Win" + "|" + Statistiques.Name + "|" + Statistiques.Score + "|");
                            }
                            else if (GameState.State == "multiGameOver")
                            {
                                writer.Write("server" + "over");
                            }
                            else if (GameState.State == "multiMenuPause")
                            {
                                writer.Write("server" + "pause");
                            }
                            else
                            {
                                writer.Write("server" + MultiGameplay.hero.positionGlobale.X + "|" + MultiGameplay.hero.positionGlobale.Y + "|" + Perso.Choisi + "|" + MultiGameplay.hero.animate + "|" + MultiGameplay.hero.down + "|"); //***Envoie sous forme de String la position***
                            }
                            session.LocalGamers[0].SendData(writer, SendDataOptions.None);
                            send = false;
                        }
                    }
                    else
                    {
                        if (session != null && send1 == true) // si client***
                        {
                            if (GameState.State == "GameWin")
                            {
                                writer.Write("client" + "Win" + "|" + Statistiques.Name + "|" + Statistiques.Score + "|");
                            }
                            else if (GameState.State == "multiGameOver")
                            {
                                writer.Write("client" + "over");
                            }
                            else if (GameState.State == "multiMenuPause")
                            {
                                writer.Write("client" + "pause");
                            }
                            else
                            {
                                writer.Write("client" + MultiGameplay.hero.positionGlobale.X + "|" + MultiGameplay.hero.positionGlobale.Y + "|" + Perso.Choisi + "|" + MultiGameplay.hero.animate + "|" + MultiGameplay.hero.down + "|");
                            }
                            session.LocalGamers[0].SendData(writer, SendDataOptions.None);
                            send1 = false;
                        }
                    }
                }
                #endregion
                
                if (session != null)
                    session.Update();// !!!!!!!!!!!!!!!!!!!!!!!Ne pas changer la position!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                string x = "0";
                string y = "0";
                jump = "";
                perso = "";
                down = "";

                #region Receive
                if (session != null)
                {
                    if (session.IsHost)
                    {
                        LocalNetworkGamer gamer;
                        if ((gamer = session.LocalGamers[0]).IsDataAvailable)
                        {
                            NetworkGamer sender;
                            gamer.ReceiveData(reader, out sender);
                            position = reader.ReadString();
                        }
                        if (position[0] == 'c' && position[1] == 'l' && position[2] == 'i' && position[3] == 'e' && position[4] == 'n' && position[5] == 't' &&
                        position[6] == 'W' && position[7] == 'i' && position[8] == 'n')
                        {
                            send = true;
                            GameState.State = "multiGameOver";
                            NameServeur = "";
                            pieceserveur = 0;
                            for (int i = 10; i < position.Length; i++)
                            {
                                string num = "";
                                while (position[i] != '|')
                                {
                                    NameServeur = NameServeur + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    num = num + position[i];
                                    i++;
                                }
                                i++;
                                pieceserveur = Int32.Parse(num);
                            }

                        }
                        else if (position[0] == 'c' && position[1] == 'l' && position[2] == 'i' && position[3] == 'e' && position[4] == 'n' && position[5] == 't' &&
                        position[6] == 'o' && position[7] == 'v' && position[8] == 'e' && position[9] == 'r')
                        {
                            send = true;
                            GameState.State = "multiGameWin";
                            NameServeur = "";
                            pieceserveur = 0;
                            for (int i = 11; i < position.Length; i++)
                            {
                                string num = "";
                                while (position[i] != '|')
                                {
                                    NameServeur = NameServeur + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    num = num + position[i];
                                    i++;
                                }
                                i++;
                                pieceserveur = Int32.Parse(num);
                            }

                        }
                        else if (position == "clientpause")
                        {
                            send = true;
                        }
                        else if (position[0] == 'c' && position[1] == 'l' && position[2] == 'i' && position[3] == 'e' && position[4] == 'n' && position[5] == 't')
                        {
                            for (int i = 6; i < position.Length; i++)
                            {
                                while (position[i] != '|')
                                {
                                    x = x + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    y = y + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    perso = perso + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    jump = jump + position[i];
                                    i++;
                                }
                                if (jumpBool != Convert.ToBoolean(jump))
                                    jumpBool = Convert.ToBoolean(jump);
                                i++;
                                while (position[i] != '|')
                                {
                                    down = down + position[i];
                                    i++;
                                }
                                if (downBool != Convert.ToBoolean(down))
                                    downBool = Convert.ToBoolean(down);
                                send = true;
                            }
                        }
                    }
                    else
                    {
                        LocalNetworkGamer gamer;
                        if ((gamer = session.LocalGamers[0]).IsDataAvailable)
                        {
                            NetworkGamer sender;
                            gamer.ReceiveData(reader, out sender);
                            position = reader.ReadString();
                        }
                        if (position[0] == 's' && position[1] == 'e' && position[2] == 'r' && position[3] == 'v' && position[4] == 'e' && position[5] == 'r' &&
                        position[6] == 'W' && position[7] == 'i' && position[8] == 'n')
                        {
                            send1 = true;
                            GameState.State = "multiGameOver";
                            piececlient = 0;
                            NameClient = "";
                            for (int i = 10; i < position.Length; i++)
                            {
                                string num = "";
                                while (position[i] != '|')
                                {
                                    NameClient = NameClient + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    num = num + position[i];
                                    i++;
                                }
                                i++;
                                piececlient = Int32.Parse(num);
                            }
                        }
                        if (position[0] == 's' && position[1] == 'e' && position[2] == 'r' && position[3] == 'v' && position[4] == 'e' && position[5] == 'r' &&
                        position[6] == 'o' && position[7] == 'v' && position[8] == 'e' && position[9] == 'r')
                        {
                            send1 = true;
                            GameState.State = "multiGameWin";
                            for (int i = 11; i < position.Length; i++)
                            {
                                string num = "";
                                NameClient = "";
                                while (position[i] != '|')
                                {
                                    NameClient = NameClient + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    num = num + position[i];
                                    i++;
                                }
                                i++;
                                piececlient = Int32.Parse(num);
                            }
                        }
                        else if (position == "serverpause")
                        {
                            send1 = true;
                        }
                        else if (position[0] == 's' && position[1] == 'e' && position[2] == 'r' && position[3] == 'v' && position[4] == 'e' && position[5] == 'r')
                        {
                            for (int i = 6; i < position.Length; i++)
                            {
                                while (position[i] != '|')
                                {
                                    x = x + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    y = y + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    perso = perso + position[i];
                                    i++;
                                }
                                i++;
                                while (position[i] != '|')
                                {
                                    jump = jump + position[i];
                                    i++;
                                }
                                if (jumpBool != Convert.ToBoolean(jump))
                                    jumpBool = Convert.ToBoolean(jump);
                                i++;
                                while (position[i] != '|')
                                {
                                    down = down + position[i];
                                    i++;
                                }
                                if (downBool != Convert.ToBoolean(down))
                                    downBool = Convert.ToBoolean(down);
                            }
                            send1 = true;
                        }
                    }
                    if (!setPerso && perso != "")
                    {
                        finalPerso = perso;
                        player2.animation = Ressources.GetPerso(finalPerso.ToLower()).heroTextureAnime;
                        player2.statique = Ressources.GetPerso(finalPerso.ToLower()).heroTexture;
                        setPerso = true;
                    }
                    player2.Update();
                    #region fixe personnage
                    if ((float.Parse(x)) != 0 && (float.Parse(y)) != 0) // Eviter que le personnage se retrouve au coord (0,0)************
                    {
                        x1 = (float.Parse(x));
                        y1 = (float.Parse(y));
                    }
                    else
                    {
                        // x1++;
                    }
                    player2.positionGlobale.X = x1;
                    player2.positionGlobale.Y = y1;
                    #endregion
                }
                #endregion
            }
            catch
            {
                try
                {
                    Reseau.session = NetworkSession.Join(Reseau.asessions[0]);
                }
                catch
                { }
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            #region si_connexion
            if (session != null)
            {
                if (downBool)
                {
                    spriteBatch.Draw(Ressources.GetPerso(finalPerso.ToLower()).heroTextureDown, new Rectangle((int)player2.positionGlobale.X - (int)MultiGameplay.camera.Position.X, (int)player2.positionGlobale.Y + Ressources.GetPerso(perso).heroTexture.Height - Ressources.GetPerso(perso).heroTextureDown.Height, Ressources.GetPerso(perso).heroTextureDown.Width, Ressources.GetPerso(perso).heroTextureDown.Height), Color.White);
                }
                else
                {
                    if (!jumpBool)
                    {
                        spriteBatch.Draw(player2.statique, new Rectangle((int)player2.positionGlobale.X - (int)MultiGameplay.camera.Position.X, (int)player2.positionGlobale.Y, player2.statique.Width, player2.statique.Height), Color.White);
                    }
                    else
                    {
                        player2.animate.Draw(spriteBatch, new Vector2(player2.positionGlobale.X - 14f - MultiGameplay.camera.Position.X, player2.positionGlobale.Y - 14f), 3);
                    }
                }
            }
            #endregion
        }
    }
}