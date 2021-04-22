using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;
using PizzaSimulator.Content.World.Tiles.SubTiles;
using PizzaSimulator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
            path = new List<Node>();
            EntityState idle = new EntityState(new SpriteAnimation(Assets.Customer, 1, 5, true, 160));
            idle.OnStateEnd += delegate { PickState(); };
            EntityState wander = new EntityState(new SpriteAnimation(Assets.Customer_Walk, 4, 8, true, 160));
            wander.OnStateBegin += delegate { Velocity = new Vector2(RNGMachine.Instance.Generator.Next(-16, 17), RNGMachine.Instance.Generator.Next(-16, 17)); };
            wander.OnStateUpdate += Wander_OnUpdate;
            wander.OnStateEnd += delegate { Velocity = Vector2.Zero; SetState("Idle"); };

            EntityState tracingPath = new EntityState(new SpriteAnimation(Assets.Customer_Walk, 4, 8, true));
            tracingPath.OnStateUpdate += delegate { TracePath(); };
            tracingPath.OnStateEnd += delegate { Velocity = Vector2.Zero; SetState("Idle"); };

            EntityState goingNowhere = new EntityState(new SpriteAnimation(Assets.Customer_Walk, 4, 8, true));
            goingNowhere.OnStateBegin += GoingNowhere_OnStateBegin;
            goingNowhere.OnStateUpdate += GoingNowhere_OnStateUpdate;
            goingNowhere.OnStateUpdate += delegate { TracePath(); };

            States.Add("Idle", idle);
            States.Add("Wander", wander);
            States.Add("TracingPath", tracingPath);
            States.Add("GoingNowhere", goingNowhere);
        }

        private void GoingNowhere_OnStateUpdate(object sender)
        {
            CurrentState.CurrentAnimation.Opacity -= 0.001f;

            if (CurrentState.CurrentAnimation.Opacity <= 0.75f)
            {
                if (helper != null)
                {
                    helper.Helping = null;
                    helper = null;
                }
            }

            if (CurrentState.CurrentAnimation.Opacity <= 0.12f)
                EntityManager.KillEntity(this);
        }

        private void GoingNowhere_OnStateBegin(object sender)
        {
            path.Clear();

            path = Pathfinding.FindPath(GameLoop.World.PathingGrid, Position, Tile.GetCenter(GameLoop.World.GetTile(0, 0)));
        }

        private void PickState()
        {
            Tile tile = null;

            TileCoordinates c = Position.ToTileCoordinates();
            Tile currentTile = GameLoop.World.TileGrid[c.X, c.Y];

            if (GameLoop.World.ContainingEntities(currentTile).Count(x => x.GetType() == typeof(Worker)) > 0 && currentTile.HasSubtile(typeof(CashRegister)))
            {
                if(needsHelp)
                {
                    helper = (Worker)GameLoop.World.ContainingEntities(currentTile).Find(x => x.GetType() == typeof(Worker));
                    helper.RequestHelp(this);

                    if (helper.Helping == this)
                    {
                        needsHelp = false;
                        SetState("Idle");
                    }
                    else
                    {
                        SetState("Wander");
                    }
                    return;
                }

                SetState("GoingNowhere");
                return;
            }

            foreach (Tile t in GameLoop.World.ImportantTiles)
            {
                if (GameLoop.World.ContainingEntities(t).Count(x => x.GetType() == typeof(Worker) && (x as Worker).CanHelp()) > 0 && t.HasSubtile(typeof(CashRegister)))
                {
                    tile = t;
                    break;
                }
            }

            if (tile == null)
            {
                SetState("Wander");
                return;
            }

            path.Clear();

            path = Pathfinding.FindPath(GameLoop.World.PathingGrid, Position, Tile.GetCenter(tile));
            SetState("TracingPath");
        }

        private void TracePath()
        {
            if (path.Count <= 0)
            {
                SetState("Idle");
                return;
            }

            float deltaTime = EntityManager.Instance.DeltaTime;

            Node currentNode = path[0];

            Vector2 destination = Node.ToWorldPos(currentNode);

            Velocity = Vector2.Normalize(destination - (Position + new Vector2(5))) * 22f;

            if (Vector2.Distance(Position + new Vector2(5), destination) <= 4f)
                path.RemoveAt(0);

            CurrentState.CurrentAnimation.SpriteFX = Velocity.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Position += Velocity * deltaTime;
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

        private bool needsHelp = true;

        private Worker helper = null;

        private List<Node> path;
    }
}
