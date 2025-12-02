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
    class CubeColor : GameObject
    {

        protected VertexPositionColor[] verts;

        public CubeColor(GraphicsDevice device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            MakeVertexCube();

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

        public void MakeVertexCube() 
        {
            this.verts = new VertexPositionColor[]
            {
                // FRONT
                new VertexPositionColor(new Vector3( -0.5f, 1,0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3( 0.5f,0,0.5f),Color.Green), //v1
                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.Blue),  //v2

                new VertexPositionColor(new Vector3( -0.5f, 1,0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3( 0.5f,1,0.5f),Color.Blue), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.Green),  //v2

                // RIGHT
                new VertexPositionColor(new Vector3( 0.5f, 1,0.5f),Color.Blue),   //v0
                new VertexPositionColor(new Vector3( 0.5f,0,-0.5f),Color.Red), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.Green),  //v2

                new VertexPositionColor(new Vector3( 0.5f, 1,0.5f),Color.Blue),   //v0
                new VertexPositionColor(new Vector3( 0.5f,1,-0.5f),Color.Green), //v1
                new VertexPositionColor(new Vector3(0.5f,0,-0.5f),Color.Red),  //v2

                // BACK
                new VertexPositionColor(new Vector3( 0.5f, 1,-0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3( -0.5f,1,-0.5f),Color.Green), //v1
                new VertexPositionColor(new Vector3(-0.5f,0,-0.5f),Color.Blue),  //v2

                new VertexPositionColor(new Vector3(0.5f, 1,-0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3( -0.5f,0,-0.5f),Color.Blue), //v1
                new VertexPositionColor(new Vector3(0.5f,0,-0.5f),Color.Green),  //v2

                //LEFT
                new VertexPositionColor(new Vector3(-0.5f,1,0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.Blue), //v1
                new VertexPositionColor(new Vector3(-0.5f,1,-0.5f),Color.Green),  //v2

                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.Blue),   //v0
                new VertexPositionColor(new Vector3(-0.5f,0,-0.5f),Color.Red), //v1
                new VertexPositionColor(new Vector3(-0.5f,1,-0.5f),Color.Green),  //v2

                //TOP
                new VertexPositionColor(new Vector3(-0.5f,1,0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3(-0.5f,1,-0.5f),Color.Green), //v1
                new VertexPositionColor(new Vector3(0.5f,1,0.5f),Color.Blue),  //v2

                new VertexPositionColor(new Vector3(-0.5f,1,-0.5f),Color.Green),   //v0
                new VertexPositionColor(new Vector3(0.5f,1,-0.5f),Color.Red), //v1
                new VertexPositionColor(new Vector3(0.5f,1,0.5f),Color.Blue),  //v2

                //BOTTOM
                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.Red),   //v0
                new VertexPositionColor(new Vector3(-0.5f,0,-0.5f),Color.Green), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.Blue),  //v2

                new VertexPositionColor(new Vector3(-0.5f,0,-0.5f),Color.Green),   //v0
                new VertexPositionColor(new Vector3(0.5f,0,-0.5f),Color.Red), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.Blue),  //v2
                
                
            };
        }
    }
}
