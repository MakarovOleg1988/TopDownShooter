using System;

namespace TopDownShooter
{
    public interface IEventManager
    {
        public static event Action _onSetShootRevolver;
        public static event Action _onSetShootRifle;
        public static event Action _onSetEmptyShootRevolver;
        public static event Action _onSetActiveFlashlight;
        public static event Action _onSetDamagePlayer;
        public static event Action _onSetDamageEnemy;
        public static event Action _onSetReloadWeapon;
        public static event Action _onSetChangeWeapon;

        public static void SendSetShootRevolver()
        {
            _onSetShootRevolver?.Invoke();
        }

        public static void SendSetShootRifle()
        {
            _onSetShootRifle?.Invoke();
        }

        public static void SendSetEmptyShootRevolver()
        {
            _onSetEmptyShootRevolver?.Invoke();
        }

        public static void SendSetActiveFlashlight()
        {
            _onSetActiveFlashlight?.Invoke();
        }

        public static void SendSetDamagePlayer()
        {
            _onSetDamagePlayer?.Invoke();
        }

        public static void SendSetDamageEnemy()
        {
            _onSetDamageEnemy?.Invoke();
        }

        public static void SendSetReloadWeapon()
        {
            _onSetReloadWeapon?.Invoke();
        }

        public static void SendSetChangeWeapon()
        {
            _onSetChangeWeapon?.Invoke();
        }
    }
}
