using UnityEngine;
public class PlayerController : CharacterController
{
    public Vector2 direction = Vector2.down;
    private bool isUpSpawneable = true;
    private bool isDownSpawneable = true;
    private bool isLeftSpawneable = true;
    private bool isRightSpawneable = true;
    [SerializeField] private KeyCode left, right, top, down , boom;
    private void Update()
    {
        if (Input.GetKey(left))
        {
           Left();
        }
        else if (Input.GetKey(right))
        {
            Right();
        }
        else if (Input.GetKey(top))
        {
           Top();
        }
        else if (Input.GetKey(down))
        {
            Down();
        }
        else
        {
            Idle();
        }
        //dat boom 
        if (Input.GetKey(boom))
        {
            Boom();
        }
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(position + translation);
    }
    private void Left()
    {
        SetDirection(Vector2.left,Constant.ANIM_LeftRight);
        spriteRenderer.flipX = false;
    }

    private void Right()
    {
        SetDirection(Vector2.right ,Constant.ANIM_LeftRight);
        spriteRenderer.flipX = true;
    }

    private void Top()
    {
        SetDirection(Vector2.up,Constant.ANIM_UP);
        spriteRenderer.flipX = false;
    }
    private void Down()
    {
        SetDirection(Vector2.down,Constant.ANIM_DOWN);
        spriteRenderer.flipX = false;
    }

    private void Idle()
    {
        SetDirection(Vector2.zero,Constant.ANIM_IDLE);
        spriteRenderer.flipX = false;
    }
    // Huong ,anim
    private void SetDirection(Vector2 newDirection , string animName)
    {
        direction = newDirection;
        ChangeAnim(animName);
    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.GetComponent<EnemyMovement>())
    //         Damage();
    //     else if (collision.gameObject.tag == "Door")
    //     {
    //         Debug.Log("Next Level");
    //         GameManager.instance.Win();
    //     }
    // }
    // public void Damage()
    // {
    //     isAlive = false;
    //     GameManager.instance.GameStatus(false);
    //     Destroy(gameObject);
    // }
}

