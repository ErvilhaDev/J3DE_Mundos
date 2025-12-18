
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mundos3D
{
    class _Grid : GameObject
    {
        Game game;
        VertexPositionNormalTexture[] vertex;
        VertexBuffer vBuffer;
        int[] indices;
        IndexBuffer iBuffer;
        Texture2D hmTexture, texture;
        BasicEffect effect;

        int row, col;
        Color[] color;

        // NORMAL PARA VISUALIZAÇÃO
        VertexPositionColor[] nVertex;
        VertexBuffer nBuffer;
        BasicEffect nEffect;

        Vector3 position;
        Vector3 scale;

        public _Grid(Game game, Vector3 position, Vector3 scale)
        {
            this.game = game;

            this.row = 100;
            this.col = 100;

            this.position = position;
            this.scale = scale;

            this.SetupMatrix();
            this.LoadHeightmapData();
            this.CreateVertex();
            this.CreateVertexBuffer();
            this.CreateIndices();
            this.CreateIndexBuffer();
            this.RecalculateNormals();
            this.NormalizeNormals();
            this.LoadTexture();
            this.CreateEffect();

            this.position = position;
            this.scale = scale;

            // NORMALS
            //this.CreateNormals();
            //this.CreateNormalVertexBuffer();
            //this.CreateNormalEffect();
        }

        public void Update(GameTime gt)
        {
            //this.world *= Matrix.CreateRotationY(0.01f);
            this.world =
                Matrix.CreateScale(this.scale) *
                Matrix.CreateTranslation(this.position);
        }

        public void Draw(Camera camera)
        {
            this.game.GraphicsDevice.SetVertexBuffer(this.vBuffer);
            this.game.GraphicsDevice.Indices = this.iBuffer;

            this.effect.World = this.world * this.MatrixWorld;
            this.effect.View = camera.GetView();
            this.effect.Projection = camera.GetProjection();
            this.effect.TextureEnabled = true;
            this.effect.Texture = this.texture;

            this.effect.EnableDefaultLighting();
            //this.effect.DirectionalLight0.Enabled = true;
            //this.effect.DirectionalLight0.DiffuseColor = Color.Yellow.ToVector3();
            //this.effect.DirectionalLight0.SpecularColor = Color.Yellow.ToVector3();
            //this.effect.DirectionalLight0.Direction = new Vector3(-0,-1,0);
            this.effect.SpecularPower = 10;
            this.effect.SpecularColor = Color.Purple.ToVector3();

            this.effect.FogEnabled = true;
            this.effect.FogColor = Color.DarkGreen.ToVector3();
            this.effect.FogStart = 30;
            this.effect.FogEnd = 80;


            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.game.GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(PrimitiveType.TriangleList,
                                                  this.vertex,
                                                  0,
                                                  this.vertex.Length,
                                                  this.indices,
                                                  0,
                                                  this.indices.Length / 3);
            }

            //this.DrawNormals(camera);
        }

        private void SetupMatrix()
        {
            this.world = Matrix.Identity;
        }

        private void LoadHeightmapData()
        {
            this.hmTexture = this.game.Content.Load<Texture2D>(@"Textures\t_hm");

            this.color = new Color[this.hmTexture.Width * this.hmTexture.Height];
            this.hmTexture.GetData<Color>(this.color);
        }

        private void CreateVertex()
        {
            this.vertex = new VertexPositionNormalTexture[row * col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    float u = (float)j / (col - 1);
                    float v = (float)i / (row-1);

                    int _u = (int)(u * (this.hmTexture.Width-1));
                    int _v = (int)(v * (this.hmTexture.Height-1));
                    int _index = (int)(_v * this.hmTexture.Width + _u);

                    float aux = 10;
                    int index = i * col + j;
                    this.vertex[index] = new VertexPositionNormalTexture(
                        new Vector3(j - (col-1)/2f, color[_index].R/10f, i - (row-1)/2f),
                        Vector3.UnitY,
                        new Vector2(u * aux, v * aux)); 
                }
            }
        }

        private void CreateVertexBuffer()
        {
            this.vBuffer = new VertexBuffer(this.game.GraphicsDevice,
                                            typeof(VertexPositionNormalTexture),
                                            this.vertex.Length,
                                            BufferUsage.None);
            this.vBuffer.SetData<VertexPositionNormalTexture>(this.vertex);
        }

        private void CreateIndices()
        {
            this.indices = new int[(row - 1) * (col - 1) * 2 * 3];

            int k = 0;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < col - 1; j++)
                {
                    this.indices[k++] = i * col + j; //0
                    this.indices[k++] = i * col + (j + 1); //1
                    this.indices[k++] = (i + 1) * col + j; //2

                    this.indices[k++] = i * col + (j + 1); //1
                    this.indices[k++] = (i + 1) * col + (j + 1); //3
                    this.indices[k++] = (i + 1) * col + j; //2
                }
            }
        }

        private void CreateIndexBuffer()
        {
            this.iBuffer = new IndexBuffer(this.game.GraphicsDevice,
                                           IndexElementSize.ThirtyTwoBits,
                                            this.indices.Length,
                                            BufferUsage.None);
            this.iBuffer.SetData<int>(this.indices);
        }

        private void LoadTexture()
        {
            this.texture = this.game.Content.Load<Texture2D>(@"Textures\t_grass");
        }

        private void CreateEffect()
        {
            this.effect = new BasicEffect(this.game.GraphicsDevice);
        }

        private void CreateNormals()
        {
            this.nVertex = new VertexPositionColor[row * col * 2];

            int k = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int index = i * col + j;
                    this.nVertex[k++] = new VertexPositionColor(this.vertex[index].Position,Color.Green);
                    this.nVertex[k++] = new VertexPositionColor(this.vertex[index].Position + this.vertex[index].Normal, Color.YellowGreen);
                }
            }

        }

        private void CreateNormalVertexBuffer()
        {
            this.nBuffer = new VertexBuffer(this.game.GraphicsDevice,
                                            typeof(VertexPositionColor),
                                            this.nVertex.Length,
                                            BufferUsage.None);
            this.nBuffer.SetData<VertexPositionColor>(this.nVertex);
        }

        private void CreateNormalEffect()
        {
            this.nEffect = new BasicEffect(this.game.GraphicsDevice);
        }

        private void DrawNormals(Camera camera)
        {
            this.game.GraphicsDevice.SetVertexBuffer(this.nBuffer);

            this.nEffect.World = this.world;
            this.nEffect.View = camera.GetView();
            this.nEffect.Projection = camera.GetProjection();
            this.nEffect.VertexColorEnabled = true;
 
            foreach (EffectPass pass in this.nEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.game.GraphicsDevice.DrawUserPrimitives
                    <VertexPositionColor>(PrimitiveType.LineList,
                                          this.nVertex,
                                          0,
                                          this.nVertex.Length / 2);
            }
        }

        private void RecalculateNormals()
        {
            int k = 0;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < col - 1; j++)
                {
                    for (int l = 0; l < 2; l++)
                    {
                        Vector3 v0 = vertex[this.indices[k++]].Position;
                        Vector3 v1 = vertex[this.indices[k++]].Position;
                        Vector3 v2 = vertex[this.indices[k++]].Position;

                        Vector3 _vr1 = v0 - v1;
                        Vector3 _vr2 = v2 - v0;
                        Vector3 vrf = Vector3.Cross(_vr1, _vr2);

                        k -= 3;

                        vertex[this.indices[k++]].Normal += vrf;
                        vertex[this.indices[k++]].Normal += vrf;
                        vertex[this.indices[k++]].Normal += vrf;
                    }
                }
            }
        }

        private void NormalizeNormals()
        {
            for (int i = 0; i < this.vertex.Length; i++)
            {
                this.vertex[i].Normal.Normalize();
            }
        }

        public Vector3 GetPosition()
        {
            return this.position;
        }

        public Vector3 GetScale()
        {
            return this.scale;
        }
    }
}