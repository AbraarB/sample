using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerWalkspeed = 5f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float dashDistance = 10f;
    [SerializeField] float dashCooldown = 3f;
    [SerializeField] float transCooldown = 0f;
    
    private float delay = 0.38f;
    private Animator anim;
    private bool isClickedX;
    private bool isClickedC;
    private float lastTransform;
    private int transformState = 1; //1 -> nc , 2 -> gr

    private bool isDashing;
    private float currentDashTime;
    private float startDashTime;
    private float movin;
    private float dashDirection;

    private enum MovementState {nc_idle, nc_walk, ncgmtrans, gm_idle, gm_walk, gmnctrans}

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    CapsuleCollider2D playerBodycollider;
    BoxCollider2D playerFeetcollider;

    MovementState state;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBodycollider = GetComponent<CapsuleCollider2D>();
        playerFeetcollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movin = Input.GetAxis("Horizontal");

        Walk();
        FlipSprite();
        Dash();
        Trans_nc_gm();
        trans_gm_nc();

        /*if (Input.GetKeyDown(KeyCode.X) && transformState == 1 && Time.time - lastTransform < transCooldown)
        {
            lastTransform = Time.time;
            Trans_nc_gm();
        }
        else if (Input.GetKeyDown(KeyCode.X) && transformState == 2 && Time.time - lastTransform < transCooldown)
        {
            lastTransform = Time.time;
            Trans_gm_nc();
        }
        else
        {
            return;
        }*/


    }

    // JALAN
    //input key jalan (A, D)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    //kecepatan jalan + dash mechanic
    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerWalkspeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
    }


    //Dash
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && movin != 0)
        {
            isDashing = true;
            currentDashTime = startDashTime;
            playerRigidbody.velocity = Vector2.zero;
            dashDirection = (int)movin;
            Debug.Log("Dashed!");
        }

        if (isDashing)
        {
            playerRigidbody.velocity = transform.right * dashDirection * dashDistance;

            currentDashTime -= Time.deltaTime;

            if (currentDashTime <= 0)
            {
                isDashing = false;
            }
        }
    }

    //player hadap kanan/kiri
    void FlipSprite()
    {
        
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);

            if (isClickedX)
            {
                state = MovementState.gm_walk;
            }
            else
            {
                state = MovementState.nc_walk;
            }

        }
        else if (!isPlayerMoving)
        {
            if (isClickedX)
            {
                Invoke("gm_idle", delay);
            }
            else
            {
                Invoke("nc_idle", delay);
            }
            
        }

        anim.SetInteger("state", (int) state);
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



    //berubah
    void Trans_nc_gm()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Berubah nc-gm!");
            state = MovementState.ncgmtrans;
            isClickedX = true;
        }

        anim.SetInteger("state", (int)state);
        //transformState = 2;
    }

    void trans_gm_nc()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Berubah gm-nc!");
            state = MovementState.gmnctrans;
            isClickedC = true;
            isClickedX = false;
        }

        anim.SetInteger("state", (int)state);
        //transformState = 1;
    }

    void gm_idle()
    {
        state = MovementState.gm_idle;
    }

    void nc_idle()
    {
        state = MovementState.nc_idle;
    }

    /*    void Trans_gm_nc()
        {

            //if (Input.GetKeyDown(KeyCode.X))
            //{
                Debug.Log("Berubah gm-nc!");
                state = MovementState.gmnctrans;
                isClicked = false;
            //}

            anim.SetInteger("state", (int)state);
            transformState = 1;
        }*/



}
