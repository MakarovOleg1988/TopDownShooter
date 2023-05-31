using UnityEngine;

namespace TopDownShooter
{
    public class ShowGizmos: MonoBehaviour
    {
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
