using UnityEngine;

namespace TopDownShooter
{
    [CreateAssetMenu(fileName = "NPSPhrase", menuName = "NPS/Create Phrases List")]
    public class PhraseData : ScriptableObject
    {
        public string[] phrases;
    }
}
