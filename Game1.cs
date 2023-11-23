using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Animation_Summative
{
    public class Game1 : Game
    {
        double introTime; // When user switches to battle screen, save current time elapsed to time animations
        bool tie1, tie2, tieExploded1, tieExploded2;
        bool redBlast,greenBlast1, greenBlast2;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D spaceTexture;
        Texture2D xWingTexture;
        Texture2D tieFighterTexture;
        Texture2D blastTexture;
        Texture2D explosionTexture;
        Rectangle space1Rect, space2Rect;
        Rectangle xWingRect;
        Rectangle tieFighterRect1, tieFighterRect2;
        Rectangle redBlastRect, greenBlastRect1, greenBlastRect2;
        Vector2 spaceSpeed;
        Vector2 xWingSpeed;
        Vector2 tieSpeed1;
        Vector2 tieSpeed2;
        Vector2 redBlastSpeed, greenBlastSpeed1, greenBlastSpeed2;
        float runTime;
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

            redBlast = false;
            greenBlast1 = false;
            greenBlast2 = false;
            tie1 = false;
            tieExploded1 = false;
            tie2 = false;
            tieExploded2 = false;

            space1Rect = new Rectangle(0, 0, 800, 600);
            space2Rect = new Rectangle(0, -600, 800, 600);
            xWingRect = new Rectangle(350, 475, 100, 100);
            tieFighterRect1 = new Rectangle(350, -150, 100, 100);
            tieFighterRect2 = new Rectangle(0, 0, 100, 100);
            redBlastRect = new Rectangle(628, 500, 15, 35);
            greenBlastRect1 = new Rectangle(392, 200, 15, 35);
            greenBlastRect2 = new Rectangle(0, 0, 15, 35);
            spaceSpeed = new Vector2(0, 2);
            xWingSpeed = new Vector2(0, 0);
            tieSpeed1 = new Vector2(0, 2);
            tieSpeed2 = new Vector2(0, 0);
            redBlastSpeed = new Vector2(0,-4);
            greenBlastSpeed1 = new Vector2(0,4);
            greenBlastSpeed2 = new Vector2(0,4);


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

            runTime = (float)gameTime.TotalGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Battle)
            {
                timeInIntro = 0;
                timeTotal = runTime - timeInIntro;

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

                // Checking if Blasts collide or leave

                if (redBlastRect.Bottom == 0)
                {
                    redBlast = false;
                    redBlastRect.Y = 500;
                }


                // Changing speeds and adding effects based on time

                //X-Wing
                if (timeTotal >= 4 && timeTotal < 9)
                {
                    xWingSpeed.X = 1;
                }
                else if (timeTotal > 9 && timeTotal < 13)
                {
                    xWingSpeed.X = 0;
                }
                else if (timeTotal > 13 && timeTotal < 19)
                {
                    xWingSpeed.X = -1;
                }
                else if (timeTotal > 19 && timeTotal < 20)
                {
                    xWingSpeed.X = 1;
                }
                else if (timeTotal > 20)
                {
                    xWingSpeed.X = 0;
                }

                //Ties
                if (Math.Round(timeTotal) == 3)
                {
                    tie1 = true;
                }
                else if (Math.Round(timeTotal) == 5)
                {
                    tieSpeed1.Y = 0;
                }
                else if (Math.Round(timeTotal) == 7)
                {
                    tieSpeed1.X = 1;
                }

                if (tieFighterRect1.Center.Y == redBlastRect.Y)
                {
                    redBlast = false;
                    redBlastRect.Y = 500;
                    tieExploded1 = true;
                    tieSpeed1.X = 0;
                }

                if (Math.Round(timeTotal) == 13)
                {
                    tie1 = false;
                }
                else if (Math.Round(timeTotal) == 16)
                {
                    tie1 = true;
                    tieExploded1 = false;
                    tieFighterRect1.X = 350;
                    tieFighterRect1.Y = -104;
                    tieSpeed1.Y = 2;
                }
                else if (Math.Round(timeTotal) == 21)
                {
                    tie1 = false;
                }

               

                // Blasts

                if (Math.Round(timeTotal) == 5)
                {
                    greenBlast1 = true;
                }

                if (greenBlastRect1.Top == graphics.PreferredBackBufferHeight)
                {
                    greenBlast1 = false;
                }

                if ( Math.Round(timeTotal) == 9 )
                {
                    redBlast = true;
                }
                else if (Math.Round(timeTotal) == 18 && !greenBlast1)
                {
                    redBlast = true;
                    redBlastRect.X = 393;
                    greenBlastRect1.X = 393;
                    greenBlastRect1.Y = 175;
                    greenBlast1 = true;
                }
                    
                // Applying speeds

                xWingRect.X += (int)xWingSpeed.X;
                xWingRect.Y += (int)xWingSpeed.Y;

                if (tie1 && !tieExploded1)
                {
                    tieFighterRect1.X += (int)tieSpeed1.X;
                    tieFighterRect1.Y += (int)tieSpeed1.Y;
                }

                if (tie2 && !tieExploded2)
                {
                    tieFighterRect2.X += (int)tieSpeed2.X;
                    tieFighterRect2.Y += (int)tieSpeed2.Y;
                }

                if (redBlast)
                {
                    redBlastRect.Y += (int)redBlastSpeed.Y;
                }
                if (greenBlast1)
                {
                    greenBlastRect1.Y += (int)greenBlastSpeed1.Y;
                }
                if (greenBlast2)
                {
                    greenBlastRect2.Y += (int)greenBlastSpeed2.Y;
                }

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

                if (tie1 && !tieExploded1)
                {
                    spriteBatch.Draw(tieFighterTexture, tieFighterRect1, Color.White);
                }
                else if (tie1 && tieExploded1)
                {
                    spriteBatch.Draw(explosionTexture, tieFighterRect1, Color.White);
                }

                if (tie2 && !tieExploded2)
                {

                }
                else if (tie2 && tieExploded2)
                {

                }

                if (redBlast)
                {
                    spriteBatch.Draw(blastTexture, redBlastRect, Color.Red);
                }
                if (greenBlast1)
                {
                    spriteBatch.Draw(blastTexture, greenBlastRect1, Color.Green);
                }
                if (greenBlast2)
                {

                }

            }
            else if (screen == Screen.End)
            {

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}