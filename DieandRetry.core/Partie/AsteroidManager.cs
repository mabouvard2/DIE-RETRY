using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Manager des astéroides
    /// </summary>
    public class AsteroidManager : GameObject
    {
        #region Attribut
        /// <summary>
        /// Nombre maximum d'astéroide sur la fenêtre
        /// </summary>
        private const int MaxAsteroid = 10;

        /// <summary>
        /// Manager du player
        /// </summary>
        private PlayerManager managerPlayer;

        /// <summary>
        /// Liste des astéroides
        /// </summary>
        public IList<Asteroid> Asteroids { get; set;} = new List<Asteroid>();
        #endregion  

        #region Méthodes

        /// <summary>
        /// Constructeur du manager d'astéroide
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public AsteroidManager(Microsoft.Xna.Framework.Game game,SpriteBatch spriteBatch): base(game,spriteBatch)
        {
            managerPlayer = ((Game1)Game).RecupPlayerManager;
        }

        /// <summary>
        /// Méthode update qui met a jour tout les astéroides de la liste
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var asteroid in Asteroids)
            {
                asteroid.Update(gameTime);

                if (asteroid.Rectangle.Intersects(managerPlayer.player.BoundingRectangle))
                {
                    managerPlayer.player.OnDeath();
                }

            }
            if(gameTime.TotalGameTime.Ticks % 100 == 0 && Asteroids.Count < MaxAsteroid)
            {
                Asteroids.Add(new Asteroid(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/asteroid"), new Vector2(RandomHelper.GetNextFloat(), RandomHelper.GetNextFloat()), 1f));
            }

            CheckPosition();
        }

        /// <summary>
        /// Méthode permettant de dessiner tout les astéroides de la liste
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach(var asteroid in Asteroids)
            {
                asteroid.Draw(gameTime);
            }
        }

        /// <summary>
        /// Méthode permettant de vérifier la position de chaque astéroide affin de les faire réaparaître
        /// </summary>
        private void CheckPosition()
        {
            for (int i = 0; i < Asteroids.Count; i++)
                if (Asteroids[i].Rectangle.X > Game1.ScreenWidth)
                    Asteroids[i] = new Asteroid(Game, _spriteBatch, Game.Content.Load<Texture2D>("Sprites/asteroid"), new Vector2(RandomHelper.GetNextFloat(), RandomHelper.GetNextFloat()), 1f);
        }

        #endregion
    }
}
