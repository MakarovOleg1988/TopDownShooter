using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
    public class ItemFrame : MonoBehaviour
    {
        [SerializeField]
        private Material[] materials;
        
        private Renderer objectRenderer; 

        private int currentMaterialIndex = 0;

        [SerializeField, Range(1f, 10f)]
        private float _timeForChange;

        private void Start()
        {
            objectRenderer = GetComponent<Renderer>();
            objectRenderer.material = materials[currentMaterialIndex];

            StartCoroutine(ChangeMaterialCoroutine());
        }

        private IEnumerator ChangeMaterialCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeForChange);
                currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
                objectRenderer.material = materials[currentMaterialIndex];
            }
        }
    }
}
