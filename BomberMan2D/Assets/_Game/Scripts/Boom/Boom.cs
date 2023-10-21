using System.Collections;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private BoxCollider2D hitBox;
    public GameObject explosionPrefab;
    private CharacterController characterController;
    [SerializeField] private ItemsData itemsData;
    private bool isUp = true ;
    private bool isDown = true ;
    private bool isRight = true ;
    private bool isLeft = true ;

    public IEnumerator FireRate(CharacterController characterController, float delayTimer, int col , int row ,int rangeBoom)
    {
        yield return new WaitForSeconds(delayTimer);
        this.characterController = characterController;
        Vector2 posSpawn = new Vector2(col + 0.5f,
            row + 0.5f);
        Instantiate(explosionPrefab, posSpawn, Quaternion.identity);
        Destroy(gameObject);
        LevelManager.Ins.loadData.ChangeValue(col ,row , "3");
        //check FireRate
        for (int i = 1; i <= rangeBoom; i++)
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
        characterController.spawnBoom++;
    }

    private void CheckValue(int col, int row, ref bool isBlock)
    {
        LevelManager.Ins.loadData.GetInformation(col, row, out string value, out GameObject block);
        if (value == "3")
        {
            SpawnExplosionPrefab(col, row);
        }
        else if (value == "1" & block != null)
        {
            SpawnExplosionPrefab(col, row);
            block.gameObject.SetActive(false);
            LevelManager.Ins.loadData.ChangeValue(col ,row , "3");
            isBlock = false;
        }
        else if (value == "2" & block != null)
        {
            Vector2 posSpawn = new Vector2(col + 0.5f,
                row + 0.5f);
            SpawnExplosionPrefab(col, row);
            block.gameObject.SetActive(false);
            LevelManager.Ins.loadData.ChangeValue(col ,row , "3");
            isBlock = false;
            var x = Instantiate(itemsData.GetRandomItem(), posSpawn, Quaternion.identity);
        }
        if (value == "0")
        {
            isBlock = false;
        }
    }

    private void SpawnExplosionPrefab(int col , int row)
    {
        Vector2 posSpawn = new Vector2(col + 0.5f,
            row + 0.5f);
        Instantiate(explosionPrefab, posSpawn, Quaternion.identity);
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
