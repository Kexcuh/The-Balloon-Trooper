using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Balloon moves horizontally and EXPANDS
public class BalloonMovement2 : MonoBehaviour
{
    public Vector2 speed = new Vector2(2f, 0f);  // Speed in x and y directions
    private Vector2 direction;                   // Direction of movement
    private Camera cam;                          // Reference to the main camera
    [SerializeField] AudioManager audio;
    [SerializeField] ScoreKeeper scoreKeeper;
    public GameObject balloon;
    public float scaleFactor = 1.01f;
    public Vector2 maxSize; //1.5f
    public int scoreValue;

    void Start()
    {
        direction = speed;                      // Initial movement direction
        cam = Camera.main;                      // Reference the main camera
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        balloon = GameObject.FindGameObjectWithTag("ExpandingBalloon");
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        maxSize = new Vector2(1.5f, 1.5f);
        InvokeRepeating("IncreaseSize", 2.0f, 0.1f);
        scoreValue = 4;
    }

    void Update()
    {
        
        Vector2 spriteSize = new Vector2(transform.localScale.x, transform.localScale.y);
        // Move the sprite based on speed and direction
        transform.Translate(direction * Time.deltaTime);
        


        // Get the sprite's position in screen space
        Vector3 spritePos = cam.WorldToViewportPoint(transform.position);

        // Check for collision with screen edges and reverse direction
        if (spritePos.x <= 0 || spritePos.x >= 1)
        {
            direction.x = -direction.x;         // Bounce horizontally
        }

        if(spriteSize.x >= maxSize.x && spriteSize.y >= maxSize.y)
        {
          explode();
        }

        updateValue();
    }

    public void IncreaseSize()
    {
       this.transform.localScale *= scaleFactor;

       
    }

    public void decreaseValue()
    {
      scoreValue--;
    }

        // Public method to generate a random Vector2
    public Vector2 GetRandomVector2()
    {
        float x = Random.Range(-9, 9);
        float y = Random.Range(1, 9);

        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Destroy upon contact with Player Projectile
        if (collision.gameObject.tag == "PlayerProjectile"){
            pop();
        }

        //Bounce off of player
        if (collision.gameObject.tag == "Player"){ 
            direction.y = -direction.y; 
        }

        if (collision.gameObject.tag == "Spike")
        {
          explode();
        }

        // else
        //   Debug.Log(collision.gameObject.tag);
        //animator.SetInteger("motion", IDLE);
    }
    
    public void pop() // 
    {
      Destroy(gameObject);
      audio.PlaySFX(audio.balloonPop);
      scoreKeeper.AddPoints(scoreValue);
    }

    public void explode()
    {
      Destroy(gameObject);
      audio.PlaySFX(audio.balloonPop);
      scoreValue = 0;
    }


    public void updateValue()
    {
      if (transform.localScale.x > 0.75f && transform.localScale.x < 0.76f)
       {
        scoreValue = 3;
       }
       else if (transform.localScale.x > 1.0f && transform.localScale.x < 1.1f)
       {
        scoreValue = 2;
       }
       else if (transform.localScale.x > 1.25f && transform.localScale.x <1.26f)
       {
        scoreValue = 1;
       }
       else if (transform.localScale.x > 1.5f && transform.localScale.x < 1.6f)
       {
        scoreValue = 0;
       }
    }
}
