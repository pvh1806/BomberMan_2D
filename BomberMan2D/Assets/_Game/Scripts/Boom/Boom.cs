using System.Collections;
using _Game.Scripts.Manager;
using UnityEngine;

namespace _Game.Scripts.Boom
{
    public class Boom : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D hitBox;
        public FireBoom explosionPrefab;
        [SerializeField] private ItemsData itemsData;
        private bool isUp = true ;
        private bool isDown = true ;
        private bool isRight = true ;
        private bool isLeft = true ;
        public IEnumerator FireRate(CharacterController characterController, float delayTimer, int col , int row ,int rangeBoom)
        {   
            LevelManager.Ins.loadData.ChangeValue(col ,row , "3");
            //check FireRate
            for (int i = 0; i <= rangeBoom; i++)
            {   
                int newRow ;
                int newCol;
                // check up
                if (isUp)
                {
                    newRow = row ;
                    newCol = col + i;
                    CheckValue(newCol , newRow , ref isUp);
                }
                // check down
                if (isDown)
                {
                    newRow = row;
                    newCol = col - i;
                    CheckValue(newCol , newRow , ref isDown);
                }
                //check left
                if (isLeft)
                {
                    newRow = row -i;
                    newCol = col;
                    CheckValue(newCol , newRow, ref isLeft);
                }
                //check right
                if (isRight)
                {
                    newRow = row +i;
                    newCol = col;
                    CheckValue(newCol , newRow , ref isRight);
                }
            }
            yield return new WaitForSeconds(delayTimer);
            characterController.spawnBoom++;
            Destroy(gameObject);
        }
    
        private void CheckValue(int col, int row, ref bool isBlock)
        {
            LevelManager.Ins.loadData.GetInformation(col, row, out string value, out GameObject block);
            if (value == "3")
            {
                StartCoroutine(SpawnExplosionPrefab(col, row));
                LevelManager.Ins.loadData.ChangeValue(col ,row , "4");
            }
            else if (value == "1" || value == "2" & block != null)
            {
                StartCoroutine(SpawnExplosionPrefabOne(col, row,block , value));
                isBlock = false;
            }
            if (value == "0")
            {
                isBlock = false;
            }
        }

        private IEnumerator SpawnExplosionPrefab(int col , int row )
        {
            yield return new WaitForSeconds(4f);
            Vector2 posSpawn = new Vector2(col + 0.5f,
                row + 0.5f);
            Instantiate(explosionPrefab, posSpawn, Quaternion.identity);
        }
        private IEnumerator SpawnExplosionPrefabOne(int col , int row,GameObject block,string value)
        {
            yield return new WaitForSeconds(4f);
            Vector2 posSpawn = new Vector2(col + 0.5f,
                row + 0.5f);
            Instantiate(explosionPrefab, posSpawn, Quaternion.identity);
            block.gameObject.SetActive(false);
            if (value == "2")
            {
                Instantiate(itemsData.GetRandomItem(), posSpawn, Quaternion.identity);
            }
            LevelManager.Ins.loadData.ChangeValue(col ,row , "3");
        } 
        // collision that activates other bombs if they collide with another explosions of another bomb.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constant.TAG_FIRERATE))
            {
                Destroy(gameObject);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            hitBox.isTrigger = false;
        }
    }
}
