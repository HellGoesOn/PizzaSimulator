using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.Components
{
    public class EntityState
    {
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

        public event EntityStateEventHandler OnStateBegin;
        public event EntityStateEventHandler OnStateUpdate;
        public event EntityStateEventHandler OnStateEnd;

        public void BeginState()
        {
            OnStateBegin?.Invoke(this);
        }

        public void Update()
        {
            OnStateUpdate?.Invoke(this);
            AssignedAnimations[SelectedAnimation].Update();

            if(!CurrentAnimation.Active)
            {
                OnStateEnd?.Invoke(this);

                foreach (SpriteAnimation a in AssignedAnimations)
                    a.Reset();
            }
        }

        public SpriteAnimation CurrentAnimation => AssignedAnimations[SelectedAnimation];

        public int SelectedAnimation { get; set; }

        public List<SpriteAnimation> AssignedAnimations { get; }
    }
}
