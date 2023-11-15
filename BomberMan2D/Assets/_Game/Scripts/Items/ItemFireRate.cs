using _Game.Scripts.Character;
using UnityEngine;
using CharacterController = _Game.Scripts.Character.CharacterController;

public class ItemFireRate : Items
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            characterController.rangeBoom++;
            SetActive(false);
        }
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.rangeBoom++;
            playerController.SetText();
            SetActive(false);
        }
    }
}
