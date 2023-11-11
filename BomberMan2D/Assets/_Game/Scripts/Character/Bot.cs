using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Bot : CharacterController
    {
        private List<Vector3> lstTarget = new List<Vector3>();
        private Vector3 target;
        private bool isUp = true;
        private bool isDown = true;
        private bool isRight = true;
        private bool isLeft = true;
        private Vector3 posSpawnBoom;
        private LoadData loadData;
        private List<Vector3> lstTargetBoomUp = new List<Vector3>();
        private List<Vector3> lstTargetBoomRight = new List<Vector3>();
        private List<Vector3> lstTargetBoomLeft = new List<Vector3>();
        private List<Vector3> lstTargetBoomDown = new List<Vector3>();
        private List<List<Vector3>> lstCheckBoom = new List<List<Vector3>>();
        private int lastX;
        private int lastY;
        private int index;
        private bool isCheckBoomUp = true;
        private bool isCheckBoomDown = true;
        private bool isCheckBoomLeft = true;
        private bool isCheckBoomRight = true;
        private bool isCheck;
        private List<Vector3> y;

        private enum StateBot
        {
            Move,
            Boom
        };

        private StateBot currentState;

        public void OnInit()
        {
            loadData = LevelManager.Ins.loadData;
            Move();
            currentState = StateBot.Move;
        }

        void Update()
        {
            loadData.GetPos(transform.position, out col, out row);
            var x = loadData.mapData[row, col];
            if (x == "4")
            {
                currentState = StateBot.Boom;
                //ChangeAnim(Constant.ANIM_IDLE);
            }

            if (currentState == StateBot.Move)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, target) < 0.001f)
                {
                    lstTarget.Clear();
                    isUp = true;
                    isDown = true;
                    isRight = true;
                    isLeft = true;
                    Move();
                }
            }

            if (currentState == StateBot.Boom)
            {
                if (!isCheck)
                {
                    isCheck = true;
                    ListGoNotBoom();
                    CheckListGoNotBoom();
                    y = FindSmallestList(lstCheckBoom);
                }

                if (y == null)
                {
                    Debug.Log("lists null");
                    ChangeAnim(Constant.ANIM_IDLE);
                }
                else
                {
                    //Debug.Log(y.Count);
                    if (index < y.Count)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, y[index], moveSpeed * Time.deltaTime);
                        // Check the distance to the current position, not the original target
                        if (Vector3.Distance(transform.position, y[index]) < 0.01f)
                        {
                            index++;
                            SetAnim(y[index]);
                        }
                    }
                    else
                    {
                        isCheckBoomUp = true;
                        isCheckBoomDown = true;
                        isCheckBoomLeft = true;
                        isCheckBoomRight = true;
                        lstCheckBoom.Clear();
                        lstTargetBoomUp.Clear();
                        lstTargetBoomDown.Clear();
                        lstTargetBoomLeft.Clear();
                        lstTargetBoomRight.Clear();
                        isCheck = false;
                        index = 0;
                        Move();
                        currentState = StateBot.Move; 
                    }
                   
                }
            }
        }

        private List<Vector3> FindSmallestList(List<List<Vector3>> lists)
        {
            if (lists.Count == 0)
            {
                return null;
            }

            // Use LINQ to find the list with the minimum number of elements
            var smallestList = lists
                .OrderBy(list => list.Count)
                .FirstOrDefault();

            return smallestList;
        }

        private void CheckCanMove()
        {
            int newRow;
            int newCol;
            // check up
            if (isUp)
            {
                newRow = row;
                newCol = col + 1;
                CheckValue(newCol, newRow, ref isUp);
            }

            // check down
            if (isDown)
            {
                newRow = row;
                newCol = col - 1;
                CheckValue(newCol, newRow, ref isDown);
            }

            //check left
            if (isLeft)
            {
                newRow = row - 1;
                newCol = col;
                CheckValue(newCol, newRow, ref isLeft);
            }

            //check right
            if (isRight)
            {
                newRow = row + 1;
                newCol = col;
                CheckValue(newCol, newRow, ref isRight);
            }
        }

        private void Move()
        {
            loadData.GetPos(transform.position, out col, out row);
            CheckCanMove();
            SetTarget();
            SetAnim(target);
        }

        private void CheckValue(int col, int row, ref bool isBlock)
        {
            var x = loadData.mapData[row, col];
            if (x == "3" || x=="4")
            {
                AddTarget(col, row);
            }
            else
            {
                isBlock = false;
            }
        }

        private void AddTarget(int col, int row)
        {
            Vector3 target = new Vector2(col + 0.5f,
                row + 0.5f);
            lstTarget.Add(target);
        }

        private void CheckListGoNotBoom()
        {
            if (lstTargetBoomUp.Count > 0)
            {
                lastX = (int)lstTargetBoomUp[lstTargetBoomUp.Count - 1].x;
                lastY = (int)lstTargetBoomUp[lstTargetBoomUp.Count - 1].y;
                if (loadData.mapData[lastY, lastX] == "3")
                {
                    lstCheckBoom.Add(lstTargetBoomUp);
                    Debug.Log("1");
                }
            }

            if (lstTargetBoomDown.Count > 0)
            {
                Debug.Log("2");
                lastX = (int)lstTargetBoomDown[lstTargetBoomDown.Count - 1].x;
                lastY = (int)lstTargetBoomDown[lstTargetBoomDown.Count - 1].y;
                if (loadData.mapData[lastY, lastX] == "3")
                {
                    lstCheckBoom.Add(lstTargetBoomDown);
                }
            }

            if (lstTargetBoomLeft.Count > 0)
            {
                lastX = Mathf.RoundToInt(lstTargetBoomLeft[lstTargetBoomLeft.Count - 1].x);
                lastY = Mathf.RoundToInt(lstTargetBoomLeft[lstTargetBoomLeft.Count - 1].y);
                if (loadData.mapData[lastY, lastX] == "3")
                {
                    lstCheckBoom.Add(lstTargetBoomLeft);
                    Debug.Log("3");
                }
            }

            if (lstTargetBoomRight.Count > 0)
            {
                lastX = Mathf.RoundToInt(lstTargetBoomRight[lstTargetBoomRight.Count - 1].x);
                lastY = Mathf.RoundToInt(lstTargetBoomRight[lstTargetBoomRight.Count - 1].y);
                if (loadData.mapData[lastY, lastX] == "3")
                {
                    lstCheckBoom.Add(lstTargetBoomRight);
                    Debug.Log("4");
                }
            }
        }

        private void ListGoNotBoom()
        {
            for (int i = 1; i <= 4; i++)
            {
                //if (i < 3)
                //{
                //checkUp
                if (isCheckBoomUp)
                {
                    if (loadData.mapData[row, col + i] == "3" || loadData.mapData[row, col + i] == "4")
                    {
                        lstTargetBoomUp.Add(new Vector3(col + i + 0.5f, row + 0.5f, 0f));
                        if (loadData.mapData[row - 1, col + i] == "3")
                        {
                            lstTargetBoomUp.Add(new Vector3(col + i + 0.5f, row - 0.5f, 0f));
                            break;
                        }

                        if (loadData.mapData[row + 1, col + i] == "3")
                        {
                            lstTargetBoomUp.Add(new Vector3(col + i + 0.5f, row + 1.5f, 0f));
                            break;
                        }
                    }
                    else
                    {
                        isCheckBoomUp = false;
                    }
                }

                //checkDown
                if (isCheckBoomDown)
                {
                    if (loadData.mapData[row, col - i] == "3" || loadData.mapData[row, col - i] == "4")
                    {
                        lstTargetBoomDown.Add(new Vector3(col - i + 0.5f, row + 0.5f, 0f));
                        if (loadData.mapData[row - 1, col - i] == "3")
                        {
                            lstTargetBoomDown.Add(new Vector3(col - i + 0.5f, row - 0.5f, 0f));
                            break;
                        }

                        if (loadData.mapData[row + 1, col - i] == "3")
                        {
                            lstTargetBoomDown.Add(new Vector3(col - i + 0.5f, row + 1.5f, 0f));
                            break;
                        }
                    }
                    else
                    {
                        isCheckBoomDown = false;
                    }
                }


                //checkLeft
                if (isCheckBoomLeft)
                {
                    if (loadData.mapData[row - i, col] == "3" || loadData.mapData[row - i, col] == "4")
                    {
                        lstTargetBoomLeft.Add(new Vector3(col + 0.5f, row - i + 0.5f, 0f));
                        if (loadData.mapData[row - i, col + 1] == "3")
                        {
                            lstTargetBoomLeft.Add(new Vector3(col + 1.5f, row - i + 0.5f, 0f));
                            break;
                        }

                        if (loadData.mapData[row - i, col - 1] == "3")
                        {
                            lstTargetBoomLeft.Add(new Vector3(col - 0.5f, row - i + 0.5f, 0f));
                            break;
                        }
                    }
                    else
                    {
                        isCheckBoomLeft = false;
                    }
                }


                //checkRight
                if (isCheckBoomRight)
                {
                    if (loadData.mapData[row + i, col] == "3" || loadData.mapData[row + i, col] == "4")
                    {
                        lstTargetBoomRight.Add(new Vector3(col + i, row + 0.5f, 0f));
                        if (loadData.mapData[row + i, col + 1] == "3")
                        {
                            lstTargetBoomRight.Add(new Vector3(col + i, row + 1.5f, 0f));
                            break;
                        }

                        if (loadData.mapData[row + i, col - 1] == "3")
                        {
                            lstTargetBoomRight.Add(new Vector3(col + i, row - 0.5f, 0f));
                            break;
                        }
                    }
                    else
                    {
                        isCheckBoomRight = false;
                    }
                    //}
                }
                // else
                // {
                //     //checkUp
                //     if (isCheckBoomUp)
                //     {
                //         if (loadData.mapData[row, col + i] == "3")
                //         {
                //             lstTargetBoomUp.Add(new Vector3(col + 0.5f, row + i + 0.5f, 0f));
                //             break;
                //         }
                //         isCheckBoomUp = false;
                //     }
                //    
                //
                //     //checkDown
                //     if (isCheckBoomDown)
                //     {
                //         if (loadData.mapData[row, col - i] == "3")
                //         {
                //             lstTargetBoomDown.Add(new Vector3(col + 0.5f, row - i + 0.5f, 0f));
                //             break;
                //         }
                //
                //         isCheckBoomDown = false;
                //     }
                //     
                //
                //     //checkLeft
                //     if (isCheckBoomLeft)
                //     {
                //         if (loadData.mapData[row - i, col] == "3")
                //         {
                //             lstTargetBoomLeft.Add(new Vector3(col - i, row + 0.5f, 0f));
                //             break;
                //         }
                //
                //         isCheckBoomLeft = false;
                //     }
                //    
                //
                //     //checkRight
                //     if (loadData.mapData[row + i, col] == "3")
                //     {
                //         lstTargetBoomRight.Add(new Vector3(col + i, row + 0.5f, 0f));
                //         break;
                //     }
                // }
            }
        }

        private void SetTarget()
        {
            if (lstTarget.Count > 1)
            {
                int random = Random.Range(0, lstTarget.Count);
                Vector3 randomTarget = lstTarget[random];
                target = randomTarget;
            }
            else if (lstTarget.Count == 1)
            {
                if (spawnBoom > 0)
                {
                    Boom();
                    //currentState = StateBot.Boom;
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
}