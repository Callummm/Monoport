using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace PlatformerMonoPort
{
    // Abstract class that defines an Entity that is used with this framework regardless of the actual game
    // Note: More work intended
    public abstract class Entity : IEntity
    {
        private int myUniqueID;
        private string myUniqueName;
        private Vector2 position;
        private EntityManager manager;
        private bool isdisposed = false;

        public bool isDisposed
        {
            get { return isdisposed; }
            set { isdisposed = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int UID
        {
            get { return myUniqueID; }
            set { myUniqueID = value; }
        }

        public string UName
        {
            get { return myUniqueName; }
            set { myUniqueName = value; }
        }

        public EntityManager Manager
        {
            set { manager = value; }
            get { return manager; }
        }

        //Entity constructor. Note: Entities add themselves to the managers list, so that it does not have to be done manually elsewhere.
        public Entity()
        {
            Manager = EntityManager.GetInstance;
            Manager.ActiveEntities.Add(this);
        }

        public abstract void Update();

        public abstract void Dispose();

        public abstract void LoadContent();

        public abstract void Draw();
    }
}
