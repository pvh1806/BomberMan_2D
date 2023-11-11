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
        private int botCount, player1Count, player2Count;

        void Start()
        {
            loadData.OnInit();
            for (int i = 0; i < 2; i++)
            {
                Bot botSpawn = Instantiate(botPrefab, lstPosSpawn[0].position,Quaternion.identity);
                lstPosSpawn.RemoveAt(0);
                botSpawn.OnInit();
            }

            if (player1Count == 1)
            {
                PlayerController playerControllerSpawn = Instantiate(player1Prefab, lstPosSpawn[0]);
                lstPosSpawn.RemoveAt(0);
            }

            // if (player2Count == 1)
            // {
            //     PlayerController playerControllerSpawn = Instantiate(player2Prefab, lstPosSpawn[0]);
            //     lstPosSpawn.RemoveAt(0);
            // }
        }
    }
}