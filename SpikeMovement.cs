using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    public Vector2 speed = new Vector2(2f, 0f);  // Speed in x and y directions
    private Vector2 direction;                   // Direction of movement
    private Camera cam;                          // Reference to the main camera
    
    [SerializeField] AudioManager audio;
    
    //public ScoreKeeper scoreKeeper;
    public int scoreValue;
    public int healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        direction = speed;
        cam = Camera.main;
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        scoreValue = 1;
        healthPoints = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        healthPoints--;
    }

    public void die()
    {
        Destroy(gameObject);
      //audio.PlaySFX(audio.balloonPop);
      //scoreKeeper.AddPoints(scoreValue);
    }

    public void explode()
    {
        Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Destroy upon contact with Player Projectile
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            takeDamage();
            if(healthPoints <= 0)
            {
                die();
            }

        }

        //Bounce off of player
        if (collision.gameObject.tag == "Player")
        { 
            direction.x = -direction.x; //bounce to other direction
        }

        if (collision.gameObject.tag == "Spike")
        {
            direction.x = -direction.x; //bounce to other direction
            takeDamage();

            if(healthPoints <= 0)
            {
                explode();
            }
        }
    }

}