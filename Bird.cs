using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
  public Vector2 speed = new Vector2(2f, 0f);  // Speed in x and y directions
  private Vector2 direction;                   // Direction of movement
  private Camera cam;   
  [SerializeField] AudioManager audio;
  [SerializeField] ScoreKeeper scoreKeeper;
  public GameObject bird;
  public GameObject enemyProjectile;
  private float projectileSpeed;
  private bool isFacingRight;
  public int scoreValue;
  

  void Start()
  {
    direction = speed *= -1;                      // Initial movement direction
    cam = Camera.main;                      // Reference the main camera
    isFacingRight = false;
    audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
    InvokeRepeating("HandleBirdAttack", 0f, 2.5f);
    projectileSpeed = 5;
    scoreValue = 5;
  }

  void Update()
  {
    // Move the sprite based on speed and direction
    transform.Translate(direction * Time.deltaTime);

    // Get the sprite's position in screen space
    Vector3 spritePos = cam.WorldToViewportPoint(transform.position);

    // Check for collision with screen edges and reverse direction
    
    //if(transform.position.x <= -9 || transform.position.x >= 9)
    if (spritePos.x <= 0 || spritePos.x >= 1)
    {
        //direction.x = -direction.x;         // Bounce horizontally
        Flip();
    }


  }

  public void HandleBirdAttack()
  {
    ShootProjectile();
  }

  public void ShootProjectile()
  {
    Vector3 offset = new Vector3(0f,0.5f,0f);
    //audio.PlaySFX(audio.shoot);

    // Instantiate the projectile at the player's position
    GameObject projectile = Instantiate(enemyProjectile, transform.position - offset, Quaternion.identity);

    // Get the Rigidbody2D component from the projectile
    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

    // Determine the direction: 1 for right, -1 for left
    float direction = -1f;

    // Set the projectile's velocity to shoot horizontally in the correct direction
    rb.velocity = new Vector2(0f, direction * projectileSpeed);
  }

  private void UpdateBullet()
    {
        if(enemyProjectile.transform.position.y < -6 || enemyProjectile.transform.position.y > 6 )
        {
            Destroy(enemyProjectile);
        }
    }

  private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "PlayerProjectile"){
            die();
        }
    }

    private void die()
    {
      Destroy(bird);
      scoreKeeper.AddPoints(scoreValue);
    }
}