using System.Collections.Generic;
using _Game.Scripts.Canvas;
using _Game.Scripts.Character;
using _Game.ScriptsTableObj;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        public LoadData loadData;
        [SerializeField] private PlayerController[] playerPrefab;
        [SerializeField] private HeathPlayer[] heathPlayer;
        [SerializeField] private Bot botPrefab;
        [SerializeField] private List<Transform> lstPosSpawn = new List<Transform>();
        [SerializeField] private LevelData levelData;
        public TextMeshProUGUI textTimer;
        private int index = -1;
        void Start()
        {
            loadData.OnInit();
            for (int i = 0; i < levelData.botCount ; i++)
            {
                index++;
                Bot botSpawn = Instantiate(botPrefab, lstPosSpawn[index].position,Quaternion.identity);
                botSpawn.OnInit();
            }

            for (int i = 0; i < levelData.playerCount ; i++)
            {
                index++;
                PlayerController playerSpawn = Instantiate(playerPrefab[i], lstPosSpawn[index].position,Quaternion.identity);
                playerSpawn.OnInit(heathPlayer[i]);
            }
        }

        public void Dead()
        {
            
        }
    }
}