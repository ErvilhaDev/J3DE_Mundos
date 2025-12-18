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
    class Billboard : GameObject
    {
        Game game;
        VertexPositionTexture[] verts;
        Texture2D texture;
        Vector3 position;

        public Billboard(GraphicsDevice device, Game game, Vector3 position)
        {
            this.game = game;
            this.device = device;
            this.position = position;

            this.world = Matrix.Identity;

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = false;

            MakeVertexTexture();

            this.texture = game.Content.Load<Texture2D>(@"textures\t_arvore");
        }

        public void Update(GameTime gametime, float angleY)
        {
            this.world = Matrix.Identity;
            this.world *= Matrix.CreateRotationY(angleY); 
            this.world *= Matrix.CreateTranslation(this.position);
        }

        public override void Draw(Camera camera)
        {
            effect.World = world * MatrixWorld;
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();
            effect.TextureEnabled = true;
            effect.Texture = this.texture;

            device.SetVertexBuffer(buffer);
            
            device.BlendState = BlendState.AlphaBlend;
            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                    this.verts, 0, this.verts.Length / 3);

            }
            device.BlendState = BlendState.Opaque;

            base.Draw(camera);

        }

        public void MakeVertexTexture()
        {
            this.verts = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-1, 1,0),Vector2.Zero),  //v0
                new VertexPositionTexture(new Vector3( 1, 1,0),Vector2.UnitX), //v1
                new VertexPositionTexture(new Vector3(-1,-1,0),Vector2.UnitY), //v2

                new VertexPositionTexture(new Vector3( 1, 1,0),Vector2.UnitX), //v3
                new VertexPositionTexture(new Vector3( 1,-1,0),Vector2.One),   //v4
                new VertexPositionTexture(new Vector3(-1,-1,0),Vector2.UnitY), //v5
            };
        
        }

        public Vector3 GetPosition()
        {
            return this.position;
        }
    }
}
