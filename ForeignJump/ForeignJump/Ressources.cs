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
    public static class Ressources
    {
        static Niveau renoi;
        static Niveau roumain;
        //static Perso indien;
        //static Perso reunionnais;

        public static ContentManager Content;

        public static void Load()
        {
            #region renoi
            
            renoi = new Niveau();
            //menuChoose
            renoi.persoMenu = Content.Load<Texture2D>("Menu/Choose/renoi/perso");
            renoi.drapeauMenu = Content.Load<Texture2D>("Menu/Choose/renoi/drapeau");
            renoi.nameMenu = Content.Load<Texture2D>("Menu/Choose/renoi/name");
            renoi.description = "       Moussa, le renoi. \n \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con";

            //map
            renoi.objets = new Objet[300, 19];
            renoi.obstacle = Content.Load<Texture2D>("Perso/renoi/brique");
            renoi.nuage = Content.Load<Texture2D>("Perso/renoi/nuage");
            renoi.nulle = Content.Load<Texture2D>("Perso/renoi/rien");
            renoi.terre = Content.Load<Texture2D>("Perso/renoi/terre");
            renoi.terre1 = Content.Load<Texture2D>("Perso/renoi/terre1");
            renoi.terre2 = Content.Load<Texture2D>("Perso/renoi/terre3");
            renoi.sousterre = Content.Load<Texture2D>("Perso/renoi/terre2");
            renoi.piece = Content.Load<Texture2D>("Perso/renoi/piece");
            renoi.path = "map.txt";

            //hero
            renoi.heroTexture = Content.Load<Texture2D>("Perso/renoi/hero6");
            renoi.heroTextureAnime = Content.Load<Texture2D>("Perso/renoi/hero6anime");
            renoi.heroAnime = new Animate(renoi.heroTextureAnime, 1, 12);

            //game
            renoi.bg = Content.Load<Texture2D>("Perso/renoi/bg");
            renoi.barre = Content.Load<Texture2D>("Perso/renoi/barre");
            renoi.glass = Content.Load<Texture2D>("Perso/renoi/glass");
            renoi.font = Content.Load<SpriteFont>("Perso/renoi/Font");

            //ennemi
            renoi.texture = Content.Load<Texture2D>("Perso/renoi/voitureanime");
            renoi.ennemiAnime = new Animate(renoi.texture, 1, 4);

            #endregion

            #region Roumain
            roumain = new Niveau();
            //menuChoose
            roumain.persoMenu = Content.Load<Texture2D>("Menu/Choose/roumain/perso");
            roumain.drapeauMenu = Content.Load<Texture2D>("Menu/Choose/roumain/drapeau");
            roumain.nameMenu = Content.Load<Texture2D>("Menu/Choose/roumain/name");
            roumain.description = "       Andrei, le roumain. \n \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con";

            //map
            roumain.objets = new Objet[300, 19];
            roumain.obstacle = Content.Load<Texture2D>("Perso/roumain/brique");
            roumain.nuage = Content.Load<Texture2D>("Perso/roumain/nuage");
            roumain.nulle = Content.Load<Texture2D>("Perso/roumain/rien");
            roumain.terre = Content.Load<Texture2D>("Perso/roumain/terre");
            roumain.terre1 = Content.Load<Texture2D>("Perso/roumain/terre1");
            roumain.sousterre = Content.Load<Texture2D>("Perso/roumain/terre2");
            roumain.terre2 = Content.Load<Texture2D>("Perso/renoi/terre3");
            roumain.piece = Content.Load<Texture2D>("Perso/roumain/piece");
            roumain.path = "map2.txt";

            //hero
            roumain.heroTexture = Content.Load<Texture2D>("Perso/roumain/hero6");
            roumain.heroTextureAnime = Content.Load<Texture2D>("Perso/roumain/hero6anime");
            roumain.heroAnime = new Animate(roumain.heroTextureAnime, 1, 12);

            //game
            roumain.bg = Content.Load<Texture2D>("Perso/roumain/bg");
            roumain.barre = Content.Load<Texture2D>("Perso/roumain/barre");
            roumain.glass = Content.Load<Texture2D>("Perso/roumain/glass");
            roumain.font = Content.Load<SpriteFont>("Perso/roumain/Font");

            //ennemi
            roumain.texture = Content.Load<Texture2D>("Perso/roumain/voitureanime");
            roumain.ennemiAnime = new Animate(roumain.texture, 1, 4);
            #endregion
        }

        public static Niveau GetPerso(string perso)
        {
            if (perso == "renoi")
                return renoi;
            else
                return roumain;
            
        }
    }
}
