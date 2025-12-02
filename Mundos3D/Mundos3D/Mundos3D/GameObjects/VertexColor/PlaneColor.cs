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
    class PlaneColor : GameObject
    {
        protected VertexPositionColor[] verts;

        public PlaneColor(GraphicsDevice device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            MakeTriangle();

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = true;

        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,
                                                                    this.verts, 0, verts.Length / 3);
            }


        }

        public void MakeTriangle()
        {
            this.verts = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(-5,  0, -5), Color.Green),
                new VertexPositionColor(new Vector3( 5, 0, -5), Color.LightGreen),
                new VertexPositionColor(new Vector3(-5, 0, 5), Color.LightGreen),

                new VertexPositionColor(new Vector3(5,  0, -5), Color.LightGreen),
                new VertexPositionColor(new Vector3( 5,  0, 5), Color.Green),
                new VertexPositionColor(new Vector3(-5, 0, 5), Color.LightGreen)
            };
        }
    }
}
