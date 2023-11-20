using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Animation_Summative
{
    public class Game1 : Game
    {
        double introTime; // When user switches to battle screen, save current time elapsed to time animations

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D spaceTexture;
        Texture2D xWingTexture;
        Texture2D tieFighterTexture;
        Texture2D blastTexture;
        Texture2D explosionTexture;
        Rectangle space1Rect, space2Rect;
        Rectangle xWingRect;
        Rectangle tieFighterRect;
        Rectangle blastRect;
        Vector2 spaceSpeed;
        float timeTotal;
        float timeInIntro;

        enum Screen
        {
            Intro,
            Battle,
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
            screen = Screen.Battle;
            
            space1Rect = new Rectangle(0, 0, 800, 600);
            space2Rect = new Rectangle(0, -600, 800, 600);
            xWingRect = new Rectangle(350, 475, 100, 100);
            spaceSpeed = new Vector2(0, 2);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceTexture = Content.Load<Texture2D>("Space");
            tieFighterTexture = Content.Load<Texture2D>("Tie");
            xWingTexture = Content.Load<Texture2D>("X-Wing");
            blastTexture = Content.Load<Texture2D>("rectangle");
            explosionTexture = Content.Load<Texture2D>("PixelBoom");

        }

        protected override void Update(GameTime gameTime)
        {

            timeTotal = (float)gameTime.TotalGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            space1Rect.Y += (int)spaceSpeed.Y;
            if (space1Rect.Top >= graphics.PreferredBackBufferHeight)
            {
                space1Rect.Y = -600;
            }

            space2Rect.Y += (int)spaceSpeed.Y;
            if (space2Rect.Top >= graphics.PreferredBackBufferHeight)
            {
                space2Rect.Y = -600;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (screen == Screen.Intro)
            {

            }
            else if (screen == Screen.Battle)
            {
                spriteBatch.Draw(spaceTexture, space1Rect, Color.White);
                spriteBatch.Draw(spaceTexture, space2Rect, Color.White);
                spriteBatch.Draw(xWingTexture, xWingRect, Color.White);

            }
            else if (screen == Screen.End)
            {

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}