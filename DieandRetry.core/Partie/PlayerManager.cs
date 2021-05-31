using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Manager du player
    /// </summary>
    public class PlayerManager : GameObject
    {
        /// <summary>
        /// Le joueur
        /// </summary>
        public Player player { get; set; }

        /// <summary>
        /// Constructeur du manager
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public PlayerManager(Microsoft.Xna.Framework.Game game,SpriteBatch spriteBatch): base(game,spriteBatch)
        {

        }

        /// <summary>
        /// Méthode permettant de mettre a jour le joueur
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var keyboardState = Keyboard.GetState();
            if (player == null)
                player = new Player(Game, _spriteBatch, new Vector2()); //On instancie le joueur seulement a partir du l'update et pas dans le constructeur sous peine de lancer une exception dans la classe joueur car le joueur utilise les plateformes
            player.Update(gameTime, keyboardState);
        }

        /// <summary>
        /// Méthode permettant de dessiner le joueur
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            player.Draw(gameTime, _spriteBatch);
        }
    }
}
