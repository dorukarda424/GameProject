using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{

    // Start is called before the first frame update
    const float movementSpeed = 18;
    Rigidbody2D rb2d;
    GameObject blueShip;
    const float angleChangingSpeed=25;
    const float rocketDuration = 6;
    Timer deleteTimer;
    // Update is called once per frame
    private void Start()
    { 
        AudioManager.Play(AudioClipName.missile);
        deleteTimer = gameObject.AddComponent<Timer>();
        deleteTimer.Duration = rocketDuration;
        deleteTimer.Run();
        rb2d = GetComponent<Rigidbody2D>();
        blueShip = GameObject.FindGameObjectWithTag("playerShip");
    }
    void Update()
    {
        if (deleteTimer.Finished)
        {
            Destroy(gameObject);
        }


    }
    private void FixedUpdate()
    {
        
        if(blueShip!=null)
        {
            Vector2 direction = (Vector2)blueShip.transform.position - rb2d.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, -transform.right).z;
            rb2d.angularVelocity = -angleChangingSpeed * rotateAmount;
            rb2d.velocity = -transform.right * movementSpeed;
        }
        

    }
    public void InstantiateRocket()
    {
        

    }
}
