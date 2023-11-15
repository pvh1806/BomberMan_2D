using _Game.Scripts.Canvas;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public sealed class PlayerController : CharacterController
    {
        public Vector2 direction = Vector2.down;
        [SerializeField] private KeyCode left, right, top, down , boom;
        private HeathPlayer heathPlayer;
        public void OnInit(HeathPlayer heathPlayer)
        {
            this.heathPlayer = heathPlayer;
            SetText();
        }

        public void SetText()
        {
            heathPlayer.gameObject.SetActive(true);
            heathPlayer.SetText(health,spawnBoom,rangeBoom,moveSpeed);
        }
        private void FixedUpdate()
        {
            if (!isDead)
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
                Vector2 translation = direction * (moveSpeed * Time.fixedDeltaTime);
                rigidbody.MovePosition(position + translation);
            }
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

        private void Die()
        {
            base.Die();
        }
    }
}

