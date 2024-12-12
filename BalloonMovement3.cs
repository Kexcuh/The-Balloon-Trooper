using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Balloon does NOT move
public class BalloonMovement3 : MonoBehaviour
{
    [SerializeField] AudioManager audio;
    [SerializeField] ScoreKeeper scoreKeeper;
    public int scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        scoreValue = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        //Destroy upon contact with Player Projectile
        if (collision.gameObject.tag == "PlayerProjectile"){
            Destroy(gameObject);
            audio.PlaySFX(audio.balloonPop);
            scoreKeeper.AddPoints(scoreValue);
        }

        // else
        //   Debug.Log(collision.gameObject.tag);
        //animator.SetInteger("motion", IDLE);
    }
}
