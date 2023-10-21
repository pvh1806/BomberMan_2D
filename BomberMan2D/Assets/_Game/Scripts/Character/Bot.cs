using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : CharacterController
{
    private List<Vector3> lstTarget = new List<Vector3>();
    private Vector3 target;
    private bool isUp = true ;
    private bool isDown = true ;
    private bool isRight = true ;
    private bool isLeft = true ;
    private Vector3 posSpawnBoom;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target) < 0.001f)
        {
            lstTarget.Clear();
            isUp = true;
            isDown = true;
            isRight = true;
            isLeft = true;
            Move();
        }
    }

    private void CheckCanMove()
    {
        int newRow ;
            int newCol;
            // check up
            if (isUp)
            {
                newRow = row ;
                newCol = col + 1;
                CheckValue(newCol , newRow , ref isUp);
            }
            // check down
            if (isDown)
            {
                newRow = row;
                newCol = col - 1;
                CheckValue(newCol , newRow , ref isDown);
            }
            //check left
            if (isLeft)
            {
                newRow = row - 1;
                newCol = col;
                CheckValue(newCol , newRow, ref isLeft);
            }
            //check right
            if (isRight)
            {
                newRow = row + 1;
                newCol = col;
                CheckValue(newCol , newRow , ref isRight);
            }
    }

    public void Move()
    {
        LevelManager.Ins.loadData.GetPos(transform.position, out col, out row);
        CheckCanMove();
        SetTarget();
        SetAnim(target);
    }
    private void CheckValue(int col, int row, ref bool isBlock)
    {
        LevelManager.Ins.loadData.GetInformation(col, row, out string value, out GameObject block);
        if (value == "3")
        {
           AddTarget(col, row);
        }
        else 
        {
            isBlock = false;
        }
    }

    private void AddTarget(int col , int row)
    {
        Vector3 target = new Vector2(col + 0.5f,
            row + 0.5f);
        lstTarget.Add(target);
    }

    private void SetTarget()
    {
        if(lstTarget.Count > 1)
        {
            int random = Random.Range(0, lstTarget.Count);
            Vector3 randomTarget = lstTarget[random];
            target = randomTarget;
        }
        else if(lstTarget.Count == 1)
        {
            if (spawnBoom > 0)
            {
                Boom();
            }
            target = lstTarget[0];
        }
        else
        {
            target = transform.position;
        }
        
    }
    private void SetAnim(Vector3 pos)
    { 
        //trai
        if (pos.x < transform.position.x)
        {
            ChangeAnim(Constant.ANIM_LeftRight);
            spriteRenderer.flipX = false;
        }
        //phai
        else if (pos.x > transform.position.x)
        {
            ChangeAnim(Constant.ANIM_LeftRight);
            spriteRenderer.flipX = true;
        }
        //tren
        else if (pos.y > transform.position.y)
        {
            ChangeAnim(Constant.ANIM_UP);
            spriteRenderer.flipX = false;
        }
        //duoi
        else if (pos.y < transform.position.y)
        {
            ChangeAnim(Constant.ANIM_DOWN);
            spriteRenderer.flipX = false;
        }
        //hientai
        else if (pos == transform.position)
        {
            ChangeAnim(Constant.ANIM_IDLE);
            spriteRenderer.flipX = false;
        }
    }
}
