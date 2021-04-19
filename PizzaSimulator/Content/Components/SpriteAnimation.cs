using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSimulator.Content.Components
{
    public class SpriteAnimation
    {
        public SpriteAnimation(string path, int frameCount, float fps = 5f, bool looping = false, int loopTime = -1)
        {
            SpriteSheet = Loader.LoadTexture(path);
            FramesPerSecond = fps;

            FrameCount = frameCount;
            Looping = looping;
            LoopTime = loopTime;

            SpriteFX = SpriteEffects.None;
        }

        public void Reset()
        {
            ElapsedTime = 0;
            CurrentFrame = IsReversed ? FrameCount : 0;
            Active = true;
        }

        public void Update()
        {
            if (!Active)
                return;

            ElapsedTime += 0.016f; // magic number

            if (LoopTime != -1)
            {
                LoopTimer += 1;

                if (LoopTimer >= LoopTime - 1)
                    Looping = false;
            }

            if (ElapsedTime > TimePerFrame)
            {
                if (!IsReversed)
                    DefaultBehavior();
                else
                    ReversedBehavior();

                ElapsedTime -= TimePerFrame;
            }
        }

        public void DefaultBehavior()
        {
            CurrentFrame++;

            if (CurrentFrame >= FrameCount)
            {
                CurrentFrame = 0;

                if (!Looping)
                {
                    if (LoopTime != -1)
                    {
                        LoopTimer = 0;
                        Looping = true;
                    }

                    Active = false;
                }
            }
        }

        public void ReversedBehavior()
        {
            CurrentFrame--;

            if (CurrentFrame <= 0)
            {
                CurrentFrame = FrameCount;

                if (!Looping)
                {
                    if (LoopTime != -1)
                    {
                        LoopTimer = 0;
                        Looping = true;
                    }

                    Active = false;
                }
            }
        }

        public bool IsReversed { get; set; }

        public int LoopTimer { get; set; }

        public int LoopTime { get; }

        public float ElapsedTime { get; set; }

        public float FramesPerSecond { get; set; }

        public bool Looping { get; set; }

        public int FrameCount { get; set; }

        public int CurrentFrame { get; set; }

        public bool Active { get; set; } = true;

        public SpriteEffects SpriteFX { get; set; }

        public Texture2D SpriteSheet { get; set; }

        public float TimePerFrame => 1.0f / FramesPerSecond;

        public Vector2 FrameSize => new Vector2((int)SpriteSheet.Width, (int)(SpriteSheet.Height / FrameCount));

        public Rectangle FrameRect => new Rectangle(0, (int)(FrameSize.Y * CurrentFrame), (int)FrameSize.X, (int)FrameSize.Y);

        public Vector2 DrawOrigin => new Vector2((int)(FrameSize.X * 0.5f), (int)(FrameSize.Y * 0.5f));
    }
}
