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
    class CBox : _Collider
    {

        public CBox(Game game, Vector3 position, Vector3 size)
            : base(game, position, size, Color.Red)
        {

        }

        // Método de update se precisar mover
        public void Update(Vector3 newPosition)
        {
            base.SetPosition(newPosition);
        }

        // Verifica colisão com outro collider
        public bool CheckCollision(_Collider other)
        {
            return base.IsColliding(other.GetBoundingBox());
        }
    }
}