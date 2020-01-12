using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private float horizontal;
    [SerializeField]
    private float movementSpeed;
    private bool facingRight;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float doubleJumpPower;

    public bool grounded;
    private int counter = 0;


    public GameObject dustEffect;
    private bool spawnDust;

    public GameObject whiteDust;

    CamShake cam;




    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CamShake>();
    }



    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        Flip(horizontal);
        Jump();
        MiniAtk();

    }

    private void HandleMovement(float horizontal)
    {
        myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void Jump()
    {

        if ((Input.GetKeyDown(KeyCode.W) && grounded) || (Input.GetKeyDown(KeyCode.W) && counter < 2))
        {
            if (counter > 0)
            {
                Instantiate(whiteDust, transform.position, transform.rotation);
                jumpPower = jumpPower - doubleJumpPower;

            }

            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpPower);
            counter++;
            //dust                     
        }

        myAnimator.SetBool("grounded", grounded);


    }

    private void MiniAtk()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger("mini attack");
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (counter > 1)
        {
            jumpPower = jumpPower + doubleJumpPower;
        }
        grounded = true;
        counter = 0;
        Instantiate(dustEffect, transform.position, transform.rotation);


        cam.CameraShake();


    }

    void OnCollisionExit2D(Collision2D col)
    {
        grounded = false;
        Instantiate(dustEffect, transform.position, transform.rotation);


    }



}