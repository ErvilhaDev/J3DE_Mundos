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
    class Triangle : GameObject
    {
        protected VertexPositionColor[] verts;

        public Triangle(GraphicsDevice device)
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
                new VertexPositionColor (new Vector3(0,1,0),Color.LightGreen), //v0
                new VertexPositionColor (new Vector3(1,0,0),Color.LightGreen), //v1
                new VertexPositionColor (new Vector3(-1,0,0),Color.LightGreen), //v2
            };
            /*
            this.buffer = new VertexBuffer(device,
                                           typeof(VertexPositionColor),
                                           this.verts.Length,
                                           BufferUsage.None);
            this.buffer.SetData<VertexPositionColor>(this.verts);
            */

            
        }
    }
}
