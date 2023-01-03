using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilShip : Ship
{



    int direction = 0;
    const float forceVertical = 8000;
    const float forceHorizontal = -2000;
    const float shotFrequency = 1.5f;    
    const int point = 15; 
    bool isShooted=false;
    Timer shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        
        while (direction==0)
        {
            direction = Random.Range(-1, 2);
        }
        Bullet.InstantiateBullet(bulletPrefab,transform.position,-1);
        rb2d= GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(forceHorizontal,forceVertical*direction) , ForceMode2D.Impulse);
        shotTimer = gameObject.AddComponent<Timer>();
        shotTimer.Duration = shotFrequency;
        shotTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {

        // shot when timer is finished
        if(shotTimer.Finished)
        {
            if(isShooted==false)
            {
                Bullet.InstantiateBullet(bulletPrefab, transform.position,-1);
                shotTimer.Run();
                isShooted = true;
            }
           
        }
        else
        {
            isShooted = false;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /// if blue laser hit the ship decrease health of the ship
        if (collision.gameObject.CompareTag("blueLaser"))
        {
           health -= 25;
           if(health==0)
            {
                
                Instantiate<GameObject>(prefabExplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Hud hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
                hud.AddPoints(point);
               
            }
           
           Destroy(collision.gameObject);
           

        }
        if(collision.gameObject.CompareTag("rocket"))
        {
            Instantiate<GameObject>(prefabExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Hud hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
            hud.AddPoints(point);
            Destroy(collision.gameObject);
        }
       
    }

}
