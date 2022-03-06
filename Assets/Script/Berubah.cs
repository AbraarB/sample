using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Berubah : MonoBehaviour
{
    public SpriteRenderer playerTransform;
    public Sprite[] ncgrTransformation;
    public int currentSprite;

    void JadiGrimReaper()
    {
        playerTransform.sprite = ncgrTransformation[currentSprite];
        currentSprite++;
        if(currentSprite >= ncgrTransformation.Length)
        {
            currentSprite = 0;
        }
    }

    void Start()
    {
        playerTransform = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    void OnTransform(InputValue value)
    {
        if (value.isPressed)
        {
            JadiGrimReaper();
            Debug.Log("Transformed");
        }
    }
}
