using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Berubah : MonoBehaviour
{
    SpriteRenderer playerTransform;
    Sprite[] grimReaper;

    void TransformSprite()
    {
        playerTransform.sprite = grimReaper[0];
    }

    void Start()
    {
        playerTransform = gameObject.GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        
    }

/*    void OnTransform(InputValue value)
    {
        if (value.isPressed)
        {

        }
    }*/
}
