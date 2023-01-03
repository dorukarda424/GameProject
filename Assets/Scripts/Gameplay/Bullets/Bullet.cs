using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// bullet
    /// </summary>
    const float RocketForce = 25;
    const float BulletForce = 30;
    const float BulletDuration = 2.7f;
    Rigidbody2D rb2d;
    Timer deleteTimer;

    // Update is called once per frame
    void Update()
    {
        if (deleteTimer.Finished)
        {
            Destroy(gameObject);
        }


    }
    /// <summary>
    /// instantiate bullet staticly from ships
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="position"></param>
    public static void InstantiateBullet(GameObject bulletPrefab, Vector3 position, float direction)
    {
        AudioManager.Play(AudioClipName.laser);
        GameObject bullet=Instantiate(bulletPrefab,position,Quaternion.identity);
        Bullet script=bullet.GetComponent<Bullet>();
        script.Fire(direction,BulletForce);
        
        
    }
    public static void InstantiateRocket(GameObject rocketPrefab,Vector3 position,float direction)
    {
        AudioManager.Play(AudioClipName.missile);
        GameObject rocket=Instantiate(rocketPrefab, position, Quaternion.identity);
        Bullet script = rocket.GetComponent<Bullet>();
        script.Fire(direction,RocketForce);
    }
    public void Fire(float direction,float force)
    {

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(force * direction, 0), ForceMode2D.Impulse);
        deleteTimer = gameObject.AddComponent<Timer>();
        deleteTimer.Duration = BulletDuration;
        deleteTimer.Run();
    }

}
