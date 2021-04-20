﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Extensions;

namespace PizzaSimulator.Content.Entities
{
    public class Worker : Entity
    {
        public static void SpawnWorker(Vector2 p)
        {
            Worker customer = new Worker()
            {
                Position = p - new Vector2(5, 9),
                MyCollider = new Collider(p, 10, 18)
            };

            EntityManager.Instance.Entities.Add(customer);
        }

        public static void KillWorker(Entity e)
        {
            EntityManager.Instance.Entities.Remove(e);
        }

        public Worker() : base("Idle")
        {

        }

        protected override void AddStates()
        {
            EntityState idle = new EntityState(new SpriteAnimation(Assets.Worker, 1, 5, true, 160));
            idle.OnStateEnd += delegate { SetState("Wander"); };

            EntityState wander = new EntityState(new SpriteAnimation(Assets.Worker_Walk, 4, 8, true, 160));
            wander.OnStateBegin += delegate { Velocity = new Vector2(RNGMachine.Instance.Generator.Next(-16, 17), RNGMachine.Instance.Generator.Next(-16, 17)); };
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

            States["Idle"].CurrentAnimation.SpriteFX =
                CurrentState.CurrentAnimation.SpriteFX =
                nVel.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None; 
        }

        protected override void UpdateSelf()
        {
            CurrentState.Update();
        }
    }
}
