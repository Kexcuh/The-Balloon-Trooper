using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Balloon moves in random direction
public class BalloonMovement1 : MonoBehaviour
{
    public Vector2 speed = new Vector2(2f, 2f);  // Speed in x and y directions
    private Vector2 direction;                   // Direction of movement
    private Camera cam;                          // Reference to the main camera
    [SerializeField] AudioManager audio;
    [SerializeField] ScoreKeeper scoreKeeper;
    public int scoreValue;

    void Start()
    {
        direction = GetRandomVector2();                      // Initial movement direction
        cam = Camera.main;                      // Reference the main camera
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        scoreValue = 2;
    }

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

        // Public method to generate a random Vector2
    public Vector2 GetRandomVector2()
    {
        float x = Random.Range(-9, 9);
        float y = Random.Range(1, 9);

        return new Vector2(x, y);
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

        if (collision.gameObject.tag == "Ground"){
                direction.y *= -1;         // Bounce vertically
        }

        if (collision.gameObject.tag == "Spike")
        {
            explode();
        }
    }
    
}
