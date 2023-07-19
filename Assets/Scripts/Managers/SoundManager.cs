using UnityEngine;

namespace TopDownShooter
{
    public class SoundManager : MonoBehaviour, IEventManager
    {
        [SerializeField]
        private AudioSource _shootRevolverSound;

        [SerializeField]
        private AudioSource _shootRifleSound;

        [SerializeField]
        private AudioSource _ActiveFlashlightSound;

        [SerializeField]
        private AudioSource _DamageEnemySoundSound;

        [SerializeField]
        private AudioSource _emptyShootRevolverSound;

        [SerializeField]
        private AudioSource _reloadRevolverSound;

        [SerializeField]
        private AudioSource _takeDamageSpiderSound;

        [SerializeField]
        private AudioSource _spiderAttackSound;

        [SerializeField]
        private AudioSource _spiderMeleeAttackSound;

        [SerializeField]
        private AudioSource _moneyDownSound;

        private void Start()
        {
            IEventManager._onSetShootRevolver += SetShootRevolverSound;
            IEventManager._onSetShootRifle += SetShootRifleSound;
            IEventManager._onSetActiveFlashlight += SetActiveFlashlightSound;
            IEventManager._onSetDamageEnemy += SetDamageEnemySound;
            IEventManager._onSetEmptyShootRevolver += SetEmptyShootRevolverSound;
            IEventManager._onSetReloadWeapon += SetReloadWeaponSound;
            IEventManager._onSetTakeDamageSpider += SetTakeDamageSpiderSound;
            IEventManager._onSetSpiderAttack += SetSpiderAttackSound;
            IEventManager._onSetSpiderMeleeAttack += SetSpiderAttackMeleeSound;
            IEventManager._onSetMoneyDown += SetSMoneyDownSound;
        }

        private void SetShootRevolverSound()
        {
            _shootRevolverSound.Play();
        }

        private void SetShootRifleSound()
        {
            _shootRifleSound.Play();
        }

        private void SetEmptyShootRevolverSound()
        {
            _emptyShootRevolverSound.Play();
        }

        private void SetActiveFlashlightSound()
        {
            _ActiveFlashlightSound.Play();
        }

        private void SetDamageEnemySound()
        {
            _DamageEnemySoundSound.Play();
        }

        private void SetReloadWeaponSound()
        {
            _reloadRevolverSound.Play();
        }

        private void SetTakeDamageSpiderSound()
        {
            _takeDamageSpiderSound.Play();
        }

        private void SetSpiderAttackSound()
        {
            _spiderAttackSound.Play();
        }

        private void SetSpiderAttackMeleeSound()
        {
            _spiderMeleeAttackSound.Play();
        }

        private void SetSMoneyDownSound()
        {
            _moneyDownSound.Play();
        }

        private void OnDestroy()
        {
            IEventManager._onSetShootRevolver -= SetShootRevolverSound;
            IEventManager._onSetShootRifle -= SetShootRifleSound;
            IEventManager._onSetActiveFlashlight -= SetActiveFlashlightSound;
            IEventManager._onSetDamageEnemy -= SetDamageEnemySound;
            IEventManager._onSetEmptyShootRevolver -= SetEmptyShootRevolverSound;
            IEventManager._onSetReloadWeapon -= SetReloadWeaponSound;
            IEventManager._onSetTakeDamageSpider -= SetTakeDamageSpiderSound;
            IEventManager._onSetSpiderAttack -= SetSpiderAttackSound;
            IEventManager._onSetSpiderMeleeAttack -= SetSpiderAttackMeleeSound;
            IEventManager._onSetMoneyDown -= SetSMoneyDownSound;
        }
    }
}
