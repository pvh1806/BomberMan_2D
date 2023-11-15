using UnityEngine;

namespace _Game.ScriptsTableObj
{
    [CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsData")]
    public class ItemsData : ScriptableObject
    {
        public Items[] item;
        public Items GetRandomItem()
        {
            int random = Random.Range(0, 3);
            return item[random];
        }
    }
}