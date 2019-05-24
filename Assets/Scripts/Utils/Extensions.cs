using UnityEngine;

namespace pokoro
{
	public static class Extensions
	{
		//Returns the normalized RIGHT component with Y axis zeroed
		public static Vector3 RightSansYNormalized(this Transform transform)
		{
			var result = transform.right;
			result.y = 0;
			result.Normalize();
			return result;
		}
		//Returns the normalized FORWARD component with Y axis zeroed
		public static Vector3 ForwardSansYNormalized(this Transform transform)
		{
			var result = transform.forward;
			result.y = 0;
			result.Normalize();
			return result;
		}

		//Convert degrees to radians
		public static float ToRadians(this float degrees)
		{
			//degrees * PI / 180
			return degrees * 0.017453f;     //Optimized
		} 
		//Convert radians to degrees
		public static float ToDegrees(this float radians)
		{
			//radians * 180 / PI
			return radians * 57.295779f;    //Optimized
		}
		//Convert degrees to radians (double)
		public static double ToRadians(this double degrees)
		{
			//degrees * PI / 180
			return degrees * 0.017453292519943;     //Optimized
		} 
		//Convert radians to degrees (double)
		public static double ToDegrees(this double radians)
		{
			//radians * 180 / PI
			return radians * 57.295779513082321;    //Optimized
		}
	}
}