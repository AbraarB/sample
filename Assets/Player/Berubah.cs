using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Berubah : MonoBehaviour
{
    public SpriteRenderer playerTransform;
    public Sprite[] jadiGrimreaper;

    void TransformSprite()
    {
        playerTransform.sprite = jadiGrimreaper[0];
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
