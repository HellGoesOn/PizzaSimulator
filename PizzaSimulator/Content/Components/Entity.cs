using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

        public abstract void AddStates();

        protected abstract void UpdateSelf();

        public void Update()
        {
            if (!Active)
                return;

            if(Collider != null)
                Collider.Position = Position;

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


            sb.Draw(texture, Position, frame, color, rotation, drawOrigin, scale, spriteEffects, 1f);
        }

        public void SetState(string value)
        {
            State = value;

            CurrentState.BeginState();
        }

        public bool Active { get; set; } = true;

        public Vector2 Velocity { get; set; }

        public Vector2 Position { get; set; } = Vector2.Zero;

        public string State { get; private set; }

        public EntityState CurrentState => States[State];

        public Dictionary<string, EntityState> States { get; }

        public Collider Collider { get; set; }
    }
}
