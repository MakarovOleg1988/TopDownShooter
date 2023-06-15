using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    public class EnemyParam : UnitParam
    {
        protected PoolForSpittingVenomProjectile _poolForSpittingVenomProjectile;

        [SerializeField]
        protected Transform _targetFire;

        [SerializeField, Range(0.5f, 4f), Tooltip("Дистанция рукопашной атаки")]
        protected float _attackRange;

        [SerializeField, Range(1f, 20f), Tooltip("Дистанция преследования игрока")]
        protected float _chaseRange;

        [SerializeField]
        protected List<Transform> _steps = new List<Transform>();

        protected bool _canShoot;

        protected NavMeshAgent _agent;
        protected Transform _player;
        protected ParticleSystem _particleSystem;

        public float Distance()
        {
            if (_player == null) return default;

            float distance = Vector3.Distance(transform.position, _player.position);
            return distance;
        }
    }
}
