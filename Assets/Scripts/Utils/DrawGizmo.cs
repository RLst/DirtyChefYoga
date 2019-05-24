using UnityEngine;

namespace DirtyChefYoga.Util
{
    public enum GizmoType
    {
        Sphere,
        Box
    }

    public class DrawGizmo : MonoBehaviour
    {
        [SerializeField] GizmoType type = GizmoType.Box;
        [SerializeField] Color color = new Color(1, 0.5f, 0, 0.75f);
        [SerializeField] float radius = 1f;
        [SerializeField] Vector3 size = new Vector3(1,1,1);
        [SerializeField] Vector3 offset = new Vector3();

        void OnDrawGizmos()
        {
            Gizmos.color = color;
            switch (type)
            {
                case GizmoType.Sphere:
                    Gizmos.DrawWireSphere(transform.position + offset, radius);
                    break;
                case GizmoType.Box:
                    Gizmos.DrawWireCube(transform.position + offset, size);
                    break;
            }
        }
    }
}
