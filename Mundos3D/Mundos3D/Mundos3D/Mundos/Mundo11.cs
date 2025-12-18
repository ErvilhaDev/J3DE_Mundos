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
    class Mundo11 : Mundo10
    {
        Game game;

        //PlanePixelShader planetexture;
        _Grid terrain;
        Bandeira agua;
        CubePixelShader cubetexture;

        MillPixelShader milltexture1;
        MillPixelShader milltexture2;

        private List<MillPixelShader> mills;

        Tower tower;

        Billboard[] trees;
        Vector3 treePosition;
        float[] treeDistances;
        int treeNumber;

        Camera camera;

        public Mundo11(GraphicsDevice device, Game game)
            : base(device, game)
        {
            this.device = device;
            this.world = Matrix.Identity;

            //this.planetexture = new PlanePixelShader(device, game);
            this.terrain = new _Grid(game, new Vector3(0, -2.2f, 0), new Vector3(0.4f, 0.1f, 0.4f));

            agua = new Bandeira(
                device,
                game,
                @"textures/t_water",
                @"shaders/MotionEffect",
                new Vector3(0, 0, -1),
                new Vector3(2f, 3f, 2f), -90f, 0f);

            this.cubetexture = new CubePixelShader(device, game);

            this.milltexture1 = new MillPixelShader(device, game);
            this.milltexture1.position = new Vector3(-3, 0, -1);
            this.milltexture1.rotation = 1f;

            this.milltexture2 = new MillPixelShader(device, game);
            this.milltexture2.position = new Vector3(3, 0, -1);
            this.milltexture2.rotation = -1f;

            // Cria lista de moinhos
            mills = new List<MillPixelShader>();
            // Adiciona os moinhos
            mills.Add(milltexture1);
            mills.Add(milltexture2);


            this.tower = new Tower(device, game);
            this.tower.position = new Vector3(0, 0, -4);
            this.tower.scale = new Vector3(0.04f);

            // Trees
            int rows = 1;
            int columns = 7;

            float spacingX = 1.5f;
            float spacingZ = 1.5f;

            Vector3 startPos = new Vector3(115, 1, -8);

            treeNumber = rows * columns;
            trees = new Billboard[treeNumber];

            int index = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Vector3 pos = new Vector3(
                        startPos.X + col * spacingX,
                        startPos.Y,
                        startPos.Z - row * spacingZ
                    );

                    trees[index] = new Billboard(device, game, pos);
                    index++;
                }
            }


        }

        public override void Update(GameTime gametime)
        {

            this.world = Matrix.CreateTranslation(120, 0, 0); //Scene position

            //this.planetexture.Update(gametime);
            //this.planetexture.MatrixWorld = this.world;
            this.terrain.Update(gametime);
            this.terrain.MatrixWorld = this.world;

            agua.MatrixWorld = this.world * this.MatrixWorld;
            agua.Update(gametime);

            this.cubetexture.Update(gametime);
            this.cubetexture.MatrixWorld = this.world;

            this.milltexture1.Update(gametime);
            this.milltexture1.MatrixWorld = this.world;

            this.milltexture2.Update(gametime);
            this.milltexture2.MatrixWorld = this.world;

            foreach (MillPixelShader mill in mills)
            {
                CBox collider = mill.GetCollider();


                if (collider != null && camera != null)
                {
                    if (collider.IsColliding(camera.GetBoundingBox()))
                    {

                        camera.RestorePosition();
                        break;
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }

            this.tower.Update(gametime);
            this.tower.MatrixWorld = this.world;

            if (camera != null)
            {
                foreach (Billboard tree in trees)
                {
                    tree.Update(gametime, camera.GetYaw());
                }
            }

        }

        public override void Draw(Camera camera)
        {
            this.camera = camera;

            //this.planetexture.Draw(camera);
            this.terrain.Draw(camera);
            this.agua.Draw(camera);
            this.cubetexture.Draw(camera);

            this.milltexture1.Draw(camera);
            this.milltexture2.Draw(camera);

            this.tower.Draw(camera);

            SortTrees(camera);

            foreach (Billboard tree in trees)
            {
                tree.Draw(camera);
            }
        }

        private void SortTrees(Camera camera)
        {
            Vector3 camPos = camera.GetPosition();


            for (int i = 0; i < trees.Length - 1; i++)
            {
                for (int j = 0; j < trees.Length - i - 1; j++)
                {
                    float distA = Vector3.Distance(camPos, trees[j].GetPosition());
                    float distB = Vector3.Distance(camPos, trees[j + 1].GetPosition());

                    // If tree[j] is closer than tree[j+1], swap
                    if (distA < distB)
                    {
                        Billboard temp = trees[j];
                        trees[j] = trees[j + 1];
                        trees[j + 1] = temp;
                    }
                }
            }
        }
    }
}
