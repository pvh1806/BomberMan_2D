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
    private bool isSpawnBoom = true;
    public bool isDead;
    protected void ChangeAnim(string animNameType)
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
        if (spawnBoom > 0 && isSpawnBoom)
        {
            isSpawnBoom = false;
            spawnBoom--;
            LevelManager.Ins.loadData.GetPos(transform.position, out col, out row);
            Vector2 posSpawn = new Vector2(col + 0.5f, row +  0.5f);
            Boom boomSpawn = Instantiate(boomPrefab, posSpawn, Quaternion.identity);
            boomSpawn.StartCoroutine(boomSpawn.FireRate(this, 4f, col,row , rangeBoom));
            LevelManager.Ins.loadData.ChangeValue(col ,row , "0");
            Invoke(nameof(SetActiveBoolSpawnBoom),1f);
        }
    }

    private void SetActiveBoolSpawnBoom()
    {
        isSpawnBoom = true;
    }
    public void Die()
    {
        ChangeAnim(Constant.ANIM_DIE);
        Invoke(nameof(SetActive),1.2f);
    }

    protected void SetActive()
    {
        gameObject.SetActive(false);
    }
}
