using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShip : Ship 
{
    // Start is called before the first frame update
    /// <summary>
    /// Ship 
    /// </summary>
    [SerializeField]
    GameObject rocketPrefab;
    const int bullets = 1;
    Timer bulletReload;
 

   
    void Start()
    {
        bulletReload = gameObject.AddComponent<Timer>();
        bulletReload.Duration = 0.3f;
        bulletReload.Run();
        rocketReload = gameObject.AddComponent<Timer>();
        rocketReload.Duration = 1;
        rocketReload.Run();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        invisibilityTimer = gameObject.AddComponent<Timer>();
        invisibilityTimer.Duration = invisibilityTime;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /// fire
        if (Input.GetKeyDown(KeyCode.Space) && bulletReload.Finished)
        {
            
            Fire(bullets);
            bulletReload.Run();

        }
        if (Input.GetKeyDown(KeyCode.F) && rocketReload.Finished)
        {
            
            rocketReload.Duration = reloadTime;
            rocketReload.Run();
            Bullet.InstantiateRocket(rocketPrefab,transform.position,1);
         

        }



        if (invisibilityTimer.Finished)
        {
            if (!invisibiltyThisFrame)
            {
                invisibiltyThisFrame = true;
                spriteRender.color = new Color(1, 1, 1, 1);
            }
        
        }
        else
        {
            invisibiltyThisFrame = false;
        }
        

    }
    private void FixedUpdate()
    {
        /// ship movement
        Vector2 position = transform.position;
        if(Input.GetAxis("Horizontal")!=0)
        {
            
            float x = position.x;
            x += movePerSeconds * Time.deltaTime* Input.GetAxis("Horizontal");
            position.x = x;
            
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            
            float y = position.y;
            y += movePerSeconds * Time.deltaTime * Input.GetAxis("Vertical");
            position.y = y;
            
        }
        rb2d.MovePosition(position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /// if red laser hit the ship decrease health of the ship
        if(collision.gameObject.CompareTag("redLaser"))
        {
            if(!invisibilityTimer.Running)
            {
                health -= 10;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                HitEffect();
                invisibilityTimer.Run();
                Hud hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
                hud.RemoveHealth(10);
            }
            Destroy(collision.gameObject);



        }
        if (collision.gameObject.CompareTag("enemyRocket"))
        {
            if (!invisibilityTimer.Running)
            {
                Instantiate<GameObject>(prefabExplosion, collision.gameObject.transform.position, Quaternion.identity);
                health -= 50;
                if (health <= 0)
                {
                    health = 0;
                    Destroy(gameObject);
                }
                HitEffect();
                invisibilityTimer.Run();
                Hud hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
                hud.RemoveHealth(50);
                }
                Destroy(collision.gameObject);



        }

    }
    void HitEffect()
    {
        spriteRender.color = new Color(1, 1, 1, 0.5f);
    }
    /// <summary>
    /// for fire options
    /// </summary>
    /// <param name="bulletCount"></param>
    void Fire(int bulletCount)
    {
        
        if (bulletCount%2==0)
        {
            for (int i = 0; i < bulletCount+1; i++)
            {
                if(i-bulletCount/2!=0)
                {
                    Bullet.InstantiateBullet(bulletPrefab, new Vector2(transform.position.x, transform.position.y + i - bulletCount / 2),-1);
                }
                
            }
        }
        else
        {
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet.InstantiateBullet(bulletPrefab, new Vector2(transform.position.x, transform.position.y + i - (bulletCount-1)/ 2),1);
            }
        }
        
        
    }
    

}
