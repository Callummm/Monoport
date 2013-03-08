using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PlatformerMonoPort
{
    // An interface that specifies the 'contract' for all entities (inherits from IDisposable to encorperate the dispose function) that will be used with this framework, regardless of the actual game
    public interface IEntity : IDisposable
    {
        int UID { get; set; }
        string UName { get; set; }
        Vector2 Position { get; set; }
        EntityManager Manager { get; set; }
        void Update();
        new void Dispose();
    }
}
