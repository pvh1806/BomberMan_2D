using UnityEngine;

namespace _Game.ScriptsTableObj
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
    public class Level : ScriptableObject
    {
        public string filePath;
        public Sprite imageBackGround;
        public GameObject[] prefabs;
        public GameObject backGround;
        public int countSpawn;
        public float posSpawn;
        public float timer;
    }
}
