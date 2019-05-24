using UnityEngine;

namespace pokoro
{
	public static class Utility 
	{
		//Returns normalized forward vector of camera with Y zeroed. 
		//Useful for moving characters in alignment with the current game view.
		public static Vector3 GetFilteredCameraForward(Camera cam)
        {
            var result = cam.transform.forward;
            result.y = 0;   //Zero the Y component. Customize yourself ie. Jump
            result.Normalize();
            return result;
        }
		//Returns normalized right vector of camera with Y zeroed. 
		//Useful for moving characters in alignment with the current game view.
        public static Vector3 GetFilteredCameraRight(Camera cam)
        {
            var result = cam.transform.right;
            result.y = 0;   //Zero the Y component. Customize yourself ie. Jump
            result.Normalize();
            return result;
        }
	}
}