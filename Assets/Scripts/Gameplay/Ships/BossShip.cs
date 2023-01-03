using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShip : Ship
{
    [SerializeField]
    GameObject enemyRocket;
    int direction = 0;

    const float forceVertical = 4000;
    const float forceHorizontal = -2000;
    const float shotFrequency = 1.0f;
    const int point = 1000;
    bool isShooted = false;
    bool isShootedRocket = false;
    Timer shotTimer;
    float heightOfShip;
    float widthOfShip;
    // Start is called before the first frame update
    void Start()
    {
        this.health = 1000;
        this.reloadTime = 2.40f;
        rocketReload = gameObject.AddComponent<Timer>();
        rocketReload.Duration = reloadTime;
        rocketReload.Run();
        heightOfShip = gameObject.GetComponent<BoxCollider2D>().size.y;
        widthOfShip = gameObject.GetComponent<BoxCollider2D>().size.x;
        while (direction == 0)
        {
            direction = Random.Range(-1, 2);
        }
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(forceHorizontal, forceVertical * direction), ForceMode2D.Impulse);
        shotTimer = gameObject.AddComponent<Timer>();
        shotTimer.Duration = shotFrequency;
        shotTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {

        // shot when timer is finished
        if (shotTimer.Finished)
        {
            if (isShooted == false)
            {
                Fire();
                shotTimer.Run();
                isShooted = true;
            }

        }
        else
        {
            isShooted = false;
        }

        if (rocketReload.Finished)
        {
            if (isShootedRocket == false)
            {
                Instantiate(enemyRocket, transform.position, transform.rotation);
                rocketReload.Run();
                isShootedRocket = true;
            }

        }
        else
        {
            isShootedRocket = false;
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /// if blue laser hit the ship decrease health of the ship
        if (collision.gameObject.CompareTag("blueLaser"))
        {
            health -= 25;
            if (health <= 0)
            {

                Instantiate<GameObject>(prefabExplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Hud hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
                hud.AddPoints(point);

            }

            Destroy(collision.gameObject);


        }
        /// if rocket hit the ship decrease health and create explosion
        if (collision.gameObject.CompareTag("rocket"))
        {
            health -= 100;
            Instantiate<GameObject>(prefabExplosion, collision.gameObject.transform.position, Quaternion.identity);
            if (health <= 0)
            {

                Instantiate<GameObject>(prefabExplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Hud hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
                hud.AddPoints(point);

            }

            Destroy(collision.gameObject);
        }
        

    }
    void Fire()
    {

        Bullet.InstantiateBullet(bulletPrefab, new Vector2(transform.position.x - widthOfShip * 2 / 3, transform.position.y),-1);
        Bullet.InstantiateBullet(bulletPrefab, new Vector2(transform.position.x - widthOfShip * 2 / 3, transform.position.y + heightOfShip * 3 / 10),-1);
        Bullet.InstantiateBullet(bulletPrefab, new Vector2(transform.position.x - widthOfShip * 2 / 3, transform.position.y -heightOfShip * 3 / 10),-1);


    }
}
