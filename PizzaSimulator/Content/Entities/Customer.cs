﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World.Tiles;
using PizzaSimulator.Extensions;
using System;
using System.Collections.Generic;

namespace PizzaSimulator.Content.Entities
{
    public class Customer : Entity
    {
        public static void SpawnCustomer(Vector2 p)
        {
            Customer customer = new Customer()
            {
                Position = p - new Vector2(5, 9),
                MyCollider = new Collider(p, 10, 18)
            };

            EntityManager.Instance.Entities.Add(customer);
        }

        public static void KillCustomer(Entity e)
        {
            EntityManager.Instance.Entities.Remove(e);
        }

        public Customer() : base("Idle")
        {

        }

        protected override void AddStates()
        {
            EntityState idle = new EntityState(new SpriteAnimation(Assets.Customer, 1, 5, true, 160));
            idle.OnStateEnd += delegate { SetState("Wander"); };

            EntityState wander = new EntityState(new SpriteAnimation(Assets.Customer_Walk, 4, 8, true, 160));
            wander.OnStateBegin += delegate { Velocity = new Vector2(RNGMachine.Instance.Generator.Next(-16, 17), RNGMachine.Instance.Generator.Next(-16, 17)); };
            wander.OnStateUpdate += Wander_OnUpdate;
            wander.OnStateEnd += delegate { Velocity = Vector2.Zero; SetState("Idle"); };
            wander.OnStateBegin += WanderTheFuckOff;

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

        private void WanderTheFuckOff(object sender)
        {
            Worker worker = null;
            foreach(Entity e in EntityManager.Instance.Entities)
                if(e is Worker worker1)
                {
                    worker = worker1;
                    break;
                }

            if (worker == null)
                return;

            List<Node> path = Pathfinding.FindPath(GameLoop.World.PathingGrid, Position, worker.Position);

            foreach (Node node in path)
            {
                GameLoop.World.SetTile(new WoodFloorTile(new TileCoordinates(node.X, node.Y)), node.X, node.Y);
            }
        }
    }
}
