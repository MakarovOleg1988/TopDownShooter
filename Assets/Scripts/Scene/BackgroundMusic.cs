using System.Collections;
using UnityEngine;

namespace TopDownShooter
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField]
        private bool isVillage;

        [SerializeField]
        private bool isForest;

        public AudioSource _source;
        public AudioClip _villageMusic;
        public AudioClip _forestMusic;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>() && isVillage == true) _source.PlayOneShot(_villageMusic);
            if (other.GetComponent<PlayerController>() && isForest == true) _source.PlayOneShot(_forestMusic);
        }
    }
}