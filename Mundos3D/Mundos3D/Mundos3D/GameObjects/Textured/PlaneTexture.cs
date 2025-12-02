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
    class PlaneTexture : PlaneColor
    {
        protected VertexPositionTexture[] verts;
        
        protected Texture2D texture;

        public PlaneTexture(GraphicsDevice device, Game game) : base(device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = false;


            MakeTriangleTexture();

            this.texture = game.Content.Load<Texture2D>(@"textures\t_grass");
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public override void Draw(Camera camera)
        {
            //base.Draw(camera);
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

        public void MakeTriangleTexture()
        {
            this.verts = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-5,  0, -5), new Vector2(0,0)),   //v0
                new VertexPositionTexture(new Vector3( 5, 0, -5), new Vector2(1,0)), //v1
                new VertexPositionTexture(new Vector3(-5, 0, 5), new Vector2(0,1)),  //v2

                new VertexPositionTexture(new Vector3(5,  0, -5), new Vector2(1,0)),   //v0
                new VertexPositionTexture(new Vector3( 5,  0, 5), new Vector2(1,1)), //v1
                new VertexPositionTexture(new Vector3(-5, 0, 5), new Vector2(0,1)),  //v2
            };
        }
    }
}
