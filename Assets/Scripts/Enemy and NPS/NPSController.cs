using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(NavMeshAgent))]
    public class NPSController : NPSParam
    {
        private void OnValidate()
        {
            ChooseUnit(_unitType);
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _anim = GetComponentInChildren<Animator>();
        }

        public void Update()
        {
            MovementNPS(_steps);
        }

        private void MovementNPS(Transform[] _steps)
        {
            StartCoroutine(MovementNPSCoroutine(_steps));
        }

        private IEnumerator MovementNPSCoroutine(Transform[] _steps)
        {
            int random = Random.Range(0, _steps.Length - 1);

            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                _agent.speed = _speedMovement;
                _agent.SetDestination(_steps[random].position);
                _anim.SetBool("IsMoving", true);
                yield return new WaitForSeconds(2f);
            }
        }

        public void ChooseUnit(UnitType _unitType)
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
