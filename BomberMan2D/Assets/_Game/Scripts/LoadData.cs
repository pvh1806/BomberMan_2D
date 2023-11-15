using System.IO;
using _Game.Scripts.Manager;
using _Game.ScriptsTableObj;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class LoadData : MonoBehaviour
    {
        private string filePath;
        public string[,] mapData;
        private GameObject[,] spawnedObjects;
        [SerializeField] private LevelData levelData;
        [SerializeField] private Image imageBackground;
        private Level levelCurrent;
        public int countSpawn;
        public float posSpawn;
        private float timer;
        public void OnInit()
        {
            OnReSources();
            if (levelCurrent != null)
            {
                LoadMapFromFile();
                GenerateMap();
                LoadBackGround();
                timer = levelCurrent.timer;
                Timer();
            }
        }

        private void Timer()
        {
            LevelManager.Ins.textTimer.SetText( timer.ToString());
            timer--;
            if (timer > 0)
            {
                Invoke(nameof(Timer),1f);
                if (timer < 20)
                {
                    LevelManager.Ins.textTimer.color = Color.red;
                }
            }
            else
            {
                if (LevelManager.Ins.playerCount < 2)
                {
                    LevelManager.Ins.Dead("Bot Win");
                }
                else
                {
                    LevelManager.Ins.Dead("No Player Win");
                }
            }
        }
        private void OnReSources()
        {
            levelCurrent = levelData.levelCurrent;
            filePath = levelCurrent.filePath;
            imageBackground.sprite = levelCurrent.imageBackGround;
        }

        private void LoadBackGround()
        {
            countSpawn = levelCurrent.countSpawn;
            posSpawn = levelCurrent.posSpawn;
            for (int i = 0; i < countSpawn; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Vector3 position = new Vector3(i + posSpawn, j + posSpawn, 0f);
                    var x = Instantiate(levelCurrent.backGround, position, Quaternion.identity);
                }
            }
        }

        private void LoadMapFromFile()
        {
            string[] lines = File.ReadAllLines(filePath);
            int rowCount = lines.Length;
            int columnCount = lines[0].Length;
            mapData = new string[rowCount, columnCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    string value = lines[row][col].ToString();
                    mapData[row, col] = value;
                }
            }
        }

        public void GetPos(Vector3 pos, out int col, out int row)
        {
            col = Mathf.FloorToInt(pos.x);
            row = Mathf.FloorToInt(pos.y);
        }


        public void GetInformation(int col, int row, out string value, out GameObject obj)
        {
            if (col >= 0 && col < mapData.GetLength(1) && row >= 0 && row < mapData.GetLength(0))
            {
                value = mapData[row, col];
                obj = spawnedObjects[row, col];
            }
            else
            {
                value = null;
                obj = null;
            }
        }

        public void ChangeValue(int col, int row, string value)
        {
            mapData[row, col] = value;
        }

        private void GenerateMap()
        {
            int rowCount = mapData.GetLength(0);
            int columnCount = mapData.GetLength(1);
            spawnedObjects = new GameObject[rowCount, columnCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    string value = mapData[row, col];
                    if (int.TryParse(value, out int intValue) && intValue >= 0 && intValue <= 3 && intValue != 3)
                    {
                        GameObject prefab = levelCurrent.prefabs[intValue];
                        Vector3 position = new Vector3(col, row, 0f);
                        {
                            var x = Instantiate(prefab, position, levelCurrent.prefabs[intValue].transform.rotation);
                            spawnedObjects[row, col] = x;
                        }
                    }
                }
            }
        }
    }
}