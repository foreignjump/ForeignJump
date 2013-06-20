using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Configuration;
using System.ComponentModel;
using TweetSharp;
using System.Threading;

namespace ForeignJump
{
    class MultiGameWin
    {
        public static void Die() //fonction appliquée avant de rentrer dans le GameOver State
        {

        }


        public MultiGameWin()
        {
            /*
            #region Authentification Twitter

            TwitterClientInfo twitterClientInfo = new TwitterClientInfo();

            twitterClientInfo.ConsumerKey = "TF30IyeUwXRm1Xe38Qig"; //Read ConsumerKey out of the app.config

            twitterClientInfo.ConsumerSecret = "2gUJrzq3JtSSFLkwwaflRaFKxsYvCsDHu1WikEBRcM"; //Read the ConsumerSecret out the app.config

            twitterService = new TwitterService(twitterClientInfo);

            twitterService.AuthenticateWith("1515530498-gZaKFgEILJiF4rfFsbyURF1adSv6CpuqLzrZ9AO", "a0TyYuTBEqc68uq1GiaSCCGICc5MODIlABj0JvyM");
            #endregion
             */
        }

        public void Update()
        {
            if (KB.New.IsKeyDown(Keys.Enter) && !KB.Old.IsKeyDown(Keys.Enter))
            {
                /*
                //mise à jour du status en fonction du score
                string status;
                
                //utilisation d'un nouveau thread 
                ThreadStart threadStarter = delegate
                {
                    //twitter
                    twitterService.SendTweet(new SendTweetOptions { Status = status });
                };
                Thread loadingThread = new Thread(threadStarter);
                loadingThread.Start();
                */

                Statistiques.Score = 0;
                GameState.State = "initial";
                Reseau.session.Dispose();
                Reseau.session = null;
                Reseau.asessions = null;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.Content.Load<Texture2D>("GameWin"), new Rectangle(440, 185, 400, 431), Color.White);
        }
    }
}