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
    class PropellerColor : GameObject
    {

        protected VertexPositionColor[] verts;
        float rotateAngle;
        float rotateSpeed = 2f;

        public Matrix millWorld;

        Random random;

        public PropellerColor(GraphicsDevice device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            random = new Random();
            rotateSpeed = 1f + (float)random.NextDouble() * 3f;

            MakeVertexPropeller();

            effect = new BasicEffect(device);
            effect.VertexColorEnabled = true;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            RotateVertexPropeller(gametime);
            PositionVertexPropeller();
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);


            effect.World = this.world * millWorld;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,
                                                                    this.verts, 0, verts.Length / 3);
            }
        }

        public void MakeVertexPropeller()
        {
            this.verts = new VertexPositionColor[]
            {
                // Blade 1
                new VertexPositionColor(new Vector3(-0.5f, 1f, 0), Color.Orange),
                new VertexPositionColor(new Vector3( 0.5f, 1f, 0), Color.Orange),
                new VertexPositionColor(new Vector3( 0f, 0f, 0), Color.DarkOrange),

                // Blade 2
                new VertexPositionColor(new Vector3(1f, 0.5f, 0f), Color.Orange),
                new VertexPositionColor(new Vector3(1f, -0.5f, 0f), Color.Orange),
                new VertexPositionColor(new Vector3(0f, 0f, 0f), Color.DarkOrange),

                // Blade 3
                new VertexPositionColor(new Vector3(0.5f, -1f, 0f), Color.Orange),
                new VertexPositionColor(new Vector3(-0.5f, -1f, 0f), Color.Orange),
                new VertexPositionColor(new Vector3(0f,  0f, 0f), Color.DarkOrange),

                // Blade 4
                new VertexPositionColor(new Vector3(-1f, -0.5f, 0f), Color.Orange),
                new VertexPositionColor(new Vector3(-1f,  0.5f, 0f), Color.Orange),
                new VertexPositionColor(new Vector3( 0f,  0f, 0f), Color.DarkOrange),

            };
        }

        public void RotateVertexPropeller(GameTime gametime) 
        {
            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;

            rotateAngle += rotateSpeed * dt;

            this.world = Matrix.CreateRotationZ(rotateAngle);
        }

        public void PositionVertexPropeller() 
        {
            this.world *= Matrix.CreateTranslation(0f, 1.5f, 0.6f);
        }
    }
}
