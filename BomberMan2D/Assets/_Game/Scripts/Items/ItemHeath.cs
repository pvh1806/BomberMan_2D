using _Game.Scripts.Character;
using UnityEngine;
using CharacterController = _Game.Scripts.Character.CharacterController;

public class ItemHeath : Items
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            characterController.health++;
            SetActive(false);
        }
        if (other.CompareTag(Constant.TAG_PLAYER))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.health++;
            playerController.SetText();
            SetActive(false);
        }
    }
}
