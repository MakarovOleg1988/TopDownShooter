using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    public class NPSParam : UnitParam
    {
        [SerializeField, Range(0.5f, 4f), Tooltip("Дистанция рукопашной атаки")]
        protected float _attackRange;

        [SerializeField, Range(1f, 20f), Tooltip("Дистанция преследования игрока")]
        protected float _chaseRange;

        [SerializeField, Range(1f, 15f)]
        protected float _timer;

        [SerializeField]
        protected Transform[] _steps;

        protected NavMeshAgent _agent;
        protected Transform _player;

        protected bool _findNextSteps = false;
        

        public float Distance()
        {
            if (_player == null) return default;

            float distance = Vector3.Distance(transform.position, _player.position);
            return distance;
        }
    }
}
