using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // new stuff = *** --------------------
    public GameObject explosion;
    private bool isDead = false;
    public Camera cam;
    Animator camAnim;
    //-------------------------------------
    private Rigidbody2D rb;
    private Animator anim;
    private float h;
    private Vector3 loc;
    public GameObject dustEffect;
    private bool spawnDust;
    public GameObject whiteDust;
    public bool grounded;
    private int counter = 0;
    public Transform spawnPoint;
    
    public Boss boss;
    

   


    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private float doubleJumpPower;


    [SerializeField]
    private bool facingRight;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float currentSpeed;

    public int hp = 30;
    public bool enter = false;
    public int dmg = 10;

    //*** Air Dust fix---------------------------------------------
    private Vector3 pos3;
    //---------------------------------------------------------

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = (transform.localScale.x > 0) ? true : false;
        grounded = false;
        //***----------------------------------------------------------------------------------
        pos3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        //-------------------------------------------------------------------------------------
    }

   
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        Move(h);
        Jump();
        ArrowPunch();
        // *** -------------------------------------------------------------------------------
        TakeDamage(dmg);
        Die();
        //------------------------------------------------------------------------------------
        //*** Air dust fix -------------------------------------------------------------------
        pos3.Set(transform.position.x, transform.position.y - 2, transform.position.z);
        //-------------------------------------------------------------------------------------
    }

    private void Move(float h)
    {
        changeDir(h);
        rb.velocity = new Vector2(h * movementSpeed, rb.velocity.y);

        anim.SetFloat("movementSpeed", Mathf.Abs(h));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // *** ako nqma tag ground shte smenq grounded (anim(air)) kogato se dokosne do Box
        if (col.gameObject.tag == "ground")
        {
            if (counter > 1)
            {
                jumpPower = jumpPower + doubleJumpPower;
            }

            grounded = true;
            counter = 0;
            Instantiate(dustEffect, transform.position, transform.rotation);
            

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        /// *** ---------------------------
        if (col.gameObject.tag == "ground")
        /// --------------------------------
        {
            grounded = false;
            Instantiate(dustEffect, transform.position, transform.rotation);
        }
    }

    private void changeDir(float h)
    {
        if ((facingRight && h < 0f) || (!facingRight && h > 0f))
        {
            loc = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
            transform.localScale = loc;
            facingRight = !facingRight;
        }
    } // on point

    private void ArrowPunch()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("arrowPunch", true);
            movementSpeed = 0;
            
        }
        else if (anim.GetBool("arrowPunch"))
        {
            
            anim.SetBool("arrowPunch", false);
            movementSpeed = currentSpeed;
            
                if (Bell.isContactingPlayer && Boss.isDead==false)
                {
                    Boss.instance.Die();
                }
        }

    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) && grounded) || (Input.GetKeyDown(KeyCode.W) && counter < 2))
        {
            if (counter > 0)
            {
                // *** air dust fix -------------------------------
                Instantiate(whiteDust, pos3, transform.rotation);
                // ------------------------------------------------
                jumpPower = jumpPower - doubleJumpPower;
                anim.SetTrigger("airJump");
            }

            counter++;


            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                               
        }

            anim.SetBool("grounded", grounded);
    } 
    //*** ---------------------------------------------
    public void TakeDamage(int dmg)
    {
        if (TongueCollider.isContactingPlayer)
        {
            hp -= dmg;
            TongueCollider.isContactingPlayer = false;
            anim.SetTrigger("damaged");
            camAnim.SetTrigger("screenShake");
        }
        if (BoxExplode.isContactingPlayer)
        {
            hp -= dmg;
            BoxExplode.isContactingPlayer = false;
            anim.SetTrigger("damaged");
            camAnim.SetTrigger("screenShake");
        }
    }

    void Die()
    {
        if (hp <= 0 && isDead == false)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            isDead = true;
            SceneManager.LoadScene("game over", LoadSceneMode.Single);
        }
    }
    //------------------------------------------------------------------------
    
   
    
}
