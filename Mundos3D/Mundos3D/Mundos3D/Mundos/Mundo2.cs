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
    class Mundo2 : Mundo1
    {
        protected MillColor mill1;
        protected MillColor mill2;

        public Mundo2(GraphicsDevice device, Game game) : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;

            this.mill1 = new MillColor(device);
            this.mill1.position = new Vector3(-3, 0, -1);
            this.mill1.rotation = 1f;

            this.mill2 = new MillColor(device);
            this.mill2.position = new Vector3(3, 0, -1);
            this.mill2.rotation = -1f;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            this.mill1.Update(gametime);
            this.mill2.Update(gametime);

            this.world = Matrix.CreateTranslation(10,0,0);

            mill1.MatrixWorld = this.world;
            mill2.MatrixWorld = this.world;
        }

        public override void Draw(Camera camera)
        {
            base.Draw(camera);
            this.mill1.Draw(camera);
            this.mill2.Draw(camera);
        }
    }
}
