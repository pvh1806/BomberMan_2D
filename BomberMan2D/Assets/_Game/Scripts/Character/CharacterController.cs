using _Game.Scripts.Boom;
using _Game.Scripts.Manager;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int health;
    public int moveSpeed;
    public int rangeBoom ;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public int row, col;
    public Boom boomPrefab;
    public int spawnBoom;
    private string animName;
    public SpriteRenderer spriteRenderer;
    public void ChangeAnim(string animNameType)
    {
        if (animName != animNameType)
        {
            animator.ResetTrigger(animName);
            animName = animNameType;
            animator.SetTrigger(animName);
        }
    }
    
    protected void Boom()
    {
        if (spawnBoom > 0)
        {
            spawnBoom--;
            LevelManager.Ins.loadData.GetPos(transform.position, out col, out row);
            Vector2 posSpawn = new Vector2(col + 0.5f, row +  0.5f);
            Boom boomSpawn = Instantiate(boomPrefab, posSpawn, Quaternion.identity);
            boomSpawn.StartCoroutine(boomSpawn.FireRate(this, 4f, col,row , rangeBoom));
            LevelManager.Ins.loadData.ChangeValue(col ,row , "0");
        }
    }
    public void Die()
    {
        
    }
}
