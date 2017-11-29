////////////////////////////////////////////////////////////////
// Copyright 2013, CompuScholar, Inc.
//
// This source code is for use by the students and teachers who 
// have purchased the corresponding TeenCoder or KidCoder product.
// It may not be transmitted to other parties for any reason
// without the written consent of CompuScholar, Inc.
// This source is provided as-is for educational purposes only.
// CompuScholar, Inc. makes no warranty and assumes
// no liability regarding the functionality of this program.
//
////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace CatAndMouse
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CatAndMouse : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        // old keyboard, mouse, and gamepad states track the device states
        // from the previous call to Update()
        KeyboardState oldKeyState;
        MouseState oldMouseState;

        GamePadState oldGamePadState1;
        GamePadState oldGamePadState2;

        // these textures will hold the images of the cat and mouse
        Texture2D catTexture;
        Texture2D mouseTexture;

        // initialize new random number generator for teleport feature
        Random random = new System.Random();

        // these vectors will hold the X,Y coordinate pairs for the cat and mouse
        Vector2 catLocation;
        Vector2 mouseLocation;

        // these variables will hold the client window's width and height
        int screenWidth;
        int screenHeight;

        // This method is provided complete as part of the activity starter
        public CatAndMouse()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        // This method is provided complete as part of the activity starter
        protected override void Initialize()
        {
            // initialize screen width and screen height variables for future reference
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;
            
            this.IsMouseVisible = true; // make the mouse visible on the screen

            // set starting positions of cat and mouse
            catLocation = new Vector2(random.Next(0, screenWidth), random.Next(0, screenHeight));
            mouseLocation = new Vector2(random.Next(0, screenWidth), random.Next(0, screenHeight));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        // This method is provided complete as part of the activity starter
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load the cat and mouse textures
            catTexture = Content.Load<Texture2D>("cat");
            mouseTexture = Content.Load<Texture2D>("mouse");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        // This method is provided complete as part of the activity starter
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        // This method is provided complete as part of the activity starter
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // process all keyboard input
            checkKeyboardInput();

            // process all mouse input
            checkMouseInput();

            // optionally process input from Xbox Controllers
            checkGamepad1Input();
            checkGamepad2Input();

            base.Update(gameTime);
        }

        // This method will process all keyboard input and move the cat accordingly.
        // The student will complete this method.
        private void checkKeyboardInput()
        {

            GamePadState CurrentGamePadState = GamePad.GetState(PlayerIndex.Two);
            if (CurrentGamePadState.IsConnected == false)
                return;

            KeyboardState CurrentKeyState = Keyboard.GetState();
            if (oldKeyState == null)
                oldKeyState = CurrentKeyState;

            if (CurrentKeyState.IsKeyDown(Keys.W))
            {
                catLocation.Y = catLocation.Y - 3;
            }
            if (CurrentKeyState.IsKeyDown(Keys.S))
            {
                catLocation.Y = catLocation.Y + 3;
            }
            if (CurrentKeyState.IsKeyDown(Keys.A))
            {
                catLocation.X = catLocation.X - 3;
            }
            if (CurrentKeyState.IsKeyDown(Keys.D))
            {
                catLocation.X = catLocation.X + 3;
            }

            if ((oldKeyState.IsKeyUp(Keys.Space)) && (CurrentKeyState.IsKeyDown(Keys.Space)))
            {
                catLocation.Y = random.Next(0, screenHeight);
                catLocation.X = random.Next(0, screenWidth);
            }

            oldKeyState = CurrentKeyState;

        }

        // This method will process all mouse input and move the mouse accordingly.
        // The student will complete this method.
        private void checkMouseInput()
        {

            GamePadState CurrentGamePadState = GamePad.GetState(PlayerIndex.Two);
            if (CurrentGamePadState.IsConnected == true)
                return;

            MouseState CurrentMouseState = Mouse.GetState();
            if (oldMouseState == null)
                oldMouseState = CurrentMouseState;

            Vector2 MouseCenter = new Vector2(mouseLocation.X + mouseTexture.Width / 2, mouseLocation.Y + mouseTexture.Height / 2);

            if (CurrentMouseState.Y >= MouseCenter.Y)
            {
                mouseLocation.Y = mouseLocation.Y + 3;
            }
            if (CurrentMouseState.Y <= MouseCenter.Y)
            {
                mouseLocation.Y = mouseLocation.Y - 3;
            }
            if (CurrentMouseState.X >= MouseCenter.X)
            {
                mouseLocation.X = mouseLocation.X + 3;
            }
            if (CurrentMouseState.X <= MouseCenter.X)
            {
                mouseLocation.X = mouseLocation.X - 3;
            }

            if ((oldMouseState.LeftButton == ButtonState.Released) && (CurrentMouseState.LeftButton == ButtonState.Pressed))
            {
                mouseLocation.Y = random.Next(0, screenHeight);
                mouseLocation.X = random.Next(0, screenWidth);
            }

            oldMouseState = CurrentMouseState;

        }

        //***************************************************************************************************
        // The following two methods can be completed by the student if they would like to use
        // one or two Xbox Controller gamepads. If the student is not using gamepads, they will not
        // need to complete these methods

        // This method will be completed if the student is using a gamepad for player 1 (the cat)
        private void checkGamepad1Input()
        {

            GamePadState CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
            if (CurrentGamePadState.IsConnected == false)
                return;

            if (oldGamePadState1 == null)
                oldGamePadState1 = CurrentGamePadState;

            if (CurrentGamePadState.ThumbSticks.Left.Y >= 0)
            {
                catLocation.Y = catLocation.Y - 3;
            }
            if (CurrentGamePadState.ThumbSticks.Left.Y <= 0)
            {
                catLocation.Y = catLocation.Y + 3;
            }
            if (CurrentGamePadState.ThumbSticks.Left.X <= 0)
            {
                catLocation.X = catLocation.X - 3;
            }
            if (CurrentGamePadState.ThumbSticks.Left.X >= 0)
            {
                catLocation.X = catLocation.X + 3;
            }

            if ((CurrentGamePadState.Buttons.A == ButtonState.Pressed) && (oldGamePadState1.Buttons.A == ButtonState.Released))
            {
                catLocation.Y = random.Next(0, screenHeight);
                catLocation.X = random.Next(0, screenWidth);
            }

            oldGamePadState1 = CurrentGamePadState;
        }


        // This method will be completed if the student is using a gamepad for player 2 (the mouse)
        private void checkGamepad2Input()
        {

            GamePadState CurrentGamePadState = GamePad.GetState(PlayerIndex.Two);
            if (CurrentGamePadState.IsConnected == false)
                return;

            if (oldGamePadState2 == null)
                oldGamePadState2 = CurrentGamePadState;

            if (CurrentGamePadState.ThumbSticks.Left.Y >= 0)
            {
                mouseLocation.Y = mouseLocation.Y - 3;
            }
            if (CurrentGamePadState.ThumbSticks.Left.Y <= 0)
            {
                mouseLocation.Y = mouseLocation.Y + 3;
            }
            if (CurrentGamePadState.ThumbSticks.Left.X <= 0)
            {
                mouseLocation.X = mouseLocation.X - 3;
            }
            if (CurrentGamePadState.ThumbSticks.Left.X >= 0)
            {
                mouseLocation.X = mouseLocation.X + 3;
            }

            if ((CurrentGamePadState.Buttons.A == ButtonState.Pressed) && (oldGamePadState2.Buttons.A == ButtonState.Released))
            {
                mouseLocation.Y = random.Next(0, screenHeight);
                mouseLocation.X = random.Next(0, screenWidth);
            }

            oldGamePadState2 = CurrentGamePadState;
        }
        //****************************************************************************************************************************

                /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        // This method is provided complete as part of the activity starter
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);//CornflowerBlue);

            // start the spritebatch
            spriteBatch.Begin();

            // draw the cat and mouse textures at their current location
            spriteBatch.Draw(mouseTexture, mouseLocation, Color.White);
            spriteBatch.Draw(catTexture, catLocation, Color.White);

            // all done drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
