using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movement;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] const int SPEED = 3;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] bool jumpPressed = false;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded = true;
    private Camera cam;
    [SerializeField] AudioManager audio; //audio manager
    [SerializeField] ScoreKeeper scoreKeeper; // score keeper manager
    private float projectileSpeed; // speed of projectile character can shoot
    public GameObject projectilePrefab; // projectile profab
    public GameObject player; // reference to player cahracter

    //[SerializeField] Animator animator;

    const int IDLE = 0;
    const int RUN = 1;
    const int JUMP = 2;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        player = GameObject.FindGameObjectWithTag("Player");
        isFacingRight = true;
        projectileSpeed = 10f;
        jumpForce = 300.0f;
        cam = Camera.main;
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        /*if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetInteger("motion", IDLE);
        */
        
    }

    // Update is called once per frame --used for user input
    //do NOT use for physics & movement
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ShootProjectile();
        }
        

        //Vector3 spritePos = cam.WorldToViewportPoint(transform.position);
        
        /*
        if (spritePos.y <= 0)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            Destroy(gameObject);         // Bounce vertically
            
        }
        
*/
        if (transform.position.y < -6)
        {
            die();
        }
        
    }

    //called potentially many times per frame
    //use for physics & movement
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
            Flip();
        if (jumpPressed && isGrounded)
            Jump();
        else
        { 
            jumpPressed = false;
            /*if (isGrounded)
            {
                if (movement > 0 || movement < 0)
                {
                    animator.SetInteger("motion", RUN);
                }
                else
                {
                    animator.SetInteger("motion", IDLE);
                }
            }
            */
        }

        
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void Jump()
    {
        //animator.SetInteger("motion", JUMP);
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        //Debug.Log("jumped");
        jumpPressed = false;
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
        // else
        //   Debug.Log(collision.gameObject.tag);
        //animator.SetInteger("motion", IDLE);

        if (collision.gameObject.tag == "EnemyProjectile")
        {   
            die();
        }

        if (collision.gameObject.tag == "Spike")
        {
            die();
        }  
    }

    private void ShootProjectile()
    {
        Vector3 offset; //bullet offset from player model
        audio.PlaySFX(audio.shoot);
        
        if(isFacingRight)
        {
            offset = new Vector3(1f,0,0);
        }
        else
        {
            offset = new Vector3(-1f,0,0);
        }

        // Instantiate the projectile at the player's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity);

        // Get the Rigidbody2D component from the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Determine the direction: 1 for right, -1 for left
        float direction = isFacingRight ? 1f : -1f;

        // Set the projectile's velocity to shoot horizontally in the correct direction
        rb.velocity = new Vector2(direction * projectileSpeed, 0f);
    }
    


    private void die()
    {
        Destroy(player);
        scoreKeeper.loseALife();

        if(scoreKeeper.playerLives <= 0)
        {
            scoreKeeper.GameOver();
        }
        else
        {   
            scoreKeeper.restartLevel();
        }
        
    }
    
}
