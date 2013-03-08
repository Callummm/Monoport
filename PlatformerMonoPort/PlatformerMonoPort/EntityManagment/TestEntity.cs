using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PlatformerMonoPort.EntityManagment;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerMonoPort.EntityManagment
{
    // See GameXEntity for notes, this would inherit from the games base entity class (Note: the games base entity class not the framework), might be expanded upon, but should not be modified (Open/Closed principle)
    public class TestEntity : PlatformerEntity
    {
        Texture2D TestTexture;

        public TestEntity()
        {
        }

        public override void LoadContent()
        {
            TestTexture = new Texture2D(Manager.GraphicsDevice, 64, 64);
            TestTexture = Manager.ContentManager.Load<Texture2D>("OrangeFish");
        }

        public override void Draw()
        {
            Manager.SpriteBatch.Draw(TestTexture, new Vector2(Manager.GraphicsDevice.Viewport.Width / 2, Manager.GraphicsDevice.Viewport.Height / 2), Color.White);
        }

        public override void Update()
        {
        }

        public override void Dispose()
        {
        }
    }
}
