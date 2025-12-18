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
    class EBox : _Collider
    {

        public EBox(Game game) :
            base(game, new Vector3(0, 1, 10), new Vector3(1f, 1f, 1f), Color.Red)
        {

        }

        public void Update()
        { 
        
        }

        public void Draw()
        {

        }

    }
}
