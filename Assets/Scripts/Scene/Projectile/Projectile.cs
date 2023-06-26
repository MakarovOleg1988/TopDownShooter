using System;
using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : ProjectilesParam
    {
        private void OnValidate()
        {
            ChooseBullet(_projectileType);
        }

        private void Start()
        {
            _rbProjectile = GetComponent<Rigidbody>();

            StartCoroutine(DestroyCoroutine());
        }

        private void Update()
        {
            MovementBullet(_speedMovementProjectile);
        }

        private void MovementBullet(float _speedMovementProjectile)
        {
            _rbProjectile.AddForce(transform.forward * _speedMovementProjectile * Time.deltaTime, ForceMode.Impulse);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _rbProjectile.velocity = Vector3.zero;
        }

        public void ChooseBullet(ProjectileType _projectileType)
        {
            switch (_projectileType)
            {
                case ProjectileType.RevolverBullet:
                {
                    _damage = 1;
                    _speedMovementProjectile = 35;
                    _lifeTimeProjectile = 4;
                }; break;
                case ProjectileType.RifleBullet:
                {
                    _damage = 2;
                    _speedMovementProjectile = 25;
                    _lifeTimeProjectile = 4;
                    }; break;
                case ProjectileType.SpittingVenom:
                    {
                        _damage = 1;
                        _speedMovementProjectile = 25;
                        _lifeTimeProjectile = 4;
                    }; break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageAble>(out var enemy))
            {
                enemy.ApplyDamage(_damage);
            }

            else if (other.TryGetComponent<IDamageAblePlayer>(out var player))
            {
                player.ApplyDamagePlayer(_damage);
                ReturnToPool();
            }

            DestroyCoroutine();
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(_lifeTimeProjectile);
            ReturnToPool();
        }
    }
}