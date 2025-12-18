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
    class CubePixelShader : CubeTexture
    {
        Game game;
        
        protected VertexPositionTexture[] verts;
        protected Texture2D texture1;
        protected Texture2D texture2;
        protected GameTime gt;

        Effect shadereffect;

        public BasicEffect debugEffect;

        public CubePixelShader(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.game = game;

            debugEffect = new BasicEffect(device)
            {
                VertexColorEnabled = true
            };



            MakeVertexTexture();

            this.texture1 = game.Content.Load<Texture2D>(@"textures\t_brick");
            this.texture2 = game.Content.Load<Texture2D>(@"textures\t_bricksnow");

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
