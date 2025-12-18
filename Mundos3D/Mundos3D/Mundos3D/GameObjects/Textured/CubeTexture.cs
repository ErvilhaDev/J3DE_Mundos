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
    class CubeTexture : CubeColor
    {
        protected VertexPositionTexture[] verts;
        protected Texture2D texture;

        public CubeTexture(GraphicsDevice device, Game game) : base(device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = false;

            MakeVertexTexture();

            this.texture = game.Content.Load<Texture2D>(@"textures\t_brick");
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public override void Draw(Camera camera)
        {
            effect.World = world * MatrixWorld;
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();
            effect.TextureEnabled = true;
            effect.Texture = this.texture;
            

            device.SetVertexBuffer(buffer);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                    this.verts, 0, this.verts.Length / 3);

            }
        }

        public void MakeVertexTexture()
        { 
            this.verts = new VertexPositionTexture[]
            {
                // FRONT
                new VertexPositionTexture(new Vector3( -0.5f, 1,0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3( 0.5f,0,0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-0.5f,0,0.5f), new Vector2(1,0)),

                new VertexPositionTexture(new Vector3( -0.5f, 1,0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3( 0.5f,1,0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(0.5f,0,0.5f), new Vector2(0,0)),

                // RIGHT
                new VertexPositionTexture(new Vector3( 0.5f, 1,0.5f),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3( 0.5f,0,-0.5f),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(0.5f,0,0.5f),new Vector2(1,0)),

                new VertexPositionTexture(new Vector3( 0.5f, 1,0.5f),new Vector2(1,1)),
                new VertexPositionTexture(new Vector3( 0.5f,1,-0.5f),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(0.5f,0,-0.5f),new Vector2(0,0)),

                // BACK  (FIXED UVs)
                new VertexPositionTexture(new Vector3( 0.5f, 1,-0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 1,-0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-0.5f), new Vector2(1,0)),

                new VertexPositionTexture(new Vector3( 0.5f, 1,-0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0,-0.5f), new Vector2(0,0)),

                //LEFT
                new VertexPositionTexture(new Vector3(-0.5f,1,0.5f),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f,0,0.5f),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-0.5f,1,-0.5f),new Vector2(1,1)),

                new VertexPositionTexture(new Vector3(-0.5f,0,0.5f),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-0.5f,0,-0.5f),new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f,1,-0.5f),new Vector2(1,1)),

                //TOP
                new VertexPositionTexture(new Vector3(-0.5f,1,0.5f),new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f,1,-0.5f),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(0.5f,1,0.5f),new Vector2(1,1)),

                new VertexPositionTexture(new Vector3(-0.5f,1,-0.5f),new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(0.5f,1,-0.5f),new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(0.5f,1,0.5f),new Vector2(1,1)),

                // BOTTOM (FIXED UVs)
                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,1)),

                new VertexPositionTexture(new Vector3(-0.5f, 0,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0,-0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,1))
            };
        }
    }
}
