using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public int damage;

    private void OnBecameInvisible()
    {
       gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Enemy" )
        {
            gameObject.SetActive(false);
        }
    }
}
