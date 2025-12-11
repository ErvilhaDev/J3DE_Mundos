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
    class Mundo7 : Mundo6
    {
        PlanePixelShader planetexture;
        CubePixelShader cubetexture;

        MillPixelShader milltexture1;
        MillPixelShader milltexture2;

        //public CubeBoundingBox collisiontext;

        Tower tower;

        Camera camera;

        public Mundo7(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;


            //this.collisiontext = new CubeBoundingBox(device);
            //    this.collisiontext.scale = new Vector3(0.5f,0.5f,0.5f);
            //    this.collisiontext.angle = 0f;
            //    this.collisiontext.position = new Vector3(0,0.5f,0);

            this.planetexture = new PlanePixelShader(device, game);
            this.cubetexture = new CubePixelShader(device, game);

            this.milltexture1 = new MillPixelShader(device, game);
            this.milltexture1.position = new Vector3(-3, 0, -1);
            this.milltexture1.rotation = 1f;

            this.milltexture2 = new MillPixelShader(device, game);
            this.milltexture2.position = new Vector3(3, 0, -1);
            this.milltexture2.rotation = -1f;


            this.tower = new Tower(device, game);
            this.tower.position = new Vector3(0, 0, -4);
            this.tower.scale = new Vector3(0.04f);
        }

        public override void Update(GameTime gametime)
        {

            this.world = Matrix.CreateTranslation(50, 0, 0); //Scene position

            //this.collisiontext.Update(gametime);
            //this.collisiontext.MatrixWorld = this.world;

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
            this.camera = camera;

            this.planetexture.Draw(camera);
            this.cubetexture.Draw(camera);

            this.milltexture1.Draw(camera);
            this.milltexture2.Draw(camera);

            //this.collisiontext.Draw(camera);

            this.tower.Draw(camera);
        }
    }
}
