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
    class PlanePixelShader : PlaneTexture
    {
        Game game;

        protected VertexPositionTexture[] verts;
        protected Texture2D texture1;
        protected Texture2D texture2;
        protected GameTime gt;

        Effect shadereffect;

        public PlanePixelShader(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.game = game;

            MakeTriangleTexture();

            this.texture1 = game.Content.Load<Texture2D>(@"textures\t_grass");
            this.texture2 = game.Content.Load<Texture2D>(@"textures\t_snow");

            this.shadereffect = this.game.Content.Load<Effect>(@"shaders\TextureChange");
        }

        public override void Update(GameTime gametime)
        {
            this.gt = gametime;
        }

        public override void Draw(Camera camera)
        {

            shadereffect.CurrentTechnique = shadereffect.Techniques["Technique1"];
            shadereffect.Parameters["World"].SetValue(this.world * MatrixWorld);
            shadereffect.Parameters["View"].SetValue(camera.GetView());
            shadereffect.Parameters["Projection"].SetValue(camera.GetProjection());
            shadereffect.Parameters["texture1"].SetValue(this.texture1);
            shadereffect.Parameters["texture2"].SetValue(this.texture2);

            float time = (float)gt.TotalGameTime.TotalSeconds;
            float blend = (float)((Math.Sin(time) + 1) / 2.0);

            shadereffect.Parameters["BlendAmount"].SetValue(blend);

            foreach (EffectPass pass in this.shadereffect.CurrentTechnique.Passes)
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
