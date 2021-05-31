using DieandRetry.core.Partie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DieandRetry.core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D diedOverlay;
        public static int ScreenWidth { get; } = 1280;
        public static int ScreenHeight { get; } = 720;

        private List<ScrollingBackground> _scrollingBackgrounds;
        private bool wasContinuePressed;

        public IList<GameObject> GameObjects { get; set; } = new List<GameObject>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            RecupPlayerManager = new PlayerManager(this, _spriteBatch);
            GameObjects.Add(RecupPlayerManager);
            RecupAsteroideManager = new AsteroidManager(this , _spriteBatch);
            GameObjects.Add(RecupAsteroideManager);
            RecupPlateformeManager = new ManagerPlateforme(this, _spriteBatch, 30f);
            GameObjects.Add(RecupPlateformeManager);
            LoadContent();
        }
        public AsteroidManager RecupAsteroideManager { get; set; }
        public ManagerPlateforme RecupPlateformeManager { get; set; }
        public PlayerManager RecupPlayerManager { get; set; }
        protected override void LoadContent()
        {
            _scrollingBackgrounds = new List<ScrollingBackground>();
            _scrollingBackgrounds.Add(new ScrollingBackground(this, _spriteBatch, Content.Load<Texture2D>("Sprites/Back/Nebula Aqua-Pink"), 0.5f, 0f));
            _scrollingBackgrounds.Add(new ScrollingBackground(this, _spriteBatch, Content.Load<Texture2D>("Sprites/Back/Stars Small_1"), 0.62f, 25f));
            _scrollingBackgrounds.Add(new ScrollingBackground(this, _spriteBatch, Content.Load<Texture2D>("Sprites/Back/Stars Small_2"), 0.65f, 25f));
            _scrollingBackgrounds.Add(new ScrollingBackground(this, _spriteBatch, Content.Load<Texture2D>("Sprites/Back/Stars-Big_1_2_PC"), 0.80f, 30f));
            _scrollingBackgrounds.Add(new ScrollingBackground(this, _spriteBatch, Content.Load<Texture2D>("Sprites/Back/Stars-Big_1_2_PC"), 0.85f, 35f));
            diedOverlay = Content.Load<Texture2D>("Overlays/deathScreen");
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);

            foreach (var sb in _scrollingBackgrounds)
                sb.Update(gameTime);

            base.Update(gameTime);
           
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }
            
        }

        private void HandleInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            bool continuePressed = Keyboard.GetState().IsKeyDown(Keys.X);

            // Perform the appropriate action to advance the game and
            // to get the player back to playing.
            if (!wasContinuePressed && continuePressed)
            {
                if (!RecupPlayerManager.player.IsAlive)
                {
                    Restart();
                }
            }

            wasContinuePressed = continuePressed;
        }

        private void Restart()
        {
            Content.Unload();
            GameObjects.Clear();
            RecupPlayerManager = new PlayerManager(this, _spriteBatch);
            GameObjects.Add(RecupPlayerManager);
            RecupAsteroideManager = new AsteroidManager(this, _spriteBatch);
            GameObjects.Add(RecupAsteroideManager);
            RecupPlateformeManager = new ManagerPlateforme(this, _spriteBatch, 30f);
            GameObjects.Add(RecupPlateformeManager);
            LoadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            foreach (var gameObject in GameObjects)
            {
                gameObject.Draw(gameTime);
            }


            foreach (var sb in _scrollingBackgrounds)
                sb.Draw(gameTime);
            DrawHud();
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawHud()
        {
            Texture2D status = null;
            Vector2 center = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
            if (!RecupPlayerManager.player.IsAlive)
            {
                status = diedOverlay;
            }

            if (status != null)
            {
                // Draw status message.
                Vector2 statusSize = new Vector2(status.Width, status.Height);
                _spriteBatch.Draw(status, center - statusSize / 2, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            }
        }
    }
}
