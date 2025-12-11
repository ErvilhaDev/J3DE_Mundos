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
    class MillPixelShader : MillTexture
    {
        Game game;

        public Matrix millWorld;

        protected VertexPositionTexture[] verts;
        protected Texture2D texture1;
        protected Texture2D texture2;
        protected GameTime gt;

        Effect shadereffect;

        protected PropellerPixelShader s_propeller;

        CubeBoundingBox collider;


        public MillPixelShader(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.MatrixWorld = Matrix.Identity;
            this.game = game;

            this.s_propeller = new PropellerPixelShader(device, game);

            this.collider = new CubeBoundingBox(device);
            this.collider.scale = new Vector3(1f, 1.5f, 1f);
            this.collider.angle = 0f;
            this.collider.position = new Vector3(0, 1.5f, 0);

            MakeVertexTexture();

            this.texture1 = game.Content.Load<Texture2D>(@"textures\t_stone");
            this.texture2 = game.Content.Load<Texture2D>(@"textures\t_stonesnow");

            Effect baseEffect = this.game.Content.Load<Effect>(@"shaders\TextureChange");
            this.shadereffect = baseEffect.Clone();

        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            this.world =
                Matrix.CreateRotationY(rotation) *
                Matrix.CreateTranslation(position);


            Matrix millWorldFinal = this.world * this.MatrixWorld;
            s_propeller.millWorld = millWorldFinal;
            s_propeller.Update(gametime);

            Matrix colliderWorldFinal = this.world * this.MatrixWorld;
            this.collider.MatrixWorld = colliderWorldFinal;
            this.collider.Update(gametime);

            this.gt = gametime;

        }

        public override void Draw(Camera camera)
        {
            shadereffect.CurrentTechnique = shadereffect.Techniques["Technique1"];
            shadereffect.Parameters["World"].SetValue(world * MatrixWorld);
            shadereffect.Parameters["View"].SetValue(camera.GetView());
            shadereffect.Parameters["Projection"].SetValue(camera.GetProjection());
            shadereffect.Parameters["texture1"].SetValue(this.texture1);
            shadereffect.Parameters["texture2"].SetValue(this.texture2);

            float time = (float)gt.TotalGameTime.TotalSeconds;
            float blend = (float)((Math.Sin(time) + 1) / 2.0);

            shadereffect.Parameters["BlendAmount"].SetValue(blend);

            s_propeller.Draw(camera);
            this.collider.Draw(camera);

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
                //FRONT
                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(0,0)),

                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3( 0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3( 0.5f, 0, 0.5f), new Vector2(1,0)),

                //RIGHT
                new VertexPositionTexture(new Vector3(0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(0.5f, 0,-1f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(0.5f, 0, 0.5f), new Vector2(1,0)),

                new VertexPositionTexture(new Vector3(0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(0.5f, 2,-0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(0.5f, 0,-1f), new Vector2(0,0)),

                //BACK
                new VertexPositionTexture(new Vector3( 0.5f, 2,-0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-1f), new Vector2(0,0)),

                new VertexPositionTexture(new Vector3( 0.5f, 2,-0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-1f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 0,-1f), new Vector2(1,0)),

                //LEFT
                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(1,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,1)),

                new VertexPositionTexture(new Vector3(-0.5f, 0, 0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 0,-1f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,1)),

                //TOP
                new VertexPositionTexture(new Vector3(-0.5f, 2, 0.5f), new Vector2(0,1)),
                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 2, 0.5f), new Vector2(1,1)),

                new VertexPositionTexture(new Vector3(-0.5f, 2,-0.5f), new Vector2(0,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 2,-0.5f), new Vector2(1,0)),
                new VertexPositionTexture(new Vector3( 0.5f, 2, 0.5f), new Vector2(1,1)),

                //BOTTOM
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
