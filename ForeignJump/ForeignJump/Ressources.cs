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
        //static Niveau indien;
        static Niveau reunionnais;

        static Langue fr;
        static Langue en;

        public static ContentManager Content;

        public static Game Game;

        public static void LoadPerso()
        {
            #region renoi

            renoi = new Niveau();
            //menuChoose
            renoi.persoMenu = Content.Load<Texture2D>("Menu/Choose/renoi/perso");
            renoi.drapeauMenu = Content.Load<Texture2D>("Menu/Choose/renoi/drapeau");
            renoi.nameMenu = Content.Load<Texture2D>("Menu/Choose/renoi/name");
            renoi.description = "       Moussa, le renoi. \n \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con";

            //map
            renoi.objets = new Objet[1000, 19];
            renoi.obstacle = Content.Load<Texture2D>("Perso/renoi/brique");
            renoi.nulle = Content.Load<Texture2D>("Perso/renoi/rien");
            renoi.terre = Content.Load<Texture2D>("Perso/renoi/terre");
            renoi.terre1 = Content.Load<Texture2D>("Perso/renoi/terre1");
            renoi.terre2 = Content.Load<Texture2D>("Perso/renoi/terre3");
            renoi.sousterre = Content.Load<Texture2D>("Perso/renoi/terre2");
            renoi.piece = Content.Load<Texture2D>("Perso/renoi/piece");
            renoi.bombe = Content.Load<Texture2D>("Perso/renoi/bombe");
            renoi.bonus = Content.Load<Texture2D>("Perso/renoi/bonus");
            renoi.path = "map.txt";

            //hero
            renoi.heroTexture = Content.Load<Texture2D>("Perso/renoi/hero");
            renoi.heroTextureAnime = Content.Load<Texture2D>("Perso/renoi/heroAnime");
            renoi.heroTextureDown = Content.Load<Texture2D>("Perso/renoi/heroDown");
            renoi.heroAnime = new Animate(renoi.heroTextureAnime, 1, 12);
            renoi.name = "Moussa";

            //game
            renoi.bg = Content.Load<Texture2D>("Perso/renoi/bg");
            renoi.barregreenleft = Content.Load<Texture2D>("Perso/renoi/barregreenleft");
            renoi.barregreencenter = Content.Load<Texture2D>("Perso/renoi/barregreencenter");
            renoi.barregreenright = Content.Load<Texture2D>("Perso/renoi/barregreenright");
            renoi.barreredleft = Content.Load<Texture2D>("Perso/renoi/barreredleft");
            renoi.barreredcenter = Content.Load<Texture2D>("Perso/renoi/barreredcenter");
            renoi.barreredright = Content.Load<Texture2D>("Perso/renoi/barreredright");
            renoi.font = Content.Load<SpriteFont>("Perso/renoi/Font");

            //ennemi
            renoi.ennemiTexture = Content.Load<Texture2D>("Perso/renoi/ennemiTexture");
            renoi.ennemiTextureAnime = Content.Load<Texture2D>("Perso/renoi/ennemiTextureAnime");
            renoi.ennemiAnime = new Animate(renoi.ennemiTextureAnime, 1, 12);

            #endregion

            #region Roumain
            roumain = new Niveau();
            //menuChoose
            roumain.persoMenu = Content.Load<Texture2D>("Menu/Choose/roumain/perso");
            roumain.drapeauMenu = Content.Load<Texture2D>("Menu/Choose/roumain/drapeau");
            roumain.nameMenu = Content.Load<Texture2D>("Menu/Choose/roumain/name");
            roumain.description = "       Andrei, le roumain. \n \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con";

            //map
            roumain.objets = new Objet[1000, 19];
            roumain.obstacle = Content.Load<Texture2D>("Perso/roumain/brique");
            roumain.nulle = Content.Load<Texture2D>("Perso/roumain/rien");
            roumain.terre = Content.Load<Texture2D>("Perso/roumain/terre");
            roumain.terre1 = Content.Load<Texture2D>("Perso/roumain/terre1");
            roumain.sousterre = Content.Load<Texture2D>("Perso/roumain/terre2");
            roumain.terre2 = Content.Load<Texture2D>("Perso/roumain/terre3");
            roumain.piece = Content.Load<Texture2D>("Perso/roumain/piece");
            roumain.bombe = Content.Load<Texture2D>("Perso/roumain/bombe");
            roumain.bonus = Content.Load<Texture2D>("Perso/roumain/bonus");
            roumain.path = "map2.txt";

            //hero
            roumain.heroTexture = Content.Load<Texture2D>("Perso/roumain/hero");
            roumain.heroTextureAnime = Content.Load<Texture2D>("Perso/roumain/heroAnime");
            roumain.heroTextureDown = Content.Load<Texture2D>("Perso/roumain/heroDown");
            roumain.heroAnime = new Animate(roumain.heroTextureAnime, 1, 12);
            roumain.name = "Andrei";

            //game
            roumain.bg = Content.Load<Texture2D>("Perso/roumain/bg");
            roumain.barregreenleft = Content.Load<Texture2D>("Perso/roumain/barregreenleft");
            roumain.barregreencenter = Content.Load<Texture2D>("Perso/roumain/barregreencenter");
            roumain.barregreenright = Content.Load<Texture2D>("Perso/roumain/barregreenright");
            roumain.barreredleft = Content.Load<Texture2D>("Perso/roumain/barreredleft");
            roumain.barreredcenter = Content.Load<Texture2D>("Perso/roumain/barreredcenter");
            roumain.barreredright = Content.Load<Texture2D>("Perso/roumain/barreredright");
            roumain.font = Content.Load<SpriteFont>("Perso/roumain/Font");

            //ennemi
            roumain.ennemiTexture = Content.Load<Texture2D>("Perso/roumain/ennemiTexture");
            roumain.ennemiTextureAnime = Content.Load<Texture2D>("Perso/roumain/ennemiTextureAnime");
            roumain.ennemiAnime = new Animate(roumain.ennemiTextureAnime, 1, 12);

            #endregion

            #region reunionnais

            reunionnais = new Niveau();
            //menuChoose
            reunionnais.persoMenu = Content.Load<Texture2D>("Menu/Choose/reunionnais/perso");
            reunionnais.drapeauMenu = Content.Load<Texture2D>("Menu/Choose/reunionnais/drapeau");
            reunionnais.nameMenu = Content.Load<Texture2D>("Menu/Choose/reunionnais/name");
            reunionnais.description = "       Moussa, le reunionnais. \n \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con \n    Donec urna sem, semper \n ut bibendum in, malesuada \n sit amet diam. Vivamus id  \n convallis mi. Mauris con";

            //map
            reunionnais.objets = new Objet[1000, 19];
            reunionnais.obstacle = Content.Load<Texture2D>("Perso/reunionnais/brique");
            reunionnais.nulle = Content.Load<Texture2D>("Perso/reunionnais/rien");
            reunionnais.terre = Content.Load<Texture2D>("Perso/reunionnais/terre");
            reunionnais.terre1 = Content.Load<Texture2D>("Perso/reunionnais/terre1");
            reunionnais.terre2 = Content.Load<Texture2D>("Perso/reunionnais/terre3");
            reunionnais.sousterre = Content.Load<Texture2D>("Perso/reunionnais/terre2");
            reunionnais.piece = Content.Load<Texture2D>("Perso/reunionnais/piece");
            reunionnais.bombe = Content.Load<Texture2D>("Perso/reunionnais/bombe");
            reunionnais.bonus = Content.Load<Texture2D>("Perso/reunionnais/bonus");
            reunionnais.path = "map.txt";

            //hero
            reunionnais.heroTexture = Content.Load<Texture2D>("Perso/reunionnais/hero");
            reunionnais.heroTextureAnime = Content.Load<Texture2D>("Perso/reunionnais/heroAnime");
            reunionnais.heroTextureDown = Content.Load<Texture2D>("Perso/reunionnais/heroDown");
            reunionnais.heroAnime = new Animate(reunionnais.heroTextureAnime, 1, 12);
            reunionnais.name = "Matys";

            //game
            reunionnais.bg = Content.Load<Texture2D>("Perso/reunionnais/bg");
            reunionnais.barregreenleft = Content.Load<Texture2D>("Perso/reunionnais/barregreenleft");
            reunionnais.barregreencenter = Content.Load<Texture2D>("Perso/reunionnais/barregreencenter");
            reunionnais.barregreenright = Content.Load<Texture2D>("Perso/reunionnais/barregreenright");
            reunionnais.barreredleft = Content.Load<Texture2D>("Perso/reunionnais/barreredleft");
            reunionnais.barreredcenter = Content.Load<Texture2D>("Perso/reunionnais/barreredcenter");
            reunionnais.barreredright = Content.Load<Texture2D>("Perso/reunionnais/barreredright");
            reunionnais.font = Content.Load<SpriteFont>("Perso/reunionnais/Font");

            //ennemi
            reunionnais.ennemiTexture = Content.Load<Texture2D>("Perso/reunionnais/ennemiTexture");
            reunionnais.ennemiTextureAnime = Content.Load<Texture2D>("Perso/reunionnais/ennemiTextureAnime");
            reunionnais.ennemiAnime = new Animate(reunionnais.ennemiTextureAnime, 1, 12);

            #endregion
        }

        public static void LoadLangue()
        {
            #region en
            en = new Langue();

            //menu buttons
            en.buttonTextureStartH = Content.Load<Texture2D>("Langue/en/Menu/ButtonStartH");
            en.buttonTextureStartI = Content.Load<Texture2D>("Langue/en/Menu/ButtonStart");

            en.buttonTextureOptionsH = Content.Load<Texture2D>("Langue/en/Menu/ButtonOptionsH");
            en.buttonTextureOptionsI = Content.Load<Texture2D>("Langue/en/Menu/ButtonOptions");

            en.buttonTextureHelpH = Content.Load<Texture2D>("Langue/en/Menu/ButtonHelpH");
            en.buttonTextureHelpI = Content.Load<Texture2D>("Langue/en/Menu/ButtonHelp");

            en.buttonTextureExitH = Content.Load<Texture2D>("Langue/en/Menu/ButtonExitH");
            en.buttonTextureExitI = Content.Load<Texture2D>("Langue/en/Menu/ButtonExit");

        //menu
            en.menuAide = Content.Load<Texture2D>("Langue/en/Menu/menuAide");
            en.menuChoose = Content.Load<Texture2D>("Langue/en/Menu/MenuChoose");

        //menuChoose
            en.gameOver = Content.Load<Texture2D>("Langue/en/GameOver");
        
        //menuOptions
            en.menuOptions = Content.Load<Texture2D>("Langue/en/Menu/menuOptions");
            en.fullscreenH = Content.Load<Texture2D>("Langue/en/Menu/fullscreenH");
            en.fullscreenN = Content.Load<Texture2D>("Langue/en/Menu/fullscreenN");
            en.soundH = Content.Load<Texture2D>("Langue/en/Menu/soundH");
            en.soundN = Content.Load<Texture2D>("Langue/en/Menu/soundN");
            en.langueH = Content.Load<Texture2D>("Langue/en/Menu/langueH");
            en.langueN = Content.Load<Texture2D>("Langue/en/Menu/langueN");
            en.nomH = Content.Load<Texture2D>("Langue/en/Menu/nomH");
            en.nomN = Content.Load<Texture2D>("Langue/en/Menu/nomN");
            en.nomButton = Content.Load<Texture2D>("Langue/en/Menu/nomButton");

        //menuPause
            en.buttonTextureMenuH = Content.Load<Texture2D>("Langue/en/Menu/ButtonMenuH");
            en.buttonTextureMenuI = Content.Load<Texture2D>("Langue/en/Menu/ButtonMenu");

            en.menuPauseAide = Content.Load<Texture2D>("Langue/en/Menu/MenuPauseAide");

        //keys bonus
            en.keysStart = Content.Load<Texture2D>("Langue/en/Bonus/keysStart");
            en.keysOver = Content.Load<Texture2D>("Langue/en/Bonus/keysOver");

        //pong bonus
            en.pongStart = Content.Load<Texture2D>("Langue/en/Bonus/pongStart");
            en.pongOver = Content.Load<Texture2D>("Langue/en/Bonus/pongOver");

            #endregion

            #region fr
            fr = new Langue();

            //menu buttons
            fr.buttonTextureStartH = Content.Load<Texture2D>("Langue/fr/Menu/ButtonStartH");
            fr.buttonTextureStartI = Content.Load<Texture2D>("Langue/fr/Menu/ButtonStart");

            fr.buttonTextureOptionsH = Content.Load<Texture2D>("Langue/fr/Menu/ButtonOptionsH");
            fr.buttonTextureOptionsI = Content.Load<Texture2D>("Langue/fr/Menu/ButtonOptions");

            fr.buttonTextureHelpH = Content.Load<Texture2D>("Langue/fr/Menu/ButtonHelpH");
            fr.buttonTextureHelpI = Content.Load<Texture2D>("Langue/fr/Menu/ButtonHelp");

            fr.buttonTextureExitH = Content.Load<Texture2D>("Langue/fr/Menu/ButtonExitH");
            fr.buttonTextureExitI = Content.Load<Texture2D>("Langue/fr/Menu/ButtonExit");

            //menu
            fr.menuAide = Content.Load<Texture2D>("Langue/fr/Menu/menuAide");
            fr.menuChoose = Content.Load<Texture2D>("Langue/fr/Menu/MenuChoose");

            //menuChoose
            fr.gameOver = Content.Load<Texture2D>("Langue/fr/GameOver");

            //menuOptions
            fr.menuOptions = Content.Load<Texture2D>("Langue/fr/Menu/menuOptions");
            fr.fullscreenH = Content.Load<Texture2D>("Langue/fr/Menu/fullscreenH");
            fr.fullscreenN = Content.Load<Texture2D>("Langue/fr/Menu/fullscreenN");
            fr.soundH = Content.Load<Texture2D>("Langue/fr/Menu/soundH");
            fr.soundN = Content.Load<Texture2D>("Langue/fr/Menu/soundN");
            fr.langueH = Content.Load<Texture2D>("Langue/fr/Menu/langueH");
            fr.langueN = Content.Load<Texture2D>("Langue/fr/Menu/langueN");
            fr.nomH = Content.Load<Texture2D>("Langue/fr/Menu/nomH");
            fr.nomN = Content.Load<Texture2D>("Langue/fr/Menu/nomN");
            fr.nomButton = Content.Load<Texture2D>("Langue/fr/Menu/nomButton");

            //menuPause
            fr.buttonTextureMenuH = Content.Load<Texture2D>("Langue/fr/Menu/ButtonMenuH");
            fr.buttonTextureMenuI = Content.Load<Texture2D>("Langue/fr/Menu/ButtonMenu");

            fr.menuPauseAide = Content.Load<Texture2D>("Langue/fr/Menu/MenuPauseAide");

            //keys bonus
            fr.keysStart = Content.Load<Texture2D>("Langue/fr/Bonus/keysStart");
            fr.keysOver = Content.Load<Texture2D>("Langue/fr/Bonus/keysOver");

            //pong bonus
            fr.pongStart = Content.Load<Texture2D>("Langue/fr/Bonus/pongStart");
            fr.pongOver = Content.Load<Texture2D>("Langue/fr/Bonus/pongOver");

            #endregion
        }

        public static Niveau GetPerso(string perso)
        {
            if (perso == "renoi")
                return renoi;
            else if (perso == "roumain")
                return roumain;
            else
                return reunionnais;
            
        }

        public static Langue GetLangue(string langue)
        {
            if (langue == "fr")
                return fr;
            else
                return en;
        }
    }
}
