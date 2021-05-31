using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Classe plateforme
    /// </summary>
    public class Plateforme : Sprite
    {
        /// <summary>
        /// Hitbox de la plateforme
        /// </summary>
        public Rectangle plateformHitBox;

        /// <summary>
        /// Getteurs et Setteurs de la hitboxe de la plateforme 
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return plateformHitBox;
        }

        public void SetRectangle(Rectangle newHitbox)
        {
            plateformHitBox = newHitbox;
        }

        /// <summary>
        /// Constructeur d'une plateforme
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="texture">Texture de la plateforme</param>
        /// <param name="layer">Couche sur laquelle la plateforme va être dessinée</param>
        /// <param name="x">Position x de la plateforme</param>
        /// <param name="y">Position y de la plateforme</param>
        public Plateforme(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, Texture2D texture, float layer, int x, int y) : base(game, spriteBatch, texture, layer)
        {
            Position.X = x;
            Position.Y = y;
            plateformHitBox = new Rectangle(x, y, texture.Width, texture.Height);
        }

        /// <summary>
        /// Méthode permettant de dessiner la plateforme
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
