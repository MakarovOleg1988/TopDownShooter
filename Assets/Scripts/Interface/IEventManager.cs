using System;

namespace TopDownShooter
{
    public interface IEventManager
    {
        public static event Action _onSetShootRevolver;
        public static event Action _onSetShootRifle;
        public static event Action _onSetEmptyShootRevolver;
        public static event Action _onSetActiveFlashlight;
        public static event Action _onSetDamageEnemy;
        public static event Action _onSetReloadWeapon;
        public static event Action _onSetChangeWeapon;
        public static event Action _onSetClickButton;
        public static event Action _onSetTakeDamageSpider;
        public static event Action _onSetSpiderAttack;
        public static event Action _onSetMoneyDown;

        public static event Action _onSetKillVampire;

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

        public static void SendSetClickButton()
        {
            _onSetClickButton?.Invoke();
        }

        public static void SendSetTakeDamageSpider()
        {
            _onSetTakeDamageSpider?.Invoke();
        }

        public static void SendSetSetSpiderAttack()
        {
            _onSetSpiderAttack?.Invoke();
        }

        public static void SendSetMoneyDown()
        {
            _onSetMoneyDown?.Invoke();
        }

        public static void SendSetKillVampire()
        {
            _onSetKillVampire?.Invoke();
        }
    }
}
