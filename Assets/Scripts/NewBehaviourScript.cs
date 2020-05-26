using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveinput;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform trans;
    public Animator animator;
    public BoxCollider2D box;


    private bool facingRight = true;


    //specialKey
    public float invisibleTime = 1f;
    private bool visible = true;
    public float MyAlphaValue = 10f;
    static private Color cVisible;
    static private Color cInvisible;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpValue;

    public float fallMultiplier;
    public float lowJumpMultiplier;


    private float groundRemember;
    public float groundRememberTime;

    public float cutJumpHeight;

    public float horizontalDamping;

    public float horizontalAccelerationValue;
    private float horizontalAcceleration;

    private bool crouching=false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        extraJumps = extraJumpValue;
        horizontalAcceleration = horizontalAccelerationValue;
        setSpawn();

        cVisible = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        cInvisible = new Color(sr.color.r, sr.color.g, sr.color.b, MyAlphaValue);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveinput = Input.GetAxisRaw("Horizontal");


        float horizontalVelocity = rb.velocity.x;
        horizontalVelocity += moveinput;
        horizontalVelocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 20f);
        if (crouching == true)
        {
            horizontalVelocity *= 0.5f;
            box.size = new Vector2(0.2f, 0.1f);
            box.offset = new Vector2(0, -0.107f);
        }
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));


        if(facingRight == false && moveinput > 0)
        {
            Flip();
        } else if(facingRight == true && moveinput < 0)
        {
            Flip();
        }
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            crouching = true;
            animator.SetBool("Crouching", crouching);
        }
        else { 
			crouching = false; animator.SetBool("Crouching", crouching);
		}
        if (!crouching)
        {
            box.size = new Vector2(0.2f, 0.2f);
            box.offset = new Vector2(0, -0.06f);
        }
        

        
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift) && visible)
        {
            sr.color = cInvisible;
            Debug.Log("invisible");
            visible = false;
            Invoke("MakeVisible", invisibleTime);
        }

        groundRemember -= Time.deltaTime;
        if(isGrounded == true)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            groundRemember = groundRememberTime;
            extraJumps = extraJumpValue;
        }
        if((Input.GetKeyDown(KeyCode.Space)) && extraJumps >0)
        {
            animator.SetBool("Jumping", true);
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if ((Input.GetKeyDown(KeyCode.Space)) && extraJumps == 0 && (groundRemember>0)) {
            animator.SetBool("Jumping", true);

            groundRemember = 0;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
            animator.SetBool("Jumping", false);

        }
        else if (rb.velocity.y > 0 && Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*cutJumpHeight);
            animator.SetBool("Jumping", false);

        }
        if(!animator.GetBool("Jumping")&& !isGrounded)
        {
            animator.SetBool("Falling", true);
        }


        //// SPÄTER ÄNDERN !!!!
        if (rb.position.y < -15f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }


    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void MakeVisible()
    {
        Debug.Log("visible");
        visible = true;
        sr.color = cVisible;
    }

    public bool getVisible()
    {
        return visible;
    }

    void setSpawn()
    {
        if (FindObjectOfType<GameManager>().testActiveScene())
        {
            Debug.Log("setSpawniftrue =  " + PlayerPrefs.GetString("scene") + "__" + PlayerPrefs.GetFloat("posX") + "__" + PlayerPrefs.GetFloat("posY"));
            //trans.position.Set(PlayerPrefs.GetInt("posX"), PlayerPrefs.GetInt("posY"),0.5f);
            Vector3 pos = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), 0.5f);
            Quaternion rot = new Quaternion(0f, 0f, 0f, 0f);
            trans.SetPositionAndRotation(pos, rot);
        }
    }

}
