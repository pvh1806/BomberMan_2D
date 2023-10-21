using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic component to destroy after x seconds an object.
public class DestroyAfter : MonoBehaviour {

    [SerializeField] private float timer;

    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, timer);
	}

    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }
}
