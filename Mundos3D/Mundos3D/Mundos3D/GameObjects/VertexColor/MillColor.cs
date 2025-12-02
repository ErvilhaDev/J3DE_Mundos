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
    class MillColor : GameObject
    {

        protected VertexPositionColor[] verts;
        PropellerColor propeller;

        public Vector3 position = Vector3.Zero;
        public float rotation;

        public MillColor(GraphicsDevice device)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.MatrixWorld = Matrix.Identity;

            this.propeller = new PropellerColor(device);


            MakeVertexCube();

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = true;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            
            this.world =
                Matrix.CreateRotationY(rotation) *
                Matrix.CreateTranslation(position);
            

            propeller.millWorld = this.world * MatrixWorld;

            propeller.Update(gametime);
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);

            effect.World = this.world * MatrixWorld;
            
            propeller.Draw(camera);

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
                new VertexPositionColor(new Vector3( -0.5f, 2,0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3( 0.5f,0,0.5f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.DarkGray),  //v2

                new VertexPositionColor(new Vector3( -0.5f, 2,0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3( 0.5f,2,0.5f),Color.White), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.DarkGray),  //v2

                // RIGHT
                new VertexPositionColor(new Vector3( 0.5f, 2,0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3( 0.5f,0,-1f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.DarkGray),  //v2

                new VertexPositionColor(new Vector3( 0.5f, 2,0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3( 0.5f,2,-0.5f),Color.White), //v1
                new VertexPositionColor(new Vector3(0.5f,0,-1f),Color.DarkGray),  //v2

                // BACK
                new VertexPositionColor(new Vector3( 0.5f, 2,-0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3( -0.5f,2,-0.5f),Color.White), //v1
                new VertexPositionColor(new Vector3(-0.5f,0,-1f),Color.DarkGray),  //v2

                new VertexPositionColor(new Vector3(0.5f, 2,-0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3( -0.5f,0,-1f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(0.5f,0,-1f),Color.DarkGray),  //v2

                //LEFT
                new VertexPositionColor(new Vector3(-0.5f,2,0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(-0.5f,2,-0.5f),Color.White),  //v2

                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.DarkGray),   //v0
                new VertexPositionColor(new Vector3(-0.5f,0,-1f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(-0.5f,2,-0.5f),Color.White),  //v2

                //TOP
                new VertexPositionColor(new Vector3(-0.5f,2,0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3(-0.5f,2,-0.5f),Color.White), //v1
                new VertexPositionColor(new Vector3(0.5f,2,0.5f),Color.White),  //v2

                new VertexPositionColor(new Vector3(-0.5f,2,-0.5f),Color.White),   //v0
                new VertexPositionColor(new Vector3(0.5f,2,-0.5f),Color.White), //v1
                new VertexPositionColor(new Vector3(0.5f,2,0.5f),Color.White),  //v2

                //BOTTOM
                new VertexPositionColor(new Vector3(-0.5f,0,0.5f),Color.DarkGray),   //v0
                new VertexPositionColor(new Vector3(-0.5f,0,-0.5f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.DarkGray),  //v2

                new VertexPositionColor(new Vector3(-0.5f,0,-0.5f),Color.DarkGray),   //v0
                new VertexPositionColor(new Vector3(0.5f,0,-0.5f),Color.DarkGray), //v1
                new VertexPositionColor(new Vector3(0.5f,0,0.5f),Color.DarkGray),  //v2
                
                
            };
        }
    }
}
