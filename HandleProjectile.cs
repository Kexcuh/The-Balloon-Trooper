using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleProjectileManagement();
    }


    void HandleProjectileManagement()
    {
        // Destroy player projectile IF offscreen
        //GameObject gameObject = GameObject.FindWithTag("PlayerProjectile");
        if(projectilePrefab.transform.position.x < -9 || projectilePrefab.transform.position.x > 9 )
        {
            Destroy(projectilePrefab);
        }

        // Destroy enemy projectil IF offscreen
        //gameObject = GameObject.FindWithTag("EnemyProjectile");
        if(projectilePrefab.transform.position.y < -6 || projectilePrefab.transform.position.y > 6 )
        {
            Destroy(projectilePrefab);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Balloon" || collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Ground")
        {
            Destroy(projectilePrefab);
        }
    }
}
