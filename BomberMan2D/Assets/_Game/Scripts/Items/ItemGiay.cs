using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiay : Items
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            characterController.moveSpeed++;
            SetActive(false);
        }
    }
}
