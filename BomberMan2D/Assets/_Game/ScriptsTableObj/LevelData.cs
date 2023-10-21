using UnityEngine;
public enum LevelType
{
    Level0 = 0 ,
    Level1 = 1 ,
    Level2 = 2 ,
    Level3 = 3 ,
}
[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public Level[] level;
    public Level levelCurrent;
    public int levelTotal;
    public Level GetLevel(LevelType levelType)
    {
        return level[(int)levelType];
    }
}