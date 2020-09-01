using System;

namespace Valax321.AudioSystem
{
    [Serializable]
    public class Range<T> where T: IComparable<T>
    {
        public T min;
        public T max;

        public Range(T min, T max)
        {
            this.min = min;
            this.max = max;
        }
    }

    [Serializable]
    public class FloatRange : Range<float>
    {
        public FloatRange(float min, float max) : base(min, max)
        {
        }
    }

    [Serializable]
    public class IntRange : Range<int>
    {
        public IntRange(int min, int max) : base(min, max)
        {
        }
    }
}