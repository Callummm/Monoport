using System.Collections.Generic;
using PlatformerMonoPort.EntityManagment;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PlatformerMonoPort
{
    /// <summary>
    /// Entity Manager, manages entities creation and removal etc by keeping track of each one, also specifies to entities when they can update/be drawn
    /// </summary>
    public sealed class EntityManager
    {
        // Singleton implementation
        private static volatile EntityManager EMInstance;
        private static object syncRoot = new object();

        private List<Entity> _activeEntities;
        private int nextId;

        int _totalEntityAmount;
        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;
        private GameTime gameTime;
        private Kernel kernel;
        List<Entity> disposeList;


        public Kernel Kernel
        {
            get { return kernel; }
            set { kernel = value; }
        }

        //Gametime (incase enitites need to know about GT)
        public GameTime GameTime
        {
            get { return gameTime; }
        }

        //Reference to graphics device that entities can use
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
            set { graphicsDevice = value; }
        }

        //Reference to a content manager that entities can use
        public ContentManager ContentManager
        {
            get { return contentManager; }
            set { contentManager = value; }
        }

        //Reference to a sprite batcher that entities can use
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        //Ref to level
        public Level Level { get; set; }

        //Need to set graphics device etc
        public void setDevices(GraphicsDevice pGraphicsDevice, ContentManager pContentManager, SpriteBatch pSpriteBatch)
        {
            graphicsDevice = pGraphicsDevice;
            contentManager = pContentManager;
            spriteBatch = pSpriteBatch;
        }

        //Private Constructor
        private EntityManager()
        {
            ActiveEntities = new List<Entity>();
            disposeList = new List<Entity>();
            _totalEntityAmount = 0;
            nextId = 0;
        }

        // Return singleton instance of EntityManager
        public static EntityManager GetInstance
        {
            get
            {
                if (EMInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (EMInstance == null)
                            EMInstance = new EntityManager();
                    }
                }
                return EMInstance;
            }
        }

        //Ref to list of entities
        public List<Entity> ActiveEntities
        {
            get { return _activeEntities; }
            set { _activeEntities = value; }
        }

        //Requests creation of an entity (dynamic typing achieved through the use of generics) 
        //Note: This specifies that any requested entity must be of type entity and must have a blank constructor
        public Entity RequestEntity<EntityType>() where EntityType : Entity, new()
        {
            EntityType newEntity = new EntityType();
            newEntity.Manager = this;
            // ID and content are loaded straight away currently (content includes things such as images for textures and sound). 
            //NOTE: Loading the content should ideally be a seperate task which virtualy seperates the creation (from requesting the entity) to initialisation (of the key things such as graphics/sounds)
            newEntity.UID = nextId;
            newEntity.UName = string.Concat(newEntity.GetType().Name, newEntity.UID);
            nextId++;
            newEntity.LoadContent();
            _totalEntityAmount++;
            return newEntity;
        }

        // Counts to see how many entities are of the same type as the entity specified in the parameter
        private int eTypeCounter(Entity e)
        {
            int count = 0;
            foreach (Entity eInList in ActiveEntities)
            {
                if (eInList != e)
                {
                    if (eInList.ToString() == e.ToString())
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        
        // Removes an entity by reference
        public void RemoveEntity(Entity pEntity)
        {
            ActiveEntities.Remove(pEntity);
            _totalEntityAmount--;
        }

        // Removes an entity by its ID
        public void RemoveEntityById(int pEntityId)
        {
            Entity eToRemove = null;
            foreach (Entity e in ActiveEntities)
            {
                if (e.UID == pEntityId)
                {
                    eToRemove = e;
                }
            }

            if (eToRemove != null)
                RemoveEntity(eToRemove);
        }

        // Removes an entity by its Name
        public void RemoveEntityByName(string pEntityName)
        {
            Entity eToRemove = null;
            foreach (Entity e in ActiveEntities)
            {
                if (e.UName == pEntityName)
                {
                    eToRemove = e;
                }
            }

            if (eToRemove != null)
                RemoveEntity(eToRemove);
        }

        //returns the list of entities
        public List<Entity> GetAllEntities()
        {
            return ActiveEntities;
        }

        // Update function (GT specified incase entities might need it)
        public void Update(GameTime gT)
        {
            gameTime = gT;
            foreach (Entity e in _activeEntities)
            {
                    e.Update();
                    if (e.isDisposed)
                    {
                        disposeList.Add(e);
                    }
            }
            foreach (Entity e in disposeList)
            {
                RemoveEntity(e);
            }
            if (disposeList.Count > 0)
            disposeList.Clear();
        }

        // Draw function
        public void Draw()
        {
            foreach (Entity e in _activeEntities)
            {
                e.Draw();
            }
        }

    }
}
