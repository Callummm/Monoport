using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
namespace PlatformerMonoPort
{
    // abstract entity class that specifies the outline for all entities in this particular game (platformer)
    public abstract class PlatformerEntity : Entity
    {
        public override void Update()
        {
        }

        public override void Dispose()
        {
            isDisposed = true; 
        }

        public override void LoadContent()
        {
        }

        public override void Draw()
        {
        }
    }
}
