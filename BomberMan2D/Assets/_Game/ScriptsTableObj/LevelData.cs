using UnityEngine;

namespace _Game.ScriptsTableObj
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        public Level[] level;
        public Level levelCurrent;
        public int playerCount, botCount , mapCount;

        public void SetLevel()
        {
            levelCurrent = level[mapCount];
        }
    }
}