using UnityEngine;

namespace TopDownShooter
{
    public class TriggerManager : MonoBehaviour, IEventManager
    {
        [SerializeField]
        private bool _isDamagePlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() && _isDamagePlayer == true) IEventManager.SendSetDamagePlayer();
        }
    }
}
