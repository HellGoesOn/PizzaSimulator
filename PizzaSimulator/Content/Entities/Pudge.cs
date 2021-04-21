using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Extensions;
using System;

namespace PizzaSimulator.Content.Entities
{
    public class Pudge : Entity
    {
        public static void SpawnPudge(Vector2 p)
        {
            Pudge pudge = new Pudge()
            {
                Position = p - new Vector2(12, 12),
                MyCollider = new Collider(p, 24, 24)
            };

            EntityManager.Instance.Entities.Add(pudge);
        }

        public static void KillCustomer(Entity e)
        {
            EntityManager.Instance.Entities.Remove(e);
        }

        public Pudge() : base("Idle")
        {

        }

        protected override void AddStates()
        {
            EntityState idle = new EntityState(new SpriteAnimation(Assets.Pudge, 1, 5, true, 160));
            idle.OnStateEnd += delegate { SetState("Wander"); };

            EntityState wander = new EntityState(new SpriteAnimation(Assets.Pudge, 1, 8, true, 160));
            wander.OnStateBegin += delegate { Rot = 0f; Velocity = new Vector2(RNGMachine.Instance.Generator.Next(-36, 37), RNGMachine.Instance.Generator.Next(-16, 17)); };
            wander.OnStateUpdate += Wander_OnUpdate;
            wander.OnStateEnd += delegate { Velocity = Vector2.Zero; SetState("Idle"); };

            States.Add("Idle", idle);
            States.Add("Wander", wander);
        }

        private void Wander_OnUpdate(object sender)
        {
            float deltaTime = EntityManager.Instance.DeltaTime;

            Vector2 nVel = Velocity.ToNormalized();

            Position += Velocity * deltaTime;

            if (Math.Abs(Rot) > 0.24f)
                RotReverse = !RotReverse;

            Rot += RotReverse ? -0.04f : 0.04f;

            DrawOffset = new Vector2(Rot * 20, -Math.Abs(Rot * 40));

            CurrentState.CurrentAnimation.Rotation = Rot;

            States["Idle"].CurrentAnimation.SpriteFX =
                CurrentState.CurrentAnimation.SpriteFX =
                nVel.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        protected override void UpdateSelf()
        {
            CurrentState.Update();
        }

        public bool RotReverse { get; private set; }
        public float Rot { get; set; }
    }
}
