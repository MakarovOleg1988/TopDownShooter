using UnityEngine;

namespace TopDownShooter
{
    public class ScoreKillEnemyPoint : MonoBehaviour
    {
        private TaskRules _taskRules;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Projectile>())
            {
                _taskRules._killVampirePoint++;
            }
        }
    }
}
