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
    class MultiGameplay
    {
        public SpriteFont font;
        public static Map map;
        public static Hero hero;
        public static MultiCamera camera;
      
        #region Barre distance Textures
        private Texture2D barregreenleft;
        private Texture2D barregreencenter;
        private Texture2D barregreenright;
        private Texture2D barreredleft;
        private Texture2D barreredcenter;
        private Texture2D barreredright;
        #endregion
        
        public Reseau reseau;
        float a;
        float b;
        int nb1 = 0;
        int nb2 = 0;
        float time1;
        float time2;

        public static float speed;
 
        GameTime time;

        
        public MultiGameplay()
        {

        }
        public void Initialize()
        {
            map = new Map("mapMulti.txt");
            hero = new Hero(Ressources.GetPerso(Perso.Choisi).heroAnime, new Vector2(450, 500), new Vector2(200, 0), 90, map);
            camera = new MultiCamera(map, hero);
            reseau = new Reseau();
        }
        public void LoadContent()
        {
            font = Ressources.GetPerso(Perso.Choisi).font;
            barregreencenter = Ressources.GetPerso(Perso.Choisi).barregreencenter;
            barregreenleft = Ressources.GetPerso(Perso.Choisi).barregreenleft;
            barregreenright = Ressources.GetPerso(Perso.Choisi).barregreenright;
            barreredcenter = Ressources.GetPerso(Perso.Choisi).barreredcenter;
            barreredleft = Ressources.GetPerso(Perso.Choisi).barreredleft;
            barreredright = Ressources.GetPerso(Perso.Choisi).barreredright;
            map.Load();
        }
        public void Update(GameTime gameTime)
        {
            if (GameState.State == "multiInGame" && KB.New.IsKeyDown(Keys.Escape))
                GameState.State = "multiMenuPause";
            
            time = gameTime;
            
            camera.Position = new Vector2(hero.positionGlobale.X - 500, camera.Position.Y);
            hero.positionLocale.X = hero.positionGlobale.X - camera.Position.X + hero.positionInitiale.X;
            hero.positionLocale.Y = hero.positionGlobale.Y;
          
            #region code de sale chien
            if (nb1 == 1)
            {
                a = hero.positionGlobale.X;
                time1 = gameTime.TotalGameTime.Milliseconds;
            }
            if (nb2 == 2)
            {
                b = hero.positionGlobale.X;
                time2 = gameTime.TotalGameTime.Milliseconds;
                speed = Math.Abs(((b - a) / (time2 - time1))) * 2f;
                nb1 = 0;
                nb2 = 0;
            }
            nb1++;
            nb2++;
            if (speed > 0.99f)
                speed = 0.3f;
            #endregion
            
            hero.Update(gameTime, speed);
            reseau.Update(gameTime);

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            camera.Draw(spriteBatch);

            if (Langue.Choisie == "fr")
                spriteBatch.DrawString(font, "Nombre de Pieces :" + Convert.ToString(Statistiques.Score), new Vector2(60, 100), Color.Black);
            else
                spriteBatch.DrawString(font, "Gold :" + Convert.ToString(Statistiques.Score), new Vector2(60, 100), Color.Black);


       /*     //afficher la barre en rouge si distance en danger
            if (distance < 80)
            {
                spriteBatch.Draw(barreredleft, new Rectangle(60, 30, barreredleft.Width, barreredleft.Height), Color.White);
                spriteBatch.Draw(barreredcenter, new Rectangle(60 + barreredleft.Width, 30, (int)distance + 10, barreredcenter.Height), Color.White);
                spriteBatch.Draw(barreredright, new Rectangle((int)distance + 10 + 60 + barreredleft.Width, 30, barreredright.Width, barreredright.Height), Color.White);
            }
            else //afficher la barre en vert sinon
            {
                spriteBatch.Draw(barregreenleft, new Rectangle(60, 30, barregreenleft.Width, barregreenleft.Height), Color.White);
                spriteBatch.Draw(barregreencenter, new Rectangle(60 + barregreenleft.Width, 30, (int)distance + 10, barregreencenter.Height), Color.White);
                spriteBatch.Draw(barregreenright, new Rectangle((int)distance + +10 + 60 + barregreenleft.Width, 30, barregreenright.Width, barregreenright.Height), Color.White);
            }
         
        */
            reseau.Draw(time, spriteBatch);
        }
    }
}