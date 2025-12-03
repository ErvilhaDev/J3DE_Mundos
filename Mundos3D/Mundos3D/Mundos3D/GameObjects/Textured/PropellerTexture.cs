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
    class PropellerTexture : PropellerColor
    {
        protected VertexPositionTexture[] verts;
        protected Texture2D texture;

        //public Matrix millWorld;

        public PropellerTexture(GraphicsDevice device, Game game)
            : base(device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = false;

            MakeVertexTexture();

            this.texture = game.Content.Load<Texture2D>(@"textures\t_wood");
            
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

            effect.World = this.world * millWorld; //Important! This is what makes it attached to the Mill

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
                // -------- Blade 1 --------
                new VertexPositionTexture(new Vector3(-0.5f, 1f, 0f), new Vector2(0, 0)),   // left base
                new VertexPositionTexture(new Vector3( 0.5f, 1f, 0f), new Vector2(1, 0)),   // right base
                new VertexPositionTexture(new Vector3( 0f,  0f, 0f), new Vector2(0.5f, 1)), // center tip

                // -------- Blade 2 --------
                new VertexPositionTexture(new Vector3(1f,  0.5f, 0f), new Vector2(1, 0)),   // right base
                new VertexPositionTexture(new Vector3(1f, -0.5f, 0f), new Vector2(0, 0)),   // left base
                new VertexPositionTexture(new Vector3(0f,  0f, 0f), new Vector2(0.5f, 1)),  // center tip

                // -------- Blade 3 --------
                new VertexPositionTexture(new Vector3( 0.5f, -1f, 0f), new Vector2(1, 0)),  // right base
                new VertexPositionTexture(new Vector3(-0.5f, -1f, 0f), new Vector2(0, 0)),  // left base
                new VertexPositionTexture(new Vector3( 0f,   0f, 0f), new Vector2(0.5f, 1)), // center tip

                // -------- Blade 4 --------
                new VertexPositionTexture(new Vector3(-1f, -0.5f, 0f), new Vector2(0, 0)),  // left base
                new VertexPositionTexture(new Vector3(-1f,  0.5f, 0f), new Vector2(1, 0)),  // right base
                new VertexPositionTexture(new Vector3( 0f,  0f, 0f), new Vector2(0.5f, 1)), // center tip
            };
        }

    }
}
