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
    class Mundo3 : Mundo2
    {
        PlaneTexture planetexture;

        public Mundo3(GraphicsDevice device, Game game) : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;

            this.planetexture = new PlaneTexture(device, game);

        }

        public override void Update(GameTime gametime)
        {
            // Apply the scene world FIRST
            this.world = Matrix.CreateTranslation(20, 0, 0);

            this.planetexture.Update(gametime);
            this.planetexture.MatrixWorld = this.world;

        }

        public override void Draw(Camera camera)
        {
            this.planetexture.Draw(camera);
        }
    }
}
