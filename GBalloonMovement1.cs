using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gold balloon moves in random directions
public class GBalloonMovement1 : MonoBehaviour
{
    public Vector2 speed = new Vector2(2f, 2f);  // Speed in x and y directions
    private Vector2 direction;                   // Direction of movement
    private Camera cam;                          // Reference to the main camera
    //public LogicScript logic;
    [SerializeField] AudioManager audio;
    [SerializeField] ScoreKeeper scoreKeeper;
    public int scoreValue;
    public int healthPoints;
    // Start is called before the first frame update
    void Start()
    {
        direction = GetRandomVector2();                      // Initial movement direction
        cam = Camera.main;                      // Reference the main camera
        
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        scoreValue = 1;
        healthPoints = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the sprite based on speed and direction
        transform.Translate(direction * Time.deltaTime);

        // Get the sprite's position in screen space
        Vector3 spritePos = cam.WorldToViewportPoint(transform.position);

        // Check for collision with screen edges and reverse direction
        if (spritePos.x <= 0 || spritePos.x >= 1)
        {
            direction.x = -direction.x;         // Bounce horizontally
        }
        if (spritePos.y <= 0 || spritePos.y >= 1)
        {
            direction.y = -direction.y;         // Bounce vertically
        }
    }

    public Vector2 GetRandomVector2()
    {
        float x = Random.Range(-9, 9);
        float y = Random.Range(1, 9);

        return new Vector2(x, y);
    }

    public void takeDamage()
    {
        healthPoints--;
    }
    public void pop() // 
    {
      Destroy(gameObject);
      audio.PlaySFX(audio.balloonPop);
      scoreKeeper.AddPoints(scoreValue);
      scoreKeeper.nextLevel();
    }

    public void explode()
    {
      Destroy(gameObject);
      audio.PlaySFX(audio.balloonPop);
      scoreValue = 0;
      scoreKeeper.restartLevel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Destroy upon contact with Player Projectile
        if (collision.gameObject.tag == "PlayerProjectile"){
            takeDamage();
            if(healthPoints == 0)
            {
                pop();
            }

        }

        //Bounce off of player
        if (collision.gameObject.tag == "Player"){ 
            direction.y = -direction.y; 
        }

        if (collision.gameObject.tag == "Ground"){
                direction.y *= -1;         // Bounce vertically
        }

        if (collision.gameObject.tag == "Spike")
        {
            takeDamage();
            if(healthPoints == 0)
            {
                explode();
                scoreKeeper.restartLevel();
            }
                
        }
    }
}
