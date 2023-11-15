using _Game.Scripts.Character;
using _Game.Scripts.Manager;
using UnityEngine;
using CharacterController = _Game.Scripts.Character.CharacterController;

namespace _Game.Scripts.Boom
{
    public class FireBoom : MonoBehaviour
    {
        private float timer;
        private int col, row;
   
        public void Start()
        {
            timer = 0f;
            LevelManager.Ins.loadData.GetPos(transform.position, out row, out col);
        }
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {  
                Destroy(gameObject);
                LevelManager.Ins.loadData.ChangeValue(row ,col , "3");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constant.TAG_CHARACTER))
            {
                Bot bot = collision.GetComponent<Bot>();
                bot.health--;
                if (bot.health <= 0)
                {
                    bot.isDead = true;
                    bot.Die();
                    LevelManager.Ins.botCount--;
                    if (LevelManager.Ins.botCount == 0)
                    {
                        LevelManager.Ins.Dead("Player Win");
                    }
                }
            }

            if (collision.CompareTag(Constant.TAG_PLAYER))
            {
                PlayerController playerController = collision.GetComponent<PlayerController>();
                playerController.health--;
                playerController.SetText();
                if (playerController.health <= 0)
                {
                    playerController.isDead = true;
                    playerController.Die();
                    LevelManager.Ins.playerCount--;
                    if (LevelManager.Ins.botCount > 0)
                    {
                        if (LevelManager.Ins.playerCount == 0)
                        {
                            LevelManager.Ins.Dead("Bot Win");
                        }
                    }
                    else
                    {
                        string objectName = playerController.name;
                        if (objectName == "player1")
                        {
                            LevelManager.Ins.Dead("Player2 Win");
                        }
                        else
                        {
                            LevelManager.Ins.Dead("Player1 Win");
                        }
                    }
                }
            }
        }
    }
}