using _Game.Scripts.Manager;
using UnityEngine;

namespace _Game.Scripts.Boom
{
    public class Fire_Boom : MonoBehaviour
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
                LevelManager.Ins.loadData.ChangeValue(row ,col , "3");
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constant.TAG_CHARACTER))
            {
                CharacterController characterController = collision.GetComponent<CharacterController>();
                characterController.health--;
                if (characterController.health <= 0)
                {
                    characterController.Die();
                }
            }
        }
    }
}