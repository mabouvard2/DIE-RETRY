using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Classe GameObject
    /// </summary>
    public class GameObject : DrawableGameComponent
    {
        /// <summary>
        /// Nombre de frames
        /// </summary>
        public const int FrameCount = 80;
        /// <summary>
        /// SpriteBatch utilisé dans tout notre jeu
        /// </summary>
        protected readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public GameObject(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch) : base(game)
        {
            _spriteBatch = spriteBatch;
        }
    }
}
