using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PizzaSimulator.Content.Components;

namespace PizzaSimulator.Content.UI.Elements
{
    public class UIBKBLogo : UIImage
    {
        public UIBKBLogo() : base(new SpriteAnimation(Assets.BKBLogo, 1, 1, true))
        {
            Opacity = -2f;
        }

        protected override void UpdateSelf()
        {
            base.UpdateSelf();


            if (!hitThreshold)
                Opacity += 0.025f;
            else
                Opacity -= 0.025f;

            if(Opacity >= 1 && !playedSFX)
            {
                playedSFX = true;
                Assets.BKB.CreateInstance().Play();
            }

            if (Opacity >= 2.5f)
                hitThreshold = true;

        }

        private bool playedSFX;

        private bool hitThreshold;
    }
}
