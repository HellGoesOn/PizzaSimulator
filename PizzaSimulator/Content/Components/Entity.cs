using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Interfaces;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using PizzaSimulator.Extensions;
using System;
using System.Collections.Generic;

namespace PizzaSimulator.Content.Components
{
    public abstract class Entity : IHasDraw
    {
        protected Entity(string defaultState)
        {
            States = new Dictionary<string, EntityState>();

            AddStates();

            SetState(defaultState);
        }

        protected abstract void AddStates();

        protected abstract void UpdateSelf();

        public void Update()
        {
            if (!Active)
                return;

            if(MyCollider != null)
                MyCollider.Position = Position;

            UpdateSelf();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Active)
                return;

            DrawSelf(spriteBatch);
        }

        protected virtual void DrawSelf(SpriteBatch sb)
        {
            SpriteAnimation animation     = CurrentState.CurrentAnimation;
            Texture2D       texture       = animation.SpriteSheet;
            Rectangle       frame         = animation.FrameRect;
            Color           color         = Color.White;
            float           rotation      = 0f;
            Vector2         drawOrigin    = animation.DrawOrigin;
            float           scale         = 1f;
            SpriteEffects   spriteEffects = animation.SpriteFX;
            float           depth         = Math.Clamp(MyCollider.Center.Y / 100000f, 0, 1);

            if (Highlighted)
            for (int i = 0; i < 4; i++)
            {
                Vector2 offset = new Vector2(1, 0).RotatedBy(MathHelper.PiOver2 * i);
                sb.Draw(texture, MyCollider.Center + offset, frame, Color.Black, rotation, drawOrigin, scale, spriteEffects, depth - (0.000001f * (i + 1)));
            }

            sb.Draw(texture, MyCollider.Center, frame, color, rotation, drawOrigin, scale, spriteEffects, depth);
        }

        public void SetState(string value)
        {
            if (State != null && !CurrentState.HasEnded)
                States[State].EndState();

            State = value;

            CurrentState.BeginState();
        }

        public TileCoordinates ToTileCoordinates()
        {
            return new TileCoordinates((int)(Position.X / GameWorld.WORLD_WIDTH), (int)(Position.Y / GameWorld.WORLD_HEIGHT));
        }

        public bool Highlighted { get; set; }

        public bool Active { get; set; } = true;

        public Vector2 Velocity { get; set; }

        public Vector2 Position { get; set; } = Vector2.Zero;

        public string State { get; private set; }

        public EntityState CurrentState => States[State];

        public Dictionary<string, EntityState> States { get; }

        public Collider MyCollider { get; set; }
    }
}
