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
    class Mundo1 : Mundo
    {
        Triangle triangle;
        PlaneColor plane;
        CubeColor cubecolor;

        public Mundo1(GraphicsDevice device, Game game)
        {
            this.device = device;
            this.world = Matrix.Identity;

            this.triangle = new Triangle(device);
            this.plane = new PlaneColor(device);
            this.cubecolor = new CubeColor(device);
        }

        public override void Update(GameTime gametime)
        {
            this.world = Matrix.CreateTranslation(0, 0, 0);

            //this.triangle.Update(gametime);
            this.plane.Update(gametime);
            this.plane.MatrixWorld = this.world;
            this.cubecolor.Update(gametime);
            this.cubecolor.MatrixWorld = this.world;

            //plane.MatrixWorld = world;
            //cubecolor.MatrixWorld = world;
        }

        public override void Draw(Camera camera)
        {
            //this.triangle.Draw(camera);
            this.plane.Draw(camera);
            this.cubecolor.Draw(camera);
        }
    }
}
