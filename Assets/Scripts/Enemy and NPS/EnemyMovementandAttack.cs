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
        }

        private void Start()
        {
            _poolForSpittingVenomProjectile = GetComponent<PoolForSpittingVenomProjectile>();
            enemyController = GetComponent<EnemyMovementandAttack>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _anim = GetComponentInChildren<Animator>();
            _agent = GetComponent<NavMeshAgent>();

            MaxHealth = CurrentHealth;
            _enemyhealthBar.value = MaxHealth - CurrentHealth;
            _enemyhealthBar.maxValue = MaxHealth;

            CreatePatrolArea();
        }

        public void FixedUpdate()
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
                case UnitType.QueenSpider:
                    {
                        transform.LookAt(_player);
                    }
                    break;
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
                        if (Distance <= _attackRange && _reloadSpeed <= 0f)
                        {
                            Vector3 positionFire = _targetFire.position;
                            Quaternion rotationFire = _targetFire.rotation;

                            _anim.SetBool("IsAttack", true);
                            IEventManager.SendSetSpiderAttack();
                            _poolForSpittingVenomProjectile.GetFreeElement(positionFire, rotationFire);

                            _reloadSpeed = 2.5f;
                        }
                        else
                        {
                            _reloadSpeed -= Time.deltaTime;
                            _anim.SetBool("IsAttack", false);
                        }
                    }
                    break;
                case UnitType.QueenSpider:
                    {
                        if (Distance <= _attackRange && _reloadSpeed <= 0f && Distance >= _meleeAttackRange)
                        {
                            Vector3 positionFire = _targetFire.position;
                            Quaternion rotationFire = _targetFire.rotation;

                            _anim.SetBool("IsAttack", true);
                            IEventManager.SendSetSpiderAttack();
                            _poolForSpittingVenomProjectile.GetFreeElement(positionFire, rotationFire);

                            _reloadSpeed = 2.5f;
                        }
                        else if (Distance <= _attackRange && _reloadSpeed <= 0f && Distance <= _meleeAttackRange)
                        {
                            _anim.SetBool("IsMeleeAttack", true);
                            IEventManager.SendSetSpiderMeleeAttack();
                        }
                        else
                        {
                            _reloadSpeed -= Time.deltaTime;
                            _anim.SetBool("IsAttack", false);
                            _anim.SetBool("IsMeleeAttack", false);
                        }
                    }
                    break;
            }
        }

        public void ApplyDamage(int damage)
        {
            CurrentHealth -= damage;

            switch (_unitType)
            {
                case UnitType.Vampire: IEventManager.SendSetDamageEnemy();
                    break;
                case UnitType.Zombi:
                    IEventManager.SendSetDamageEnemy();
                    break;
                case UnitType.Spider: IEventManager.SendSetTakeDamageSpider();
                    break;
                case UnitType.QueenSpider:
                    IEventManager.SendSetTakeDamageSpider();
                    break;
            }

            if (CurrentHealth > 0)
            {
                _anim.SetTrigger("GetDamage");
                CheckCurrentHealth();
            }

            if (CurrentHealth <= 0)
            {
                _anim.SetTrigger("IsDying");
                _agent.speed = _speedMovement = 0f;
                enemyController.enabled = false;
                _colliderDamageTrigger.enabled = false;

                CheckCurrentHealth();

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

        public void CheckCurrentHealth()
        {
            _enemyhealthBar.value = MaxHealth - CurrentHealth;
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
                        _speedMovement = 2f;
                        _speedRunning = 2f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 0.5f;
                        CurrentHealth = 4;
                        _attackRange = 1f;
                        _chaseRange = 8f;
                    }; break;
                case UnitType.Vampire:
                    {
                        _speedMovement = 3f;
                        _speedRunning = 3f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 0.2f;
                        CurrentHealth = 4;
                        _attackRange = 1f;
                        _chaseRange = 12f;
                    }; break;
                case UnitType.Spider:
                    {
                        _speedMovement = 3f;
                        _speedRunning = 3f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 2.5f;
                        CurrentHealth = 5;
                        _attackRange = 10f;
                        _chaseRange = 10f;
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
                case UnitType.QueenSpider:
                    {
                        _speedMovement = 2f;
                        _speedRunning = 2f;
                        _speedRotation = 0.1f;
                        _reloadSpeed = 2.5f;
                        CurrentHealth = 10;
                        _attackRange = 10f;
                        _chaseRange = 10f;
                        _meleeAttackRange = 2f;
                    }; break;
            }
        }
    }
}
