using System.Collections.Generic;

namespace PizzaSimulator.Content.Components
{
    public class EntityState
    {
        public event EntityStateEventHandler OnStateBegin;

        public event EntityStateEventHandler OnStateUpdate;

        public event EntityStateEventHandler OnStateEnd;

        protected EntityState()
        {
            AssignedAnimations = new List<SpriteAnimation>();
        }

        public EntityState(string path, int frameCount, float fps = 5f, bool looping = false, int loopTime = -1) : this()
        {
            SpriteAnimation animation = new SpriteAnimation(path, frameCount, fps, looping, loopTime);
            AssignedAnimations.Add(animation);
        }

        public EntityState(params SpriteAnimation[] animations) : this()
        {
            foreach (SpriteAnimation animation in animations)
                AssignedAnimations.Add(animation);
        }

        public void BeginState()
        {
            HasEnded = false;
            OnStateBegin?.Invoke(this);
        }

        public void Update()
        {
            OnStateUpdate?.Invoke(this);
            AssignedAnimations[SelectedAnimation].Update();

            if(!CurrentAnimation.Active)
            {
                EndState();
            }
        }

        public void EndState()
        {
            HasEnded = true;
            OnStateEnd?.Invoke(this);

            foreach (SpriteAnimation a in AssignedAnimations)
                a.Reset();
        }

        public bool HasEnded { get; set; }

        public SpriteAnimation CurrentAnimation => AssignedAnimations[SelectedAnimation];

        public int SelectedAnimation { get; set; }

        public List<SpriteAnimation> AssignedAnimations { get; }
    }
}
