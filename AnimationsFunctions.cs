/*
        Small library for personal use
        Contains Various Animations classes that implement the GetState method, that returns an value corresponding to the current animation state.
*/

using System;
using System.Runtime.CompilerServices;

namespace AnimationFunctions
{
    interface IAnimation 
    {
        public float GetState(float time);
    }

    //Returns 0 - 1 proprotional to the time
    public class LinearAnimation : IAnimation
    {
        float duration;
        public float GetState(float time)
        {
            return (time % duration)/duration;
        }

        public LinearAnimation(float duration) 
        {
            this.duration = duration;
        }
    }

    //Loops thought 0 - 1
    public class SimpleSineAnimation : IAnimation
    {
        float duration;
        public float GetState(float time)
        {
            return (MathF.Sin(((MathF.PI * time) - (MathF.PI / 2)) / duration) + 1) / 2;
        }

        public SimpleSineAnimation(float duration)
        {
            this.duration = duration;
        }
    }

    //Keeps going, but returns an value that will execute waveAmmount ammount of waves in the duration
    class FadingSineAnimation : IAnimation
    {
        float duration;
        uint waveAmmount;
        float damping;
        public FadingSineAnimation(float duration, uint waveAmmount, float damping)
        {
            this.duration = duration;
            this.waveAmmount = waveAmmount;
            this.damping = MathF.Abs(damping);
        }

        public float GetState(float time)
        {
            float wobbleFac = (waveAmmount / duration) * MathF.PI * time;
            return MathF.Sin(wobbleFac) / (wobbleFac + damping);
        }
    }

    //Keeps going, but returns an value that will execute bounceAmmount ammount of bounce in the duration
    class BouncingAnimation : IAnimation
    {
        float duration;
        uint bounceAmmount;
        float bounciness;
        public BouncingAnimation(float duration, uint bounceAmmount, float bounciness)
        {
            this.duration = duration;
            this.bounceAmmount = bounceAmmount;
            this.bounciness = MathF.Abs(bounciness);
        }

        public float GetState(float time)
        {
            float wobbleFac = (bounceAmmount / duration) * MathF.PI * time;
            return Math.Abs(MathF.Sin(wobbleFac) / (wobbleFac + bounciness));
        }
    }

    //Maps smoothing animation beetween 0 - 1
    //Exponential smoothing
    class SimpleSmoothing : IAnimation 
    {
        float duration;

        public SimpleSmoothing(float duration) 
        {
            this.duration= duration;
        }

        public float GetState(float time) 
        {
            float q = 1 / duration;
            float t2 = 3 * time * time;
            return q * q * (t2 - (q * (2 / 3) * (t2 * time)));
        }
    }
}
