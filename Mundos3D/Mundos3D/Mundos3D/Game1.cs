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
        BasicEffect effect;

        public Camera camera;
        Screen screen;

        Game game;

        Mundo[] mundos;

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
            this.camera = new Camera(GraphicsDevice, this);
            this.effect = new BasicEffect(GraphicsDevice);

            mundos = new Mundo[]
            {
                new Mundo1(GraphicsDevice, this),
                new Mundo2(GraphicsDevice, this),
                new Mundo3(GraphicsDevice, this),
                new Mundo5(GraphicsDevice, this),
                new Mundo6(GraphicsDevice, this),
                new Mundo7(GraphicsDevice, this),
                new Mundo8(GraphicsDevice, this),
                new Mundo9(GraphicsDevice, this),
                new Mundo10(GraphicsDevice, this),
                new Mundo11(GraphicsDevice, this)
            };

            ////Mundos
            //mundo1 = new Mundo1(GraphicsDevice, this);
            //mundo2 = new Mundo2(GraphicsDevice, this);
            //mundo3 = new Mundo3(GraphicsDevice, this);
            //mundo5 = new Mundo5(GraphicsDevice, this);
            //mundo6 = new Mundo6(GraphicsDevice, this);
            //mundo7 = new Mundo7(GraphicsDevice, this);

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

            foreach (Mundo mundo in mundos)
            {
                mundo.Update(gameTime);
            }

            
            base.Update(gameTime);

            
        }


        protected override void Draw(GameTime gameTime)
        {
           GraphicsDevice.Clear(Color.CornflowerBlue);

           foreach (Mundo mundo in mundos)
           {
               mundo.Draw(camera);
           }

           camera.Draw(effect, camera);
           base.Draw(gameTime);

        }
    }
}
