using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefabExplosion;

    [SerializeField]
    protected GameObject bulletPrefab;

    
    protected const float movePerSeconds = 15;
    protected const float invisibilityTime = 1;
    protected float reloadTime = 4;

    protected int health = 100;

    protected bool invisibiltyThisFrame = false;

    protected Timer invisibilityTimer;
    protected Timer rocketReload;

    protected SpriteRenderer spriteRender;

    protected Rigidbody2D rb2d;


}
