﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Animation_Summative
{
    public class Game1 : Game
    {
        int introTextY;
        int endingTextY;
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
        Texture2D lukeTexture;
        Texture2D vaderTexture;
        Rectangle space1Rect, space2Rect;
        Rectangle xWingRect;
        Rectangle tieFighterRect1, tieFighterRect2;
        Rectangle redBlastRect, greenBlastRect1, greenBlastRect2;
        Rectangle lukeRect;
        Rectangle vaderRect;
        Vector2 spaceSpeed;
        Vector2 xWingSpeed;
        Vector2 tieSpeed1;
        Vector2 tieSpeed2;
        Vector2 redBlastSpeed, greenBlastSpeed1, greenBlastSpeed2;
        SpriteFont introFont;
        SpriteFont endingFont;
        SpriteFont instructionFont;
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
            screen = Screen.Intro; // For Testing

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
            lukeRect = new Rectangle(0, 50, 150, 500);
            vaderRect = new Rectangle(650, 50, 150, 500);
            spaceSpeed = new Vector2(0, 2);
            xWingSpeed = new Vector2(0, 0);
            tieSpeed1 = new Vector2(0, 2);
            tieSpeed2 = new Vector2(0, 2);
            redBlastSpeed = new Vector2(0,-4);
            greenBlastSpeed1 = new Vector2(0,4);
            greenBlastSpeed2 = new Vector2(0,4);
            


            base.Initialize();
        }

        protected override void LoadContent()
        {
            introTextY = graphics.PreferredBackBufferHeight;
            endingTextY = graphics.PreferredBackBufferHeight;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceTexture = Content.Load<Texture2D>("Space");
            tieFighterTexture = Content.Load<Texture2D>("Tie");
            xWingTexture = Content.Load<Texture2D>("X-Wing");
            blastTexture = Content.Load<Texture2D>("rectangle");
            explosionTexture = Content.Load<Texture2D>("PixelBoom");
            lukeTexture = Content.Load<Texture2D>("Luke");
            vaderTexture = Content.Load<Texture2D>("Vader");
            introFont = Content.Load<SpriteFont>("IntroText");
            endingFont = Content.Load<SpriteFont>("Ending");
            instructionFont = Content.Load<SpriteFont>("Instruction");

        }

        protected override void Update(GameTime gameTime)
        {

            runTime = (float)gameTime.TotalGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                introTextY -= 1;
            }
            else if (screen == Screen.Battle)
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

                // Checking if Blasts leave screen

                if (redBlastRect.Bottom == 0)
                {
                    redBlast = false;
                    redBlastRect.Y = 500;
                }

                if (greenBlastRect1.Top == graphics.PreferredBackBufferHeight)
                {
                    greenBlast1 = false;
                }

                if (greenBlastRect2.Top == graphics.PreferredBackBufferHeight)
                {
                    greenBlast2 = false;
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
                else if (Math.Round(timeTotal) == 20) // I changed methods for checking time here because these ones didn't look right otherwise
                {
                    xWingSpeed.X = 0;
                }
                else if (Math.Round(timeTotal) == 25)
                {
                    xWingSpeed.X = 1;
                }
                else if (Math.Round(timeTotal) == 26)
                {
                    xWingSpeed.X = -1;
                }
                else if (Math.Round(timeTotal) == 27)
                {
                    xWingSpeed.X = 0;
                }
                else if (Math.Round(timeTotal) == 32)
                {
                    xWingSpeed.Y = -2;
                }

                if (xWingRect.Bottom <= -20)
                {
                    screen = Screen.End;
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

                if (tieFighterRect1.Center.Y == redBlastRect.Top && !tie2)
                {
                    redBlast = false;
                    redBlastRect.Y = 500;
                    tieExploded1 = true;
                    tieSpeed1.X = 0;
                    tieSpeed1.Y = 0;
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
                    tieExploded1 = false;
                }
                else if (Math.Round(timeTotal) == 23)
                {
                    tieFighterRect1.X = 500;
                    tieFighterRect1.Y = -100;
                    tieFighterRect2.X = 200;
                    tieFighterRect2.Y = -100;
                    tie1 = true;
                    tieExploded1 = false;
                    tieSpeed1.Y = 2;
                    tie2 = true;
                }
                else if (Math.Round(timeTotal) == 26)
                {
                    tieSpeed1.Y = 0;
                    tieSpeed2.Y = 0;
                }
                else if (Math.Round(timeTotal) == 27)
                {
                    tieSpeed1.X = -1;
                    tieSpeed2.X = 1;
                }

               if (tieFighterRect1.Center.Y == redBlastRect.Y && tie2)
               {
                    redBlast = false;
                    redBlastRect.Y = 500;
                    tieExploded1 = true;
                    tieSpeed1.X = -1;
               }

               if (tieFighterRect1.Left == tieFighterRect2.Center.X && !tieExploded2)
               {
                    tieExploded2 = true;
                    tieSpeed2.X = 0;
                    tieSpeed1.X = 0;
               }

               if (Math.Round(timeTotal) == 31)
                {
                    tie1 = false;
                    tie2 = false;
                }

                // Blasts

                if (Math.Round(timeTotal) == 5)
                {
                    greenBlast1 = true;
                }
                else if ( Math.Round(timeTotal) == 9 )
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
                else if (Math.Round(timeTotal) == 26)
                {
                    redBlast = true;
                    redBlastRect.X = 495;
                }
                else if (Math.Round(timeTotal) == 27 && !greenBlast2)
                {
                    greenBlastRect1.X = 500;
                    greenBlastRect1.Y = 200;
                    greenBlastRect2.X = 250;
                    greenBlastRect2.Y = 200;
                    greenBlast1 = true;
                    greenBlast2 = true;
                }

                // Applying speeds

                xWingRect.X += (int)xWingSpeed.X;
                xWingRect.Y += (int)xWingSpeed.Y;

                if (tie1)
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
            else if (screen == Screen.End)
            {
                endingTextY -= 1;

                if (endingTextY <= -450)
                {
                    Exit();
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
                spriteBatch.Draw(spaceTexture, new Rectangle(0, 0, 800, 600), Color.White);
                spriteBatch.Draw(lukeTexture, lukeRect, Color.White);
                spriteBatch.Draw(vaderTexture, vaderRect, Color.White);
                spriteBatch.DrawString(introFont, "As Luke Skywalker", new Vector2(200, introTextY), Color.Yellow);
                spriteBatch.DrawString(introFont, "trains with Jedi", new Vector2(240, introTextY + 75), Color.Yellow);
                spriteBatch.DrawString(introFont, "Master Yoda on the", new Vector2(190, introTextY + 150), Color.Yellow);
                spriteBatch.DrawString(introFont, "remote planet of", new Vector2(215, introTextY + 225), Color.Yellow);
                spriteBatch.DrawString(introFont, "Dagobah, he suddenly", new Vector2(165, introTextY + 300), Color.Yellow);
                spriteBatch.DrawString(introFont, "has a force vision:", new Vector2(200, introTextY + 375), Color.Yellow);
                spriteBatch.DrawString(introFont, "His friends are in", new Vector2(205, introTextY + 450), Color.Yellow);
                spriteBatch.DrawString(introFont, "grave danger on the", new Vector2(180, introTextY + 525), Color.Yellow);
                spriteBatch.DrawString(introFont, "planet Bespin!", new Vector2(240, introTextY + 600), Color.Yellow);

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
                    spriteBatch.Draw(tieFighterTexture, tieFighterRect2, Color.White);
                }
                else if (tie2 && tieExploded2)
                {
                    spriteBatch.Draw(explosionTexture, tieFighterRect2, Color.White);
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
                    spriteBatch.Draw(blastTexture, greenBlastRect2, Color.Green);
                }

            }
            else if (screen == Screen.End)
            {
                spriteBatch.Draw(spaceTexture, new Rectangle(0, 0, 800, 600), Color.White);
                spriteBatch.DrawString(endingFont, "Having eliminated the", new Vector2(180, endingTextY), Color.Yellow);
                spriteBatch.DrawString(endingFont, "opposing squadron,", new Vector2(200, endingTextY + 75), Color.Yellow);
                spriteBatch.DrawString(endingFont, "Luke continous to race", new Vector2(180, endingTextY + 150), Color.Yellow);
                spriteBatch.DrawString(endingFont, "to Bespin, in hopes to", new Vector2(180, endingTextY + 225), Color.Yellow);
                spriteBatch.DrawString(endingFont, "save his friends,", new Vector2(240, endingTextY + 300), Color.Yellow);
                spriteBatch.DrawString(endingFont, "before it is too late.", new Vector2(200, endingTextY + 375), Color.Yellow);
                spriteBatch.Draw(lukeTexture, lukeRect, Color.White);
                spriteBatch.Draw(vaderTexture, vaderRect, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}