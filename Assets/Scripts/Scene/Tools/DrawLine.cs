using UnityEngine;

namespace TopDownShooter
{
    public class DrawLine : MonoBehaviour
    {
        private void LateUpdate()
        {
            Debug.DrawRay(transform.position, Vector3.forward, Color.red, 1000f);
        }
    }
}