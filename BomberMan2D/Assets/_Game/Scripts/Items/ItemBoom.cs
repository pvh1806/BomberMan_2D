using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoom : Items
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            characterController.spawnBoom++;
            SetActive(false);
        }
    }
}
