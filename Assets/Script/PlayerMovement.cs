using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerWalkspeed = 5f;
    [SerializeField] float jumpHeight = 3f;
    //[SerializeField] float dashDistance = 10f;

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    CapsuleCollider2D playerBodycollider;
    BoxCollider2D playerFeetcollider;

    //float dashCooldown;
    //bool isDashing;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBodycollider = GetComponent<CapsuleCollider2D>();
        playerFeetcollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Walk();
        FlipSprite();
    }

    // JALAN
    //input key jalan (A, D)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    //kecepatan jalan
    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerWalkspeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
    }

    //player hadap kanan/kiri
    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    // LONCAT
    //input key loncat (space)
    void OnJump(InputValue value)
    {
        if (!playerFeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpHeight);
            Debug.Log("Jumped!");
        }
    }

    // DASH
    //input key dash (shift)
    /*void OnDash(InputValue value)
    {
        //dash kiri
        
        
        //dash kanan
        
        
    }*/


}
