using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DieandRetry.core.Partie
{
    /// <summary>
    /// Classe joueur
    /// </summary>
    public class Player : GameObject
    {
        #region Attributs
        /// <summary>
        /// manager Plateforme
        /// </summary>
        private ManagerPlateforme managerPlat;
        /// <summary>
        /// Animations du joueur
        /// </summary>
        private AnimationForPlayer idleAnimation;
        private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer animationPlayer;

        /// <summary>
        /// Est-ce que le joueur est vivant
        /// </summary>
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        private bool isAlive;

        /// <summary>
        /// Position du joueur
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 position;

        /// <summary>
        /// Vecteur vélocité du joueur
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        private Vector2 velocity;

        // x
        private const float MoveAcceleration = 13000.0f;
        private const float MaxMoveSpeed = 2000.0f;
        private const float GroundSpeed = 0.48f;
        private const float AirSpeed = 0.58f;

        // y
        private const float MaxJumpTime = 0.5f;
        private const float JumpVelocity = -3500.0f;
        private const float GravityAcceleration = 3400.0f;
        private const float MaxFallSpeed = 200.0f;
        private const float JumpPower = 0.1f;

        private const Buttons JumpButton = Buttons.A;

        /// <summary>
        /// Est-ce que le joueur touche le sol
        /// </summary>
        public bool IsOnGround
        {
            get { return isOnGround; }
            set { isOnGround = value; }
        }
        private bool isOnGround;

        /// <summary>
        /// Mouvement du joueur
        /// </summary>
        private float movement;

        /// <summary>
        /// Attributs en rapport avec le saut du joueur
        /// </summary>
        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;

        private Rectangle localBounds;

        /// <summary>
        /// Hitbox du joueur
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - animationPlayer.Origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - animationPlayer.Origin.Y) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        #endregion

        #region Constructeur + Load/Update/Draw
        /// <summary>
        /// Constructeur du joueur
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="velocity">Vecteur vélocité du joueur</param>
        public Player(Microsoft.Xna.Framework.Game game, SpriteBatch spriteBatch, Vector2 velocity) : base(game, spriteBatch)
        {
            managerPlat = ((Game1)Game).RecupPlateformeManager;
            Velocity = velocity;
            animationPlayer = new AnimationPlayer();
            LoadContent();
            animationPlayer.PlayAnimation(idleAnimation);
            isAlive = true;
            Position = new Vector2(50, 50);
        }

        /// <summary>
        /// Chargement de l'animation du joueur et du localBounds
        /// </summary>
        protected override void LoadContent()
        {
            idleAnimation = new AnimationForPlayer(Game.Content.Load<Texture2D>("Animations/hedge"), 0.1f, true);

            int width = (int)(idleAnimation.FrameWidth * 0.4);
            int left = (idleAnimation.FrameWidth - width) / 2;
            int height = (int)(idleAnimation.FrameHeight * 0.8);
            int top = idleAnimation.FrameHeight - 10;
            localBounds = new Rectangle(left, top, width, height);
        }

        /// <summary>
        /// Mise à joueur de la position du joueur et de son animation selon la vitesse et les collisions
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="keyboardState"></param>
        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            base.Update(gameTime);
            if (IsAlive)
            {
                GetInput(keyboardState);
                IsOnGround = HandleCollision();
            }
            ApplyPhysics(gameTime);
            if (IsAlive && IsOnGround)
            {
                if (Math.Abs(Velocity.X) - 0.02f > 0)
                {
                    animationPlayer.PlayAnimation(idleAnimation);
                }
                else
                {
                    animationPlayer.PlayAnimation(idleAnimation);
                }
            }
            movement = 0.0f;
            isJumping = false;
            if (BoundingRectangle.Top >= Game1.ScreenHeight && IsAlive)
                OnDeath();
        }

        /// <summary>
        /// Permet de dessiner le joueur
        /// </summary>
        /// <param name="game">Game de notre jeu</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime);
            if (Velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X > 0)
                flip = SpriteEffects.None;
            animationPlayer.Draw(gameTime, spriteBatch, Position, flip);
        }

        #endregion

        #region DéplacementsG
        /// <summary>
        /// Méthode permettant de vérifier si le joueur est sur une plateforme
        /// </summary>
        /// <returns>Retourne un bolléen pour dire si le joueur est sur une plateforme ou non</returns>
        private bool HandleCollision()
        {
            foreach (var plat in managerPlat.plateformes)
            {
                var py = plat.GetRectangle().Y;
                var by = this.BoundingRectangle.Y;
                if ((plat.GetRectangle().Y + 4) >= this.BoundingRectangle.Y && plat.GetRectangle().Y <= this.BoundingRectangle.Y)
                {
                    if (plat.GetRectangle().X < this.BoundingRectangle.X)
                    {
                        if ((plat.GetRectangle().X + plat.GetRectangle().Width) > this.BoundingRectangle.X)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Récupère les appuis sur le clavier du joueur
        /// </summary>
        /// <param name="keyboardState">Statut du clavier</param>
        private void GetInput(KeyboardState keyboardState)
        {
            if (Math.Abs(movement) < 0.5f)
                movement = 0.0f;

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                movement = -1.0f;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                movement = 1.0f;
            }

            isJumping =
                keyboardState.IsKeyDown(Keys.Space) ||
                keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.Z);
        }

        /// <summary>
        /// Applique la physique, gravité et saut, sur le vecteur vélocité du joueur
        /// </summary>
        /// <param name="gameTime">Temps du jeu</param>
        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;

            velocity.X += movement * MoveAcceleration * elapsed;
            velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

            if (IsOnGround)
            {
                velocity.X *= GroundSpeed;
                velocity.Y = 0;
            }
            else
                velocity.X *= AirSpeed;

            velocity.Y = DoJump(velocity.Y, gameTime);

            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

            /*if (BoundingRectangle.Left < 0 && IsAlive)
                Position = new Vector2(1, (float)Math.Round(Position.Y));
            else if (BoundingRectangle.Top < 0 && IsAlive)
            {
                velocity.Y = 0;
                Position += velocity * elapsed;
                Position = new Vector2((float)Math.Round(Position.X), 25);
            }*/

            if (Position.X == previousPosition.X)
                velocity.X = 0;

            if (Position.Y == previousPosition.Y)
                velocity.Y = 0;
        }

        /// <summary>
        /// Modifie le vecteur vélocité du joueur sur l'axe Y si le joueur appuie sur la touche de saut
        /// </summary>
        /// <param name="velocityY">Vélocité du joueur sur l'axe Y</param>
        /// <param name="gameTime">Temps du jeu</param>
        /// <returns>Retourne la vélocité sur l'axe Y du joueur</returns>
        private float DoJump(float velocityY, GameTime gameTime)
        {
            if (isJumping)
            {

                if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
                {
                    if (jumpTime == 0.0f)
                        jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                {
                    velocityY = JumpVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpPower));
                }
                else
                {
                    jumpTime = 0.0f;
                }
            }
            else
            {
                jumpTime = 0.0f;
            }
            wasJumping = isJumping;
            return velocityY;
        }

        /// <summary>
        /// Passe le joueur à l'état mort
        /// </summary>
        public void OnDeath()
        {
            isAlive = false;
        }

        #endregion

    }
}
