using UnityEngine;

namespace TopDownShooter
{
    [CreateAssetMenu(fileName = "Store Item", menuName = "Shop/Create Store Item List")]
    public class StoreItemData : ScriptableObject
    {
        public string[] nameItem;
        public int[] costItem;
        public string[] discriptionItem;
        public Sprite[] spriteItem;
    }
}