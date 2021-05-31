using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    public class Animation
    {
        /// <summary>
        /// Toutes les frames de l'animations horizontal
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;

        /// <summary>
        /// Le temps pour afficher une frame
        /// </summary>
        public float FrameTime
        {
            get { return frameTime; }
        }
        float frameTime;

        /// <summary>
        /// Quand l'animation est terminée, doit-elle être relancée ?
        /// </summary>
        public bool IsLooping
        {
            get { return isLooping; }
        }
        bool isLooping;

        /// <summary>
        /// Récupère le nombre de frame dans une animation
        /// </summary>
        public int FrameCount => 61;

        /// <summary>
        /// Récupère la largeur de la frame
        /// </summary>
        public int FrameWidth
        {
            get { return Texture.Width / 8; }
        }

        /// <summary>
        /// Récupère la hauteur de la frame
        /// </summary>
        public int FrameHeight
        {
            get { return Texture.Height / 8; }
        }

        /// <summary>
        /// Constructeur d'une animation
        /// </summary>
        /// <param name="texture">Texture de l'animation</param>
        /// <param name="frameTime">Durée d'affichage d'une frame</param>
        /// <param name="isLooping">Est-ce que l'animation doit-être relancée ?</param>
        public Animation(Texture2D texture, float frameTime, bool isLooping)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }
    }
}

