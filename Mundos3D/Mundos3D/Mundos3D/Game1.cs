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

namespace Mundos3D
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Camera camera;
        Screen screen;

        Game game;

        //Mundos
        Mundo mundo1;
        Mundo mundo2;
        Mundo mundo3;
        Mundo mundo5;
        Mundo mundo6;
        Mundo mundo7;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

        }


        protected override void Initialize()
        {
            this.screen = Screen.GetInstance();
            this.screen.SetWidth(graphics.PreferredBackBufferWidth);
            this.screen.SetHeight(graphics.PreferredBackBufferHeight);
            this.camera = new Camera(GraphicsDevice);

            //Mundos
            mundo1 = new Mundo1(GraphicsDevice, this);
            mundo2 = new Mundo2(GraphicsDevice, this);
            mundo3 = new Mundo3(GraphicsDevice, this);
            mundo5 = new Mundo5(GraphicsDevice, this);
            mundo6 = new Mundo6(GraphicsDevice, this);
            mundo7 = new Mundo7(GraphicsDevice, this);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
        }


        protected override void UnloadContent()
        {
            
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
                
            }

            this.camera.Update(gameTime);
            mundo1.Update(gameTime);
            mundo2.Update(gameTime);
            mundo3.Update(gameTime);
            mundo5.Update(gameTime);
            mundo6.Update(gameTime);
            mundo7.Update(gameTime);

            base.Update(gameTime);

            
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mundo1.Draw(camera);
            mundo2.Draw(camera);
            mundo3.Draw(camera);
            mundo5.Draw(camera);
            mundo6.Draw(camera);
            mundo7.Draw(camera);

            base.Draw(gameTime);

        }
    }
}
