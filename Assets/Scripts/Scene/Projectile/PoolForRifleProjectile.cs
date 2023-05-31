using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class PoolForRifleProjectile : MonoBehaviour
    {
        [SerializeField]
        private Projectile _prefabRifle;

        [SerializeField] 
        private Transform _container;

        [SerializeField] 
        private int _minCapacity;
        
        [SerializeField] 
        private int _maxCapacity;

        private List<Projectile> _poolRifle;

        [SerializeField]
        private bool _autoExpand;

        private void OnValidate()
        {
            if (_autoExpand)
            {
                _maxCapacity = Int32.MaxValue;  
            }
        }

        private void Start()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            _poolRifle = new List<Projectile>(_minCapacity);

            for (int i = 0; i < _minCapacity; i++)
            {
                CreateElement();
            }
        }

        private Projectile CreateElement(bool isActiveByDefault = false)
        {
            Projectile createdObject = Instantiate(_prefabRifle, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);

            _poolRifle.Add(createdObject);
            return createdObject;
        }

        private bool TryGetElement(out Projectile element)
        {
            foreach (var item in _poolRifle)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    element = item;
                    item.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public Projectile GetFreeElement(Vector3 position, Quaternion rotation)
        {
            var element = GetFreeElement(position);
            element.transform.rotation = rotation;
            return element;
        }

        public Projectile GetFreeElement(Vector3 position)
        {
            var element = GetFreeElement();
            element.transform.position = position;
            return element;
        }

        public Projectile GetFreeElement()
        {
            if (TryGetElement(out var element))
            {
                return element;
            }

            if (_autoExpand)
            {
                return CreateElement(true);
            }

            if (_poolRifle.Count < _maxCapacity)
            {
                return CreateElement(true);
            }

            throw new Exception("Pool is over"); 
        }
    }
}
