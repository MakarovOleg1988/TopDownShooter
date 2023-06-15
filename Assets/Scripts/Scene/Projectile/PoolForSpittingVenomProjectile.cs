using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class PoolForSpittingVenomProjectile : MonoBehaviour
    {
        [SerializeField]
        private Projectile _prefabVenom;

        [SerializeField] 
        private Transform _container;

        [SerializeField] 
        private short _minCapacity;
        
        [SerializeField] 
        private short _maxCapacity;

        private List<Projectile> _poolVenom;

        [SerializeField]
        private bool _autoExpand;

        private void OnValidate()
        {
            if (_autoExpand)
            {
                _maxCapacity = short.MaxValue;  
            }
        }

        private void Start()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            _poolVenom = new List<Projectile>(_minCapacity);

            for (int i = 0; i < _minCapacity; i++)
            {
                CreateElement();
            }
        }

        private Projectile CreateElement(bool isActiveByDefault = false)
        {
            Projectile createdObject = Instantiate(_prefabVenom, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);

            _poolVenom.Add(createdObject);
            return createdObject;
        }

        private bool TryGetElement(out Projectile element)
        {
            foreach (var item in _poolVenom)
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

            if (_poolVenom.Count < _maxCapacity)
            {
                return CreateElement(true);
            }

            throw new Exception("Pool is over"); 
        }
    }
}
