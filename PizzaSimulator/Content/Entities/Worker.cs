using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;
using PizzaSimulator.Content.Components.Structs;
using PizzaSimulator.Content.World;
using PizzaSimulator.Content.World.Tiles;
using PizzaSimulator.Content.World.Tiles.SubTiles;
using PizzaSimulator.Extensions;
using System.Collections.Generic;
using System.Linq;

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
            path = new List<Node>();
            EntityState idle = new EntityState(new SpriteAnimation(Assets.Worker, 1, 5, true, 160));
            idle.OnStateEnd += delegate { PickState(); };

            EntityState wander = new EntityState(new SpriteAnimation(Assets.Worker_Walk, 4, 8, true, 160));
            wander.OnStateBegin += delegate { Velocity = new Vector2(RNGMachine.Instance.Generator.Next(-16, 17), RNGMachine.Instance.Generator.Next(-16, 17)); };
            wander.OnStateUpdate += Wander_OnUpdate;
            wander.OnStateEnd += delegate { Velocity = Vector2.Zero; SetState("Idle"); };

            EntityState tracingPath = new EntityState(new SpriteAnimation(Assets.Worker_Walk, 4, 8, true));
            tracingPath.OnStateUpdate += delegate { TracePath(); };
            tracingPath.OnStateEnd += delegate { Velocity = Vector2.Zero; SetState("Idle"); };

            States.Add("Idle", idle);
            States.Add("Wander", wander);
            States.Add("TracingPath", tracingPath);
        }

        private void PickState()
        {
            Tile tile = null;

            TileCoordinates c = Position.ToTileCoordinates();
            Tile currentTile = GameLoop.World.TileGrid[c.X, c.Y];

            if (currentTile.HasSubtile(typeof(CashRegister)))
            {
                SetState("Idle");
                CurrentState.CurrentAnimation.SpriteFX = RNGMachine.Instance.Generator.Next(2) == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.FlipHorizontally;
                return;
            }

            foreach (Tile t in GameLoop.World.ImportantTiles)
            {
                if (GameLoop.World.ContainingEntities(t).Count(x => x.GetType() == typeof(Worker)) < 1 && t.HasSubtile(typeof(CashRegister)))
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

            Velocity = Vector2.Normalize(destination - (Position + new Vector2(5, 9))) * 32f;

            if (Vector2.Distance(Position + new Vector2(5, 9), destination) <= 2f)
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

            if (!EntityManager.Instance.Entities.Contains(Helping))
                Helping = null;
        }

        public bool CanHelp() => Helping == null;

        public void RequestHelp(Entity e)
        {
            if (CanHelp())
                Helping = e;
        }

        public Entity Helping { get; set; }

        private List<Node> path;
    }
}
