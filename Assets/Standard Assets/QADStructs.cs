using UnityEngine;

namespace QuickAndDirty {

	/*
	 * These structs do not do anything that you can't already do in Unity. What they do, is let you write it with a lot less code.
	 */

	public abstract class ValueRange<T> where T : struct
	{
		public T min;
		public T max;

		public ValueRange()
		{
		}

		public ValueRange(T min, T max)
		{
			this.min = min;
			this.max = max;
		}

		public ValueRange(ValueRange<T> range)
        {
        	this.min = range.min;
        	this.max = range.max;
        }

        public abstract T Random();
        public abstract T Sample(float t);
        public abstract T Clamp(T v);

	}

	[System.Serializable]
	public class FloatRange : ValueRange<float>
	{
		public FloatRange() : base() {}
		public FloatRange(float min, float max) : base(min, max) {}
		public FloatRange(FloatRange range) : base(range) {}

		public override float Random()
		{
			return UnityEngine.Random.Range(min, max);
		}

		public override float Sample(float t)
		{
			return Mathf.Lerp(min, max, t);
		}

        public override float Clamp(float f)
        {
            return Mathf.Clamp(f, min, max);
        }
	}

	[System.Serializable]
	public class IntRange : ValueRange<int>
	{
		public IntRange() : base() {}
		public IntRange(int min, int max) : base(min, max) {}
		public IntRange(IntRange range) : base(range) {}

		public override int Random()
		{
			return UnityEngine.Random.Range(min, max);
		}

		public override int Sample(float t)
		{
			return (int)Mathf.Lerp(min, max, t);
		}

        public override int Clamp(int i)
        {
            return Mathf.Clamp(i, min, max);
        }
	}

	[System.Serializable]
	public class ColorRange : ValueRange<Color>
	{
		public ColorRange() : base() {}
		public ColorRange(Color min, Color max) : base(min, max) {}
		public ColorRange(ColorRange range) : base(range) {}

    	public override Color Random()
        {
        	float r = UnityEngine.Random.Range(min.r, max.r);
        	float g = UnityEngine.Random.Range(min.g, max.g);
        	float b = UnityEngine.Random.Range(min.b, max.b);
        	float a = UnityEngine.Random.Range(min.a, max.a);

            return new Color(r, g, b, a);
        }

    	public override Color Sample(float t)
        {
            return new Color(Mathf.Lerp(min.r, max.r, t),
            	Mathf.Lerp(min.g, max.g, t),
            	Mathf.Lerp(min.b, max.b, t),
            	Mathf.Lerp(min.a, max.a, t));
        }

    	public override Color Clamp(Color v)
        {
        	return new Color(Mathf.Clamp(v.r, min.r, max.r),
        		Mathf.Clamp(v.g, min.g, max.g),
        		Mathf.Clamp(v.b, min.b, max.b),
        		Mathf.Clamp(v.a, min.a, max.a));
        }
	}
}