using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mundos3D
{
    public class Camera
    {
        protected Matrix view;
        protected Matrix projection;

        protected Vector3 position;
        protected Vector3 target;
        protected Vector3 up;


        protected float yaw;
        protected float pitch; 

        public Camera()
        {
            this.position = new Vector3(0, 1, 10); 
            this.yaw = 0f;
            this.pitch = 0f;
            this.up = Vector3.Up;

            this.UpdateView();
            this.SetupProjection();
        }

        public void SetupProjection()
        {
            Screen screen = Screen.GetInstance();

            this.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                screen.GetWidth() / (float)screen.GetHeight(),
                0.1f,
                1000f);
        }

        private void UpdateView()
        {
            
            Matrix rotation = Matrix.CreateFromYawPitchRoll(yaw, pitch, 0);

            
            Vector3 forward = Vector3.Transform(Vector3.Forward, rotation);

            
            this.target = this.position + forward;

            
            this.view = Matrix.CreateLookAt(this.position, this.target, Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float moveSpeed = 5f * dt;
            float rotSpeed = 1.5f * dt;

            
            if (kb.IsKeyDown(Keys.Q)) yaw += rotSpeed;   
            if (kb.IsKeyDown(Keys.E)) yaw -= rotSpeed;   
            if (kb.IsKeyDown(Keys.R)) pitch += rotSpeed; 
            if (kb.IsKeyDown(Keys.F)) pitch -= rotSpeed;
            if (kb.IsKeyDown(Keys.LeftShift)) moveSpeed *= 5f;

            
            pitch = MathHelper.Clamp(pitch, -MathHelper.PiOver2 + 0.1f, MathHelper.PiOver2 - 0.1f);

            
            Matrix rotation = Matrix.CreateFromYawPitchRoll(yaw, pitch, 0);
            Vector3 forward = Vector3.Normalize(Vector3.Transform(Vector3.Forward, rotation));
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, Vector3.Up));

            
            if (kb.IsKeyDown(Keys.W)) position += forward * moveSpeed;
            if (kb.IsKeyDown(Keys.S)) position -= forward * moveSpeed;
            if (kb.IsKeyDown(Keys.A)) position -= right * moveSpeed;
            if (kb.IsKeyDown(Keys.D)) position += right * moveSpeed;
            if (kb.IsKeyDown(Keys.Z)) position += Vector3.Up * moveSpeed;
            if (kb.IsKeyDown(Keys.X)) position -= Vector3.Up * moveSpeed;

            
            UpdateView();
        }

        public Matrix GetView()
        {
            return this.view;
        }

        public Matrix GetProjection()
        {
            return this.projection;
        }
    }
}
