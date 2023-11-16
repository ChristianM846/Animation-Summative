using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Animation_Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D spaceTexture;
        Texture2D tieFighterTexture;
        Texture2D xWingTexture;
        Texture2D blastTexture;
        Texture2D explosionTexture;

        List<Rectangle> space = new List<Rectangle>();

        enum Screen
        {
            Intro,
            Something,
            End
        }
        Screen screen;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;
            spaceTexture = Content.Load<Texture2D>("Space");
            tieFighterTexture = Content.Load<Texture2D>("Tie");
            xWingTexture = Content.Load<Texture2D>("X-Wing");
            blastTexture = Content.Load<Texture2D>("rectangle");
            explosionTexture = Content.Load<Texture2D>("PixelBoom");



            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}