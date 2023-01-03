using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Sprite shipSpriteRed;
    [SerializeField]
    Sprite shipSpritePurple;
    [SerializeField]
    Sprite shipSpriteGreen;
    [SerializeField]
    GameObject evilShip;
    [SerializeField]
    GameObject bossShip;
    Timer timer;
    int shipCounter = 2;
    const float minSpawnTimer = 2;
    const float maxSpawnTimer = 3;
    bool bossSpawner=false;
    bool isSpawned = false;
    float widthOfShip;
    float heightOfShip;
    float heightOfBossShip;
    float widthOfBossShip;
    Hud hud;
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<Hud>();
        hud.EnemyCounter(shipCounter);
        GameObject ship=Instantiate<GameObject>(evilShip);
        heightOfShip = ship.GetComponent<BoxCollider2D>().size.y;
        widthOfShip = ship.GetComponent<BoxCollider2D>().size.x;
        Destroy(ship);
        ship = Instantiate<GameObject>(bossShip);
        heightOfBossShip = ship.GetComponent<BoxCollider2D>().size.y;
        widthOfBossShip = ship.GetComponent<BoxCollider2D>().size.x;
        Destroy(ship);
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = Random.Range(minSpawnTimer,maxSpawnTimer);
        timer.Run();
        



    }

    // Update is called once per frame
    void Update()
    {
        if(timer.Finished)
        {
            if(isSpawned==false)
            {
                /// instantiate new ship periodicly and vertically random locations 
                if (shipCounter >0)
                {
                    InstantiateShip();
                    shipCounter--;
                    hud.EnemyCounter(shipCounter);
                }
                  
                else if(shipCounter==0 && bossSpawner==false)
                {
                    timer.Duration = 3;
                    timer.Run();
                    bossSpawner = true;
                    isSpawned = true;
                    
                }
                else if(bossSpawner==true)
                {
                    Instantiate(bossShip, new Vector2(ScreenUtils.ScreenRight + widthOfBossShip/2, (ScreenUtils.ScreenTop / 2 - heightOfBossShip / 2)), Quaternion.identity);
                    isSpawned = true;
                }
            }
        }
        else
        {
            isSpawned = false;
        }
        
    }
    private void InstantiateShip()
    {
        int spriteNumber = (int)Random.Range(1, 4);
        float VerticalPosition = Random.Range(ScreenUtils.ScreenBottom + widthOfShip, ScreenUtils.ScreenTop - heightOfShip);
       
        
        
        Instantiate(evilShip, new Vector2(ScreenUtils.ScreenRight, VerticalPosition), Quaternion.identity);
        
        timer.Duration = Random.Range(minSpawnTimer, maxSpawnTimer);
        timer.Run();
        isSpawned = true;
        if (spriteNumber == 1)
        {
            evilShip.GetComponent<SpriteRenderer>().sprite = shipSpriteRed;
        }
        else if (spriteNumber == 2)
        {
            evilShip.GetComponent<SpriteRenderer>().sprite = shipSpritePurple;
        }
        else
        {
            evilShip.GetComponent<SpriteRenderer>().sprite = shipSpriteGreen;
        }
    }
}
