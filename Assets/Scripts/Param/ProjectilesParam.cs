using UnityEngine;

namespace TopDownShooter
{
    public class ProjectilesParam: MonoBehaviour
    {
        protected Rigidbody _rbProjectile;

        [SerializeField]
        protected int _damage;        
        
        [SerializeField, Range(1f, 100f)]
        protected float _speedMovementProjectile;
        
        [SerializeField]
        protected float _lifeTimeProjectile;

        public ProjectileType _projectileType;
    }
}
