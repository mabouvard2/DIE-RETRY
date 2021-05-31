using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Notre classe Astéroide
    /// </summary>
    public class Asteroid : Sprite
    {
        #region Attributs
        /// <summary>
        /// Animations de l'Astéroide
        /// </summary>
        private Animation asteroidAnimation;
        public IList<Texture2D> Animations { get; set; } = new List<Texture2D>();
        public AnimationAsteroid AnimationAsteroidManager { get; set; }

        /// <summary>
        /// Vecteur vélocité de l'astéroide
        /// </summary>
        public Vector2 Velocity { get; set; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Constructeur d'astéroides, appelle le contructeur de Sprite
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="texture">Texture de l'astéroide</param>
        /// <param name="velocity">Vecteur vélocité de l'astéroid</param>
        /// <param name="layer">Couche sur laquelle l'astéroide va être dessiné</param>
        public Asteroid(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, Texture2D texture, Vector2 velocity, float layer) : base(game, spriteBatch, texture, layer, new Vector2(RandomHelper.GetNextInt(0,500), 1))
        {
            Velocity = velocity;
            AnimationAsteroidManager = new AnimationAsteroid();
            LoadContent();
            AnimationAsteroidManager.PlayAnimation(asteroidAnimation);
        }

        /// <summary>
        /// LoadContent pour charger l'animation de l'astéroide
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            asteroidAnimation = new Animation(Game.Content.Load<Texture2D>("Animations/12_nebula_spritesheet"), 0.1f, true);

        }

        /// <summary>
        /// Update de la position et du vecteur vélocité de l'astéroide
        /// </summary>
        /// <param name="gameTime">Temps de jeu</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position = Vector2.Add(Position, Velocity);
            var clientBounds = Game.Window.ClientBounds;
            if (Rectangle.Left.Equals(clientBounds.Left))
            {
                Velocity = Vector2.Reflect(Velocity, Vector2.UnitX);
            }
            if (Rectangle.Top.Equals(clientBounds.Top) ||
                Rectangle.Bottom.Equals(clientBounds.Bottom))
            {
                Velocity = Vector2.Reflect(Velocity, Vector2.UnitY);
            }

        }

        /// <summary>
        /// Méthode pour dessiner un astéroïde ainsi que son animation
        /// </summary>
        /// <param name="gameTime">Temps de jeu</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            AnimationAsteroidManager.Draw(gameTime, _spriteBatch, Position, SpriteEffects.None);
        }
        #endregion
    }
}