using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace PlatformerMonoPort
{
    /// <summary>
    /// A valuable item the player can collect.
    /// New implementation using entity manager of the Gem class. Should technically inherit from GameXEntity (see the class for details), but this is just a prototype.
    /// </summary>
    class GemNew : PlatformerEntity
    {
        private Texture2D texture;
        private Vector2 origin;
        private SoundEffect collectedSound;

        public const int PointValue = 30;
        public readonly Color Color = Color.Yellow;

        private float bounce;

        /// <summary>
        /// Gets a circle which bounds this gem in world space.
        /// </summary>
        public Circle BoundingCircle
        {
            get
            {
                return new Circle(Position, Tile.Width / 3.0f);
            }
        }

        /// <summary>
        /// Loads the gem texture and collected sound.
        /// </summary>
        public override void LoadContent()
        {
            texture = Manager.ContentManager.Load<Texture2D>("Sprites/Gem");
            origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
            collectedSound = Manager.ContentManager.Load<SoundEffect>("Sounds/GemCollected");
        }

        /// <summary>
        /// Bounces up and down in the air to entice players to collect them.
        /// Animates each gem and allows the player to collect them.
        /// </summary>
        public override void Update()
        {
            // Bounce control constants
            const float BounceHeight = 0.18f;
            const float BounceRate = 3.0f;
            const float BounceSync = -0.75f;

            // Bounce along a sine curve over time.
            // Include the X coordinate so that neighboring gems bounce in a nice wave pattern.            
            float t = (float)Manager.GameTime.TotalGameTime.TotalSeconds * BounceRate + Position.X * BounceSync;
            bounce = (float)Math.Sin(t) * BounceHeight * texture.Height;

            if (this.BoundingCircle.Intersects(Manager.Level.Player.BoundingRectangle))
            {
                OnCollected(Manager.Level.Player);
                Manager.Level.OnGemCollected();
            }
        }

        /// <summary>
        /// Called when this gem has been collected by a player and removed from the level.
        /// </summary>
        /// <param name="collectedBy">
        /// The player who collected this gem. Although currently not used, this parameter would be
        /// useful for creating special powerup gems. For example, a gem could make the player invincible.
        /// </param>
        public void OnCollected(PlayerNew collectedBy)
        {
            collectedSound.Play();
            this.Dispose();
        }

        /// <summary>
        /// Draws a gem in the appropriate color.
        /// </summary>
        public override void Draw()
        {
            Manager.SpriteBatch.Draw(texture, Position + new Vector2(0.0f, bounce), null, Color, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
