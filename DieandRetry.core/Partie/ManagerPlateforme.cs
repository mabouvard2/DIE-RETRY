using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Manager de la plateforme
    /// </summary>
    public class ManagerPlateforme : GameObject
    {
        #region Attribut
        /// <summary>
        /// Manager du player
        /// </summary>
        private PlayerManager managerPlayer;

        /// <summary>
        /// Multiplicateur de la vitesse de déplacement des plateformes
        /// </summary>
        private float _scrollingSpeed;

        /// <summary>
        /// Vitesse de déplacement des plateformes
        /// </summary>
        private float _speed;
        /// <summary>
        /// Liste contenant toutes les plateformes sur la fenêtre
        /// </summary>
        public IList<Plateforme> plateformes { get; set; } = new List<Plateforme>();

        #endregion

        #region Constructeur + Update/Load/Draw
        /// <summary>
        /// Constructeur du manager de plateforme
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="scrollingSpeed">Multiplicateur de la vitesse de déplacement des plateformes</param>
        public ManagerPlateforme(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, float scrollingSpeed) : base(game, spriteBatch)
        {
            _scrollingSpeed = scrollingSpeed;
            LoadContent();
        }

        /// <summary>
        /// Méthode qui met a jour la position des platformes
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ApplySpeed(gameTime);

            CheckPosition();
        }

        /// <summary>
        /// Méthode permettant de charger les différentes plateformes
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            managerPlayer = ((Game1)Game).RecupPlayerManager;
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(0, 128), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(128, 256), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(256, 384), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(384, 512), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(512, 640), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(640, 768), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(768, 896), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(896, 1024), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(1024, 1152), RandomHelper.GetNextInt(200, 700)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(1152, 1280), RandomHelper.GetNextInt(200, 700)));

            /*plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, Random.Next(0, 256), Random.Next(200, 600)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, Random.Next(256, 512), Random.Next(200, 600)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, Random.Next(512, 768), Random.Next(200, 600)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, Random.Next(768, 1024), Random.Next(200, 600)));
            plateformes.Add(new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, Random.Next(1024, 1280), Random.Next(200, 600)));*/
        }

        /// <summary>
        /// Méthode permettant de dessiner chaque plateforme
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (var plat in plateformes)
            {
                plat.Draw(gameTime);
            }
        }
        #endregion

        #region Vitesse + vérification position
        /// <summary>
        /// Méthode pour appliquer la vitesse sur les plateformes
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        private void ApplySpeed(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            if (managerPlayer.player.Velocity.X > 0) //Permet de multiplier la vitesse des plateformes quand le joueur bouge vers la droite
            {
                _speed *= managerPlayer.player.Velocity.X * elapsed;
                _speed = (float)Math.Round(_speed);
            }

            foreach (var plat in plateformes)
            {
                plat.Position.X -= _speed; //On change la position de la plateforme
                plat.SetRectangle(new Rectangle((int)plat.Position.X, plat.GetRectangle().Y, plat.GetRectangle().Width, plat.GetRectangle().Height)); //On fait suivre la hitboxe de la plateforme selon la position
            }
        }

        /// <summary>
        /// Vérifie la position de chaque plateforme
        /// </summary>
        private void CheckPosition()
        {
            for (int i = 0; i < plateformes.Count; i++)
            {
                if (plateformes[i].GetRectangle().X + plateformes[i].GetRectangle().Width <= 0) // si la plateforme sort de l'écran a gauche
                {
                    plateformes[i] = new Plateforme(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/neon"), 1f, RandomHelper.GetNextInt(1024, 1280), RandomHelper.GetNextInt(200, 600));  //Créer une nouvelle plateforme pour remplacer l'ancienne
                }
            }
        }

        #endregion
    }
}
