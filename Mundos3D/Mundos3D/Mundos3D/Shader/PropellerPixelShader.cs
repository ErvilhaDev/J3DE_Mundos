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
    class PropellerPixelShader : PropellerTexture
    {
        Game game;

        protected VertexPositionTexture[] verts;
        protected Texture2D texture1;
        protected Texture2D texture2;
        protected GameTime gt;

        Effect shadereffect;

        public PropellerPixelShader(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.game = game;

            MakeVertexTexture();

            this.texture1 = game.Content.Load<Texture2D>(@"textures\t_wood");
            this.texture2 = game.Content.Load<Texture2D>(@"textures\t_woodsnow");

            Effect baseEffect = this.game.Content.Load<Effect>(@"shaders\TextureChange");
            this.shadereffect = baseEffect.Clone();

        }

        public override void Update(GameTime gametime)
        {
            this.gt = gametime;
            base.Update(gametime);
        }

        public override void Draw(Camera camera)
        {
            shadereffect.CurrentTechnique = shadereffect.Techniques["Technique1"];

            shadereffect.Parameters["World"].SetValue(this.world * millWorld);
            shadereffect.Parameters["View"].SetValue(camera.GetView());
            shadereffect.Parameters["Projection"].SetValue(camera.GetProjection());
            shadereffect.Parameters["texture1"].SetValue(this.texture1);
            shadereffect.Parameters["texture2"].SetValue(this.texture2);
            shadereffect.Parameters["World"].SetValue(this.world * millWorld);

            float time = (float)gt.TotalGameTime.TotalSeconds;
            float blend = (float)((Math.Sin(time) + 1) / 2.0);

            shadereffect.Parameters["BlendAmount"].SetValue(blend);

            //effect.World = this.world * millWorld; //Important! This is what makes it attached to the Mill


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
                //Blade 1
                new VertexPositionTexture(new Vector3(-0.5f, 1f, 0f), new Vector2(0, 0)),   // left base
                new VertexPositionTexture(new Vector3( 0.5f, 1f, 0f), new Vector2(1, 0)),   // right base
                new VertexPositionTexture(new Vector3( 0f,  0f, 0f), new Vector2(0.5f, 1)), // center tip

                //Blade 2
                new VertexPositionTexture(new Vector3(1f,  0.5f, 0f), new Vector2(1, 0)),   // right base
                new VertexPositionTexture(new Vector3(1f, -0.5f, 0f), new Vector2(0, 0)),   // left base
                new VertexPositionTexture(new Vector3(0f,  0f, 0f), new Vector2(0.5f, 1)),  // center tip

                //Blade 3
                new VertexPositionTexture(new Vector3( 0.5f, -1f, 0f), new Vector2(1, 0)),  // right base
                new VertexPositionTexture(new Vector3(-0.5f, -1f, 0f), new Vector2(0, 0)),  // left base
                new VertexPositionTexture(new Vector3( 0f,   0f, 0f), new Vector2(0.5f, 1)), // center tip

                //Blade 4
                new VertexPositionTexture(new Vector3(-1f, -0.5f, 0f), new Vector2(0, 0)),  // left base
                new VertexPositionTexture(new Vector3(-1f,  0.5f, 0f), new Vector2(1, 0)),  // right base
                new VertexPositionTexture(new Vector3( 0f,  0f, 0f), new Vector2(0.5f, 1)), // center tip
            };
        }

    }
}
