using System.Collections.Generic;
using _Game.Scripts.Character;
using UnityEngine;

namespace _Game.Scripts.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        public LoadData loadData;
        [SerializeField] private PlayerController player1Prefab, player2Prefab;
        [SerializeField] private Bot botPrefab;
        [SerializeField] private List<Transform> lstPosSpawn = new List<Transform>();
        private int botCount, player1Count = 1, player2Count = 1;
        private int index = -1;
        void Start()
        {
            loadData.OnInit();
            for (int i = 0; i < 2; i++)
            {
                index++;
                Bot botSpawn = Instantiate(botPrefab, lstPosSpawn[index].position,Quaternion.identity);
                botSpawn.OnInit();
            }

            if (player1Count == 1)
            {
                index++;
                PlayerController playerControllerSpawn = Instantiate(player1Prefab, lstPosSpawn[index]);
            }

            if (player2Count == 1)
            {
                index++;
                PlayerController playerControllerSpawn = Instantiate(player2Prefab, lstPosSpawn[index]);
            }
        }
    }
}