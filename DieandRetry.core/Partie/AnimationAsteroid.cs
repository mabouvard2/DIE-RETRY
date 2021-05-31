using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    public class AnimationAsteroid
    {
        /// <summary>
        /// Récupère l'animation qui est en train d'être jouée
        /// </summary>
        public Animation Animation
        {
            get { return animation; }
        }
        Animation animation;

        /// <summary>
        /// Récupère l'index de la frame actuellement affichée
        /// </summary>
        public int FrameIndex
        {
            get { return frameIndex; }
        }
        int frameIndex;

        /// <summary>
        /// Le temps en second dont la frame en court doit être afficher
        /// </summary>
        private float time;

        /// <summary>
        /// Récupère l'origine de la texte en bas et au centre de chaque 
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(Animation.FrameWidth / 2.0f, Animation.FrameHeight); }
        }

        /// <summary>
        /// Continue ou relance une annimation
        /// </summary>
        /// <param name="animation">Animation qui doit être jouée</param>
        public void PlayAnimation(Animation animation)
        {
            // Si l'animation est déjà en court, elle ne doit pas être relancée
            if (Animation == animation)
                return;

            // Lancement de l'animation
            this.animation = animation;
            this.frameIndex = 0;
            this.time = 0.0f;
        }

        /// <summary>
        /// Méthode permettant de dessiner l'animation
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="position">Position de l'animation</param>
        /// <param name="spriteEffects">Effet du sprite</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > Animation.FrameTime)
            {
                time -= Animation.FrameTime;

                if (Animation.IsLooping)
                {
                    frameIndex = (frameIndex + 1) % Animation.FrameCount;
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, Animation.FrameCount - 1);
                }
            }

            // Calcule le rectangle source de la frame en court
            Rectangle source = new Rectangle(FrameIndex % 8 * Animation.FrameWidth, FrameIndex / 8 * Animation.FrameHeight, Animation.FrameWidth, Animation.FrameHeight);

            // Dessine la frame en court
            var position2 = new Vector2(position.X + 15, position.Y + 65);
            spriteBatch.Draw(Animation.Texture, position2, source, Color.White, 0.0f, Origin, 1.0f, spriteEffects, 0.99f);
        }
    }
}
