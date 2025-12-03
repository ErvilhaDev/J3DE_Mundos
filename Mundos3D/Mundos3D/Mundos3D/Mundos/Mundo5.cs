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
    class Mundo5 : Mundo3
    {
        PlaneTexture planetexture;
        CubeTexture cubetexture;

        MillTexture milltexture1;
        MillTexture milltexture2;

        Tower tower;

        public Mundo5(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;

            
            this.planetexture = new PlaneTexture(device, game);
            this.cubetexture = new CubeTexture(device, game);

            this.milltexture1 = new MillTexture(device, game);
                this.milltexture1.position = new Vector3(-3, 0, -1);
                this.milltexture1.rotation = 1f;

            this.milltexture2 = new MillTexture(device, game);
                this.milltexture2.position = new Vector3(3, 0, -1);
                this.milltexture2.rotation = -1f;

            
            this.tower = new Tower(device, game);
                this.tower.position = new Vector3(0, 0, -4);
                this.tower.scale = new Vector3(0.04f);
        }

        public override void Update(GameTime gametime)
        {
            
            this.world = Matrix.CreateTranslation(30, 0, 0); //Scene position

            
            this.planetexture.Update(gametime);
            this.planetexture.MatrixWorld = this.world;

            this.cubetexture.Update(gametime);
            this.cubetexture.MatrixWorld = this.world;

            this.milltexture1.Update(gametime);
            this.milltexture1.MatrixWorld = this.world;

            this.milltexture2.Update(gametime);
            this.milltexture2.MatrixWorld = this.world;

            
            this.tower.Update(gametime);
            this.tower.MatrixWorld = this.world;
        }

        public override void Draw(Camera camera)
        {
            
            this.planetexture.Draw(camera);
            this.cubetexture.Draw(camera);

            this.milltexture1.Draw(camera);
            this.milltexture2.Draw(camera);

            
            this.tower.Draw(camera);
        }
    }
}
