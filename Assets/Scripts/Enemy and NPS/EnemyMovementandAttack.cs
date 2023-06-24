using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    public class EnemyMovementandAttack : EnemyParam, IDamageAble
    {
        private EnemyMovementandAttack enemyController;

        private void OnValidate()
        {
            ChooseEnemy(_unitType);
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            enemyController = GetComponent<EnemyMovementandAttack>();

            switch (_unitType)
            {
                case UnitType.Spider:
                    {
                        _particleSystem = GetComponentInChildren<ParticleSystem>();
                        _particleSystem.Stop();
                    }
                    break;
            }

            _poolForSpittingVenomProjectile = GetComponent<PoolForSpittingVenomProjectile>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _anim = GetComponentInChildren<Animator>();
            CreatePatrolArea();
        }

        public void Update()
        {
            MovementEnemy(_steps);
            ChaseEnemy(Distance());
            AttackEnemy(Distance());
        }

        private void CreatePatrolArea()
        {
            PointParam[] steps = GetComponentsInChildren<PointParam>();

            foreach (PointParam step in steps)
            {
                _steps.Add(step.transform);
            }
        }

        private void MovementEnemy(List<Transform> _steps)
        {
            StartCoroutine(MovementEnemyCoroutine(_steps));
        }

        private IEnumerator MovementEnemyCoroutine(List<Transform> _steps)
        {
            int random = Random.Range(0, _steps.Count);

            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _agent.speed = _speedMovement;
                _agent.SetDestination(_steps[random].position);
                _anim.SetFloat("IsMoving", 1f);
                yield return new WaitForSeconds(3f);
            }
        }

        private void ChaseEnemy(float Distance)
        {
            if (_player == null) return;

            switch (_unitType)
            {
                case UnitType.Vampire:
                    {
                        if (Distance <= _chaseRange)
                        {
                            _agent.speed = _speedRunning;
                            _agent.SetDestination(_player.position);
                            _anim.SetFloat("IsChasing", 1f);
                        }

                        else
                        {
                            _anim.SetFloat("IsChasing", 0f);
                            _agent.speed = _speedMovement;
                        }
                    } break;
                case UnitType.Zombi:
                    {
                        if (Distance <= _chaseRange)
                        {
                            _agent.speed = _speedRunning;
                            _agent.SetDestination(_player.position);
                            _anim.SetFloat("IsChasing", 1f);
                        }

                        else
                        {
                            _anim.SetFloat("IsChasing", 0f);
                            _agent.speed = _speedMovement;
                        }
                    } break;
                case UnitType.Spider:
                    {
                        transform.LookAt(_player);
                    } break;
            }
        }

        private void AttackEnemy(float Distance)
        {
            switch (_unitType)
            {
                case UnitType.Vampire:
                    {
                        if (Distance < _attackRange) _anim.SetBool("IsAttack", true);
                        else _anim.SetBool("IsAttack", false);
                    }
                    break;
                case UnitType.Zombi:
                    {
                        if (Distance < _attackRange) _anim.SetBool("IsAttack", true);
                        else _anim.SetBool("IsAttack", false);
                    }
                    break;
                case UnitType.Spider:
                    {
                        if (Distance < _attackRange && _canShoot == true)
                        {
                            _canShoot = false;
                            Vector3 positionFire = _targetFire.position;
                            Quaternion rotationFire = _targetFire.rotation;

                            _anim.SetBool("IsAttack", true);
                            _poolForSpittingVenomProjectile.GetFreeElement(positionFire, rotationFire);
                            _particleSystem.Play();
                        }
                        else
                        {
                            StartCoroutine(TimerbetweenShootCoroutine());
                            _anim.SetBool("IsAttack", false);
                        }
                    }
                    break;
            }
        }

        private IEnumerator TimerbetweenShootCoroutine()
        {
            yield return new WaitForSeconds(2.5f);
            _particleSystem.Stop();
            _canShoot = true;
        }

        public void ApplyDamage(int damage)
        {
            CurrentHealth -= damage;
            IEventManager.SendSetDamageEnemy();

            if (CurrentHealth > 0)
            {
                _anim.SetTrigger("GetDamage");
            }

            if (CurrentHealth <= 0)
            {
                _anim.SetTrigger("IsDying");
                _agent.speed = _speedMovement = 0f;
                enemyController.enabled = false;

                StartCoroutine(SetDeathCoroutine());

                switch (_unitType)
                {
                    case UnitType.Vampire:
                        {
                            IEventManager.SendSetKillVampire();
                        } break;
                }
            }
        }

        private IEnumerator SetDeathCoroutine()
        {
            yield return new WaitForSeconds(4f);
            _anim.enabled = false;
            yield return new WaitForSeconds(10f);
            Destroy(this.gameObject);
        }

        public void ChooseEnemy(UnitType _unitType)
        {
            switch (_unitType)
            {
                case UnitType.Zombi:
                    {
                        _speedMovement = 1f;
                        _speedRunning = 2f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 0.5f;
                        CurrentHealth = 1;
                        _attackRange = 1f;
                        _chaseRange = 6f;
                    }; break;
                case UnitType.Vampire:
                    {
                        _speedMovement = 2f;
                        _speedRunning = 3f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 0.2f;
                        CurrentHealth = 3;
                        _attackRange = 1f;
                        _chaseRange = 10f;
                    }; break;
                case UnitType.Spider:
                    {
                        _speedMovement = 2f;
                        _speedRunning = 2f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 2.5f;
                        CurrentHealth = 3;
                        _attackRange = 8f;
                        _chaseRange = 8f;
                    }; break;
                case UnitType.Player:
                    {
                        _speedMovement = 3f;
                        _speedRunning = 4f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 0.2f;
                        CurrentHealth = 5;
                    }; break;
                case UnitType.Citizen:
                    {
                        _speedMovement = 1f;
                        _speedRunning = 1f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 0.2f;
                        CurrentHealth = 4;
                    }; break;
            }
        }
    }
}
