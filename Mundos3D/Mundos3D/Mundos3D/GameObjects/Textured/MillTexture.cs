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
    class MillTexture : MillColor
    {
        protected VertexPositionTexture[] verts;
        protected Texture2D texture;

        protected PropellerTexture propeller;

        public MillTexture(GraphicsDevice device, Game game) : base(device)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.MatrixWorld = Matrix.Identity;

            this.propeller = new PropellerTexture(device, game);

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = false;

            MakeVertexTexture();

            this.texture = game.Content.Load<Texture2D>(@"textures\t_stone");
        }

        public override void Update(GameTime gametime)
        {
            this.world =
                Matrix.CreateRotationY(rotation) *
                Matrix.CreateTranslation(position);

            propeller.millWorld = this.world * MatrixWorld;
            propeller.Update(gametime);

        }

        public override void Draw(Camera camera)
        {
            effect.World = world * MatrixWorld;
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();
            effect.TextureEnabled = true;
            effect.Texture = this.texture;

            device.SetVertexBuffer(buffer);

            propeller.Draw(camera);

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
                // ---------- FRONT ----------
                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(0,0)),

                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3( 0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,0)),

                // ---------- RIGHT ----------
                new VertexPositionTexture(new Vector3(0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(0.5f, 0,-1f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(0.5f, 0, 0.5f), new Vector2(1,0)),

                new VertexPositionTexture(new Vector3(0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(0.5f, 2,-0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(0.5f, 0,-1f), new Vector2(0,0)),

                // ---------- BACK ----------
                new VertexPositionTexture(new Vector3( 0.5f, 2,-0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-1f), new Vector2(0,0)),

                new VertexPositionTexture(new Vector3( 0.5f, 2,-0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-1f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0,-1f), new Vector2(1,0)),

                // ---------- LEFT ----------
                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,1)),

                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-1f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,1)),

                // ---------- TOP ----------
                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 2, 0.5f), new Vector2(1,1)),

                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 2,-0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 2, 0.5f), new Vector2(1,1)),

                // ---------- BOTTOM ----------
                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,1)),

                new VertexPositionTexture(new Vector3(-0.5f, 0,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0,-0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,1)),
            };
        }
    }
}
