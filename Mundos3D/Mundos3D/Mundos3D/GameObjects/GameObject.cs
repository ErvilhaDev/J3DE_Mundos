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
    class GameObject
    {
        protected GraphicsDevice device;
        protected Matrix world;
        protected VertexBuffer buffer;
        protected BasicEffect effect;

        public Matrix MatrixWorld = Matrix.Identity;

        public virtual void Update(GameTime gametime)
        {

        }

        public virtual void Draw(Camera camera)
        {
            effect.World = world * MatrixWorld; //calculo pra parentear
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();

            device.SetVertexBuffer(buffer);
        }
    }

}
