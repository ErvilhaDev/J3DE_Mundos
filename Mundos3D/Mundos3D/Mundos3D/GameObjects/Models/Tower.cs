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
    class Tower : GameObject
    {
        Model model;
        Matrix modelMatrix;
        public Vector3 position = Vector3.Zero;
        public Vector3 scale;
        public float angleY;

        public Tower(GraphicsDevice device, Game game)
        {
            this.device = device;
            this.world = Matrix.Identity;
            this.MatrixWorld = Matrix.Identity;

            this.scale = new Vector3(0.05f);
            this.angleY = 0;

            this.model = game.Content.Load<Model>(@"models\XNA_Tower");
        }

        public override void Update(GameTime gametime)
        {
            this.world =
                Matrix.CreateScale(scale) *
                Matrix.CreateRotationY(MathHelper.ToRadians(angleY)) *
                Matrix.CreateTranslation(position);
        }

        public override void Draw(Camera camera)
        {
            // Final world transform
            Matrix worldMatrix =
                Matrix.CreateScale(scale) *
                Matrix.CreateRotationY(MathHelper.ToRadians(angleY)) *
                Matrix.CreateTranslation(position) *
                this.MatrixWorld;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect be in mesh.Effects)
                {
                    be.EnableDefaultLighting();
                    be.World = mesh.ParentBone.Transform * worldMatrix;
                    be.View = camera.GetView();
                    be.Projection = camera.GetProjection();
                }

                mesh.Draw();
            }
        }
    }
}
