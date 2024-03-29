using UnityEngine;

namespace TopDownShooter
{
    public class PlayerDamageTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnitType _unitType;
        
        private int _damage;

        private void Start()
        {
            ChooseEnemy(_unitType);
        }

        public void ChooseEnemy(UnitType _unitType)
        {
            switch (_unitType)
            {
                case UnitType.Zombi:
                    {
                        _damage = 1;
                    }; break;
                case UnitType.Vampire:
                    {
                        _damage = 2;
                    }; break;
                case UnitType.Player:
                    {
                        _damage = 0;
                    }; break;
                case UnitType.Citizen:
                    {
                        _damage = 0;
                    }; break;
                case UnitType.Spider:
                    {
                        _damage = 3;
                    }; break;
                case UnitType.QueenSpider:
                    {
                        _damage = 1;
                    }; break;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageAblePlayer>(out var player))
            {
                player.ApplyDamagePlayer(_damage);
            }
        }
    }
}
