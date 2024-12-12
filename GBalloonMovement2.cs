using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Still golden balloon
public class GBalloonMovement2 : MonoBehaviour
{
    [SerializeField] AudioManager audio;
    [SerializeField] ScoreKeeper scoreKeeper;
    public int scoreValue;
    public int healthPoints;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        scoreValue = 5;
        healthPoints = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void takeDamage()
    {
        healthPoints--;
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
