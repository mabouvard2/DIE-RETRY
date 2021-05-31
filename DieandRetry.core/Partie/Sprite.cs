using DieandRetry.core.Partie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Notre classe Sprite
    /// </summary>
    public class Sprite : GameObject
    {
        #region Attribut
        
        /// <summary>
        /// Texture du sprite
        /// </summary>
        protected Texture2D _texture;

        /// <summary>
        /// Couche sur laquelle le sprite va être dessiné
        /// </summary>
        private float _layer { get; set; }
        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }

        /// <summary>
        /// Position du sprite
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Hitbox du sprite
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur sans la position du sprite 
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="texture">Texture du sprite</param>
        /// <param name="layer">Couche sur laquelle le sprite va être dessiné</param>
        public Sprite(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, Texture2D texture, float layer) : base(game, spriteBatch)
        {
            _texture = texture;
            Layer = layer;
        }
        /// <summary>
        /// Constructeur avec la position du sprite
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="texture">Texture du sprite</param>
        /// <param name="layer">Couche sur laquelle le sprite va être dessiné</param>
        /// <param name="position">Position du sprite</param>
        public Sprite(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, Texture2D texture, float layer,Vector2 position) : base(game, spriteBatch)
        {
            _texture = texture;
            Layer = layer;
            Position = position;
        }
        #endregion

        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Méthode pour dessiner le sprite
        /// </summary>
        /// <param name="gameTime">Temps de jeu</param>
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
            base.Draw(gameTime);
        }
    }
}
