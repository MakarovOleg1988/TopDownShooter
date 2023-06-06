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

        private void Start()
        {
            IEventManager._onSetShootRevolver += SetShootRevolverSound;
            IEventManager._onSetShootRifle += SetShootRifleSound;
            IEventManager._onSetActiveFlashlight += SetActiveFlashlightSound;
            IEventManager._onSetDamageEnemy += SetDamageEnemySound;
            IEventManager._onSetEmptyShootRevolver += SetEmptyShootRevolverSound;
            IEventManager._onSetReloadWeapon += SetReloadWeaponSound;
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

        private void OnDestroy()
        {
            IEventManager._onSetShootRevolver -= SetShootRevolverSound;
            IEventManager._onSetShootRifle -= SetShootRifleSound;
            IEventManager._onSetActiveFlashlight -= SetActiveFlashlightSound;
            IEventManager._onSetDamageEnemy -= SetDamageEnemySound;
            IEventManager._onSetEmptyShootRevolver -= SetEmptyShootRevolverSound;
            IEventManager._onSetReloadWeapon -= SetReloadWeaponSound;
        }
    }
}
