using UnityEngine;
using System.Collections;

namespace QuickAndDirty {
	public class Math {

		/*
		 * Transform to Transform Direction
		 */
		public static Vector3 TTDirection(Vector3 direction, Transform fromParent, Transform toParent)
		{
			return toParent.InverseTransformDirection(fromParent.TransformDirection(direction));
		}

		/*
		 * Transform to Transform Vector
		 */
		public static Vector3 TTVector(Vector3 vector, Transform fromParent, Transform toParent)
		{
			return toParent.InverseTransformVector(fromParent.TransformVector(vector));
		}

		/*
		 * Transform to Transform Point
		 */
		public static Vector3 TTPoint(Vector3 point, Transform fromParent, Transform toParent)
		{
			return toParent.InverseTransformPoint(fromParent.TransformPoint(point));
		}

		/*
		 * Calculates an exponential moving average. (Doesn't require storage of values.)
		 */
		public static float MovingAverage(float oldAverage, float newValue, int oldCount)
		{
			//To simplify: the oldAverage is weighted by the number of elements, and the new value is weighted at 1.
			return ((oldAverage * oldCount) + newValue) / (oldCount + 1);
		}

		public static Vector3 MovingAverage(Vector3 oldAverage, Vector3 newValue, int oldCount)
		{
			return ((oldAverage * oldCount) + newValue) / (oldCount + 1);
		}

        public static int Sign(int d)
        {
            return d >= 0 ? 1 : -1;
        }
        public static float Sign(float f)
        {
            return f >= 0 ? 1.0f : -1.0f;
        }

        public static float GetSqrRelativeVelocity(Rigidbody right, Rigidbody left)
        {
            return (right.velocity - left.velocity).sqrMagnitude;
        }

        public static float GetSqrRelativeVelocity(Rigidbody2D right, Rigidbody2D left)
        {
            return (right.velocity - left.velocity).sqrMagnitude;
        }

        /*
		 * Move from A to B at velocity units per frame. Velocity is unscaled.
		 * @param A the origin of the move in world space.
		 * @param B the destination of the move.
		 * @param velocity the distance to move in one step. (Unscaled)
		 */
		 public struct Slide
		 {
		 	public Vector3 point;
		 	public Vector3 delta;
		 }
		 public static Slide LinearMove(Vector3 A, Vector3 B, float velocity)
		 {
		 	Slide retval = new Slide();

		 	Vector3 m = B - A;
		 	float mSqr = m.sqrMagnitude;

		 	if (mSqr < velocity * velocity)
		 	{
		 		retval.point = B;
		 		retval.delta = m;
		 	}
		 	else
		 	{
		 		retval.delta = (m /  Mathf.Sqrt(mSqr)) * velocity;
		 		retval.point = A + retval.delta;
		 	}

			return retval;
		}

		public static float AntiLerp(float low, float high, float value)
		{
			if (value < low)
			{
				return 0f;
			}
			else if (value > high)
			{
				return 1f;
			}

			return (value - low) / (high - low);
		}
	}
}
