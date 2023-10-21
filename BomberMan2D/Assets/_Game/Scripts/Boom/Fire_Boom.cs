using UnityEngine;

public class Fire_Boom : MonoBehaviour
{
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
