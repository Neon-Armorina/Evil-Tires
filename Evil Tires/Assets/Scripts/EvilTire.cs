using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTire : EvilTires
{

    public AudioSource tirePickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag.Equals("Projectile"))
        {
            tirePickup.Play();
            Destroy(go);
            tiresCount--;
        }
    }
}
