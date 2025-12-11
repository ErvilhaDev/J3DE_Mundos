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
    class CubeBoundingBox : GameObject
    {

        enum BoxState
        {
            Invisible,
            Visible
        }
        BoxState currentState = BoxState.Invisible;
        private KeyboardState previousKeyboard;

        VertexPositionColor[] verts;
        VertexBuffer vertexBuffer;

        short[] indices;
        IndexBuffer indexBuffer;

        BoundingBox boundingBox;
        bool wireframe = true;
        Color color = Color.Red;

        public Vector3 position = Vector3.Zero;
        public Vector3 scale = Vector3.One;
        public float angle;


        public CubeBoundingBox(GraphicsDevice device) 
        {

            this.device = device;
            this.world = Matrix.Identity;

            MakeVertices();
            CreateIndex();

            // Create GPU buffers
            vertexBuffer = new VertexBuffer(
                device,
                typeof(VertexPositionColor),
                verts.Length,
                BufferUsage.None);

            vertexBuffer.SetData(verts);

            indexBuffer = new IndexBuffer(
                device,
                IndexElementSize.SixteenBits,
                indices.Length,
                BufferUsage.None);

            indexBuffer.SetData(indices);

            // BasicEffect from GameObject
            effect = new BasicEffect(device);
            effect.VertexColorEnabled = true;

            this.indexBuffer = new IndexBuffer(this.device,
                                               IndexElementSize.SixteenBits,
                                               this.indices.Length,
                                               BufferUsage.None);
            this.indexBuffer.SetData<short>(this.indices);

        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            this.world =
                Matrix.CreateScale(scale) *
                Matrix.CreateRotationY(angle) *
                Matrix.CreateTranslation(position);

            //world = Matrix.CreateTranslation(position);
            UpdateBoundingBox();

            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.B) && previousKeyboard.IsKeyUp(Keys.B))
            {
                ToggleState();
            }

            previousKeyboard = key;
        }


        public override void Draw(Camera camera)
        {
            if (currentState == BoxState.Invisible)
                return; // do not draw anything

            base.Draw(camera);

            device.SetVertexBuffer(vertexBuffer);
            device.Indices = indexBuffer;

            RasterizerState previousState = device.RasterizerState;

            RasterizerState wire = new RasterizerState
            {
                CullMode = CullMode.None,
                FillMode = FillMode.WireFrame
            };
            device.RasterizerState = wire;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    verts.Length,
                    0,
                    indices.Length / 3
                );
            }

            device.RasterizerState = previousState;
        }

        public void MakeVertices()
        {
            this.verts = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(-1, 1, 1), color), // 0
                new VertexPositionColor(new Vector3( 1, 1, 1), color), // 1
                new VertexPositionColor(new Vector3( 1,-1, 1), color), // 2
                new VertexPositionColor(new Vector3(-1,-1, 1), color), // 3
                new VertexPositionColor(new Vector3(-1, 1,-1), color), // 4
                new VertexPositionColor(new Vector3( 1, 1,-1), color), // 5
                new VertexPositionColor(new Vector3( 1,-1,-1), color), // 6
                new VertexPositionColor(new Vector3(-1,-1,-1), color), // 7
            };
        }

        private void CreateIndex()
        {
            indices = new short[]
            {
                0, 1, 2,  0, 2, 3,     // front
                1, 5, 6,  1, 6, 2,     // right
                5, 4, 7,  5, 7, 6,     // back
                4, 0, 3,  4, 3, 7,     // left
                4, 5, 1,  4, 1, 0,     // top
                3, 2, 6,  3, 6, 7      // bottom
            };

        }

        private void UpdateBoundingBox() 
        {
            Vector3 worldCenter = Vector3.Transform(this.position, this.MatrixWorld);
            Vector3 halfExtents = this.scale;
            this.boundingBox = new BoundingBox(worldCenter - halfExtents, worldCenter + halfExtents);
        }

        public void ToggleState()
        {
            if (currentState == BoxState.Invisible)
                currentState = BoxState.Visible;
            else
                currentState = BoxState.Invisible;
        }

        public void ColorChange(Color newColor)
        {
            this.color = newColor;
            for (int i = 0; i < verts.Length; i++)
                verts[i].Color = newColor;
            vertexBuffer.SetData(verts);
        }

        public bool IsColliding(BoundingBox other)
        {
            return this.boundingBox.Intersects(other);
        }

        public BoundingBox GetBoundingBox()
        {
            return this.boundingBox;
        }
    }
}
