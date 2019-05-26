using UnityEngine;
using XboxCtrlrInput;
namespace DirtyChefYoga
{
    //Basic player input
    public abstract class PlayerInput : MonoBehaviour
    {
		[SerializeField] protected bool debug = true;
        [Space]

        [Header("Controller Settings")]
        [SerializeField] protected bool useRaw = false;
        [SerializeField] protected bool invertXaxis = false;
        [SerializeField] protected bool invertYaxis = false;

		//Left Axis
        public abstract Vector2 leftAxis { get; }

		//Right Axis
        public abstract Vector2 rightAxis { get; }

		//Triggers
        public abstract float move { get; }

        //Buttons
        public abstract bool @using { get; }
        public abstract bool used { get; }
        public abstract bool pickingUp { get; }
        public abstract bool pickedUp { get; }
        public abstract bool dashing { get; }
        public abstract bool dashed { get; }


        void OnGUI()
        {
            if (debug)
            {
                GUILayout.Label("Player Input");
				GUILayout.Space(5);
                GUILayout.Label("LeftAxis: " + leftAxis.x + ", " + leftAxis.y);
                GUILayout.Label("RightAxis: " + rightAxis.x + ", " + rightAxis.y);
                if (@using) GUILayout.Label("Using!");
                if (used) GUILayout.Label("Used!");
                if (pickingUp) GUILayout.Label("Picking Up!");
                if (pickedUp) GUILayout.Label("Picked Up!");
                if (dashing) GUILayout.Label("Dashing!");
                if (dashed) GUILayout.Label("Dashed!");
            }
        }
    }
}