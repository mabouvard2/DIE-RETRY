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
    /// Classe du background défilant
    /// </summary>
    public class ScrollingBackground : GameObject
    {
        #region Attributs
        /// <summary>
        /// Multiplicateur de la vitesse de défilemment du background
        /// </summary>
        private float _scrollingSpeed;

        /// <summary>
        /// Liste contenant le sprite ainsi qu'un double de ce sprite
        /// </summary>
        private List<Sprite> _sprites;

        /// <summary>
        /// Vitesse de défilemment du background
        /// </summary>
        private float _speed;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur qui va appeller un autre constructeur afin de créer une liste contenant deux fois la même texture
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="texture">Texture du background</param>
        /// <param name="_Layer">Couche sur laquel le background est dessiné</param>
        /// <param name="scrollingSpeed">Multiplicateur de la vitesse de défilemment du background</param>
        public ScrollingBackground(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, Texture2D texture, float _Layer, float scrollingSpeed)
          : this(game, spriteBatch, new List<Texture2D>() { texture, texture }, _Layer, scrollingSpeed)
        {

        }

        /// <summary>
        /// Constructeur avec la double texture
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="textures">Texture du background + son doublon</param>
        /// <param name="_Layer">Couche sur laquel le background est dessiné</param>
        /// <param name="scrollingSpeed">Multiplicateur de la vitesse de défilemment du background</param>
        public ScrollingBackground(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, List<Texture2D> textures, float _Layer, float scrollingSpeed) : base(game, spriteBatch)
        {
            _sprites = new List<Sprite>();

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Sprite(Game, _spriteBatch, texture, _Layer, new Vector2(i * texture.Width - Math.Min(i, i + 1), Game1.ScreenHeight - texture.Height)));
            }

            _scrollingSpeed = scrollingSpeed;
        }
        #endregion

        #region Déplacement et vérification

        /// <summary>
        /// Méthode mettant a jour le background
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        public override void Update(GameTime gameTime)
        {
            ApplySpeed(gameTime);

            CheckPosition();
        }

        /// <summary>
        /// Méthode permettant d'appliquer la vitesse sur le background
        /// </summary>
        /// <param name="gameTime"></param>
        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            foreach (var sprite in _sprites)
            {
                sprite.Position.X -= _speed;
            }
        }

        /// <summary>
        /// Méthode permettant de vérifier la position du background
        /// </summary>
        private void CheckPosition()
        {
            for (int i = 0; i < _sprites.Count; i++) //On regarde la liste de sprites
            {
                var sprite = _sprites[i];

                if (sprite.Rectangle.Right <= 0) //Si le sprite est entièrement disparu a gauche de l'écran
                {
                    var index = i - 1;

                    if (index < 0)
                        index = _sprites.Count - 1;

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2); //On le déplace tout a droite du sprite doublon de sorte a ce que la transition soit fluide
                }
            }
        }

        /// <summary>
        /// Méthode permettant de dessiner le background
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime);
        }

        #endregion
    }
}
